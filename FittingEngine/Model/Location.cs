namespace FittingEngine.Model
{
    public class Location : Item
    {
        public Location(string typeName, int typeId,  int groupId, int categoryId, int mass, int capacity)
            : base(typeName, typeId, groupId, categoryId, mass, capacity)
        {
        }
    }
}