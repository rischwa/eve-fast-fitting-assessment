using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CrestSharpTests
{

    
    public interface IX
    {
        string A { get; }
    }
    

    public class X : IX
    {
        private string _a;

        public string A
        {
            get { return _a; }
            set { _a = value; }
        }
    }

    public interface IY
    {
        List<IX> Z { get;
            set; } 
    }

    public class Y : IY
    {
    
        public List<IX> Z { get; set; } 
    }

    public class MyContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            if (typeof (IX).IsAssignableFrom(type))
            {
                var props = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Select(f => base.CreateProperty(f, memberSerialization))
                    .ToList();
                props.ForEach(
                              p =>
                              {
                                  p.PropertyName = p.UnderlyingName.Substring(1);
                                  p.Writable = true;
                                  p.Readable = true;
                              });
                return props;
            }
            else
            {
                return base.CreateProperties(type, memberSerialization);
            }
        }
    }

    public class MyConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object target;
            if (objectType == typeof (Y))
            {
                target = new Y();

            }
            else
            {
                 target = new X();
            }
            serializer.Populate(reader, target);

            return target;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (IX) || objectType == typeof(Y);
        }
    }

    [TestClass]
    public class PowerJsonTest
    {
        private const string jstr = "{\"z\":[{\"a\":\"bc\"}]}";

        [TestMethod]
        public void TestDeserializationPolymorphism()
        {
           var y= JsonConvert.DeserializeObject<Y>(
                                             jstr,
                                             new JsonSerializerSettings()
                                             {
                                                 Converters = new[] {new MyConverter()},
                                                 ContractResolver = new MyContractResolver()
                                             });
            Assert.IsNotNull(y);
            Assert.IsNotNull(y.Z);
            Assert.AreEqual(1, y.Z.Count);
            Assert.AreEqual("bc", y.Z.Single().A);
        }
    }

    //public class MyController : DefaultReflectionController
    //{
    //    public override string GetTypeAlias(Type type)
    //    {
    //        if (type.IsGenericTypeDefinition && type.)
    //        return base.GetTypeAlias(type);
    //    }
    //}
}
