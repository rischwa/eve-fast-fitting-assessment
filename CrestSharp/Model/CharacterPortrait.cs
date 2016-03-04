using CrestSharp.Model.Implementation;
using Newtonsoft.Json;

namespace CrestSharp.Model
{
    public class CharacterPortrait
    {
        [JsonProperty("256x256")]
        public UriResource Image256 { get; set; }
    }
}