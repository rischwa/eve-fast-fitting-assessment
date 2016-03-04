using CrestSharp.Model.Implementation;

namespace CrestSharp.Model
{
    public class AuthenticatedCrestCharacter
    {
        public UriResource Standings { get; set; }

        public UriResource BloodLine { get; set; }

        public UriResource Waypoints { get; set; }

        public CharacterPortrait Portrait { get; set; }

        public string Name { get; set; }

        public long Id { get; set; }

        public UriResource Fittings { get; set; }

        public UriResource Location { get; set; }
    }
}