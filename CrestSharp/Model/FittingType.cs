using CrestSharp.Model.Implementation;

namespace CrestSharp.Model
{
    public class FittingType
    {
        public FittingType()
        {
        }

        public FittingType(ICrestType type)
        {
            Id = type.Id;
            Name = type.Name;
            Href = type.Href;
        }

        public string Name { get; set; }

        public int Id { get; set; }

        public string Href { get; set; }

        public ICrestType AsCrestType()
        {
            return new CrestType
                   {
                       Id = Id,
                       Href = Href,
                       Name = Name
                   };
        }

        public static FittingType FromCrestType(ICrestType type)
        {
            return new FittingType(type);
        }
    }
}