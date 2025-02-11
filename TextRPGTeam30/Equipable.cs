namespace TextRPGTeam30
{
    public class Equipable : Item
    {
        public bool isEquip;

        public Equipable(string Name, int ItAbility, string ItType, string ItInfo, int price) : base(Name, ItAbility, ItType, ItInfo, price)
        {

        }

        public void Toggle()
        {
            isEquip = !isEquip;
        }
    }
}

