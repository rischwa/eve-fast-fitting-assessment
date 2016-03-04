namespace CrestSharp.Model.Implementation
{
    public class TypeDogmaEffect : ITypeDogmaEffect
    {
        public ICrestDogmaEffect Effect { get; set; }

        public bool IsDefault { get; set; }
    }
}
