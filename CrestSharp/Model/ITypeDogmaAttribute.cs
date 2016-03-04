namespace CrestSharp.Model
{
    public interface ITypeDogmaAttribute
    {
        ICrestDogmaAttribute Attribute { get; } 
        double? Value { get; }
    }
}