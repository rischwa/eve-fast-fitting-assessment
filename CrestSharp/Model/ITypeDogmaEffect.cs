namespace CrestSharp.Model
{
    public interface ITypeDogmaEffect
    {
        ICrestDogmaEffect Effect { get; }

        bool IsDefault { get; }
    }
}