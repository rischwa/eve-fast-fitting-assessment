using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CrestSharp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CrestSharp.Infrastructure
{
    /// <summary>
    ///     This contract resolver gets only private fields through the whole class hierarchy for ICrestObject subclasses.
    ///     This is unfortunately needed, because JSON.NET always reads a _property_ when it writes it with a custom
    ///     JsonConverter.
    ///     That means a lazy load of an object would trigger an infinite loop of loading the same object.
    ///     Therefor we use this to read/write the backing fields instead of the properties which doesn't trigger the lazy loading.
    /// </summary>
    public class CrestJsonContractResolver : DefaultContractResolver
    {
        private static readonly Type CREST_OBJECT_TYPE = typeof (ICrestObject);

        private static IEnumerable<FieldInfo> GetAllFields(Type t)
        {
            if (t == null)
            {
                return Enumerable.Empty<FieldInfo>();
            }

            const BindingFlags FLAGS = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            return t.GetFields(FLAGS)
                .Concat(GetAllFields(t.BaseType));
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            if (CREST_OBJECT_TYPE.IsAssignableFrom(type))
            {
                var targetType = type.IsInterface ? CrestJsonConverter.CONVERSIONS[type] : type;
                var props = GetAllFields(targetType)
                    .Select(f => CreateProperty(f, memberSerialization))
                    .ToList();
                props.ForEach(
                              p =>
                              {
                                  //remove underscore of field names
                                  p.PropertyName = p.UnderlyingName.Substring(1);
                                  p.Writable = true;
                                  p.Readable = true;
                              });
                return props;
            }
            return base.CreateProperties(type, memberSerialization);
        }
    }
}
