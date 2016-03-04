using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrestSharp.Implementation;
using CrestSharp.Model;
using CrestSharp.Model.Implementation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CrestSharp.Infrastructure
{
    public class CrestJsonConverter : JsonConverter
    {
        public static readonly IDictionary<Type, Type> CONVERSIONS;
        private static readonly Type ICREST_OBJECT_TYPE = typeof (ICrestObject);

        static CrestJsonConverter()
        {
            var types = ICREST_OBJECT_TYPE.Assembly.GetTypes();

            CONVERSIONS = types.Where(x => !x.IsAbstract && ICREST_OBJECT_TYPE.IsAssignableFrom(x))
                .ToDictionary(GetTopInterface, x => x);

            CONVERSIONS[typeof (IVector3D)] = typeof (Vector3D);
            CONVERSIONS[typeof (IAttacker)] = typeof (Attacker);
            CONVERSIONS[typeof (IVictim)] = typeof (Victim);
            CONVERSIONS[typeof (IVictimItem)] = typeof (VictimItem);
            CONVERSIONS[typeof (IVector3D)] = typeof (Vector3D);
            CONVERSIONS[typeof (IUriResource)] = typeof (UriResource);
            CONVERSIONS[typeof (ITypeDogma)] = typeof (TypeDogma);
            CONVERSIONS[typeof (ITypeDogmaAttribute)] = typeof (TypeDogmaAttribute);
            CONVERSIONS[typeof (ITypeDogmaEffect)] = typeof (TypeDogmaEffect);
            CONVERSIONS[typeof (Page<ICrestCategory>)] = typeof (Page<ICrestCategory>);
            CONVERSIONS[typeof (Page<ICrestGroup>)] = typeof (Page<ICrestGroup>);
            CONVERSIONS[typeof(Page<IMarketItem>)] = typeof(Page<IMarketItem>);
            CONVERSIONS[typeof(Page<MarketItem>)] = typeof(Page<MarketItem>);
            CONVERSIONS[typeof(Page<CrestCategory>)] = typeof(Page<CrestCategory>);
            CONVERSIONS[typeof(Page<CrestGroup>)] = typeof(Page<CrestGroup>);
        }

        private static Type GetTopInterface(Type type)
        {
            var iCrestObjectType = typeof (ICrestObject);
            var interfaces = type.GetInterfaces();
            var topLevelInterfaces = interfaces.Except(interfaces.SelectMany(t => t.GetInterfaces()));
            return topLevelInterfaces.Single(x => iCrestObjectType.IsAssignableFrom(x));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
           throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return null;
                }

                var item = JObject.Load(reader);

                CrestDocumentLoader.ThrowIfIsException(item);

                var href = (string) item["href"];
                var targetType = CONVERSIONS[objectType];

                var isICrestObjectType = ICREST_OBJECT_TYPE.IsAssignableFrom(targetType);
                if (href != null && isICrestObjectType)
                {
                    var cachedValue = Task.Factory.StartNew(async ()=> await Crest.Settings.Cache.Get<ICrestObject>(href), TaskCreationOptions.DenyChildAttach).Unwrap()
                        .Result;
                    if (cachedValue != null)
                    {
                        return cachedValue;
                    }
                }

                var target = Activator.CreateInstance(targetType);

                if (href != null && isICrestObjectType)
                {
                    InitObject(serializer, target, href, item);
                }
                else
                {
                    serializer.Populate(item.CreateReader(), target);
                }

                return target;
            }
            catch (Exception e)
            {
                //TODO log
                Console.WriteLine("error in readjson");
                throw;
            }
        }

        private static void InitObject(JsonSerializer serializer, object target, string href, JObject item)
        {
            var crestObject = (ICrestObject) target;
            crestObject.Href = href;

            serializer.Populate(item.CreateReader(), target);
            Task.Run(() => Crest.Settings.Cache.Put(crestObject));
        }

        

        public override bool CanConvert(Type objectType)
        {
            return CONVERSIONS.ContainsKey(objectType);
        }

        public override bool CanWrite => false;
    }
}
