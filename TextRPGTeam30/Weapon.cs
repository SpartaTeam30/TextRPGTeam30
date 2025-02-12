namespace TextRPGTeam30
{
    public class Weapon : Equipable
    {
        public float attack;

        public Weapon(string itName, int itAbility, string itType, string itInfo, int price, object 가격) : base(itName, itAbility, itType, itInfo, price)
        {
            attack = itAbility;
        }

        // public Weapon()
        
        public void Toggle()
        {
            isEquip = !isEquip;
        }
    }
}
