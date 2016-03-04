using System;

namespace CrestSharp.Model
{
    public class CharacterInfo
    {
        public string CharacterName { get; set; }
        public long CharacterID { get; set; }

        public string Scopes { get; set; }

        public DateTime ExpiresOn { get; set; }

    }
}