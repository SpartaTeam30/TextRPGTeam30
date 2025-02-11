namespace TextRPGTeam30
{
    public class Weapon : Equipable
    {
        public float Attack;

        public Weapon(string itName, int itAbility, string itType, string itInfo, int price) : base(itName, itAbility, itType, itInfo, price)
        {
            attack = _attack;
        }

        // public Weapon()
        
        public void Toggle()
        {
            isEquip = !isEquip;
        }
    }
}
