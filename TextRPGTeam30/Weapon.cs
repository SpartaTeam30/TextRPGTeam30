namespace TextRPGTeam30
{
    public class Weapon : Equipable
    {
        public Weapon(string itName, int itAbility, string itType, string itInfo, int price) : base(itName, itAbility, itType, itInfo, price)
        {

        }

        // public Weapon()
        
        public void Toggle()
        {
            isEquip = !isEquip;
        }
    }
}
