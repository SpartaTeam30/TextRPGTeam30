namespace TextRPGTeam30
{
    public class HealingPotion : Consumable
    {
        public int heal;

        public HealingPotion(string itName, int itAbility, string itType, string itInfo, int price) : base(itName, itAbility, itType, itInfo, price)
        {

        }
    }
}
