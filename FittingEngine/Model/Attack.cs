namespace FittingEngine.Model
{
    public class Attack
    {
        public Attack(Item weapon, double damage)
        {
            Weapon = weapon;
            Damage = damage;
        }

        public double Damage { get; private set; }
        public Item Weapon { get; private set; }
    }
}