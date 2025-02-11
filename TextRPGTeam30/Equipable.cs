namespace TextRPGTeam30
{
    public class Equipable : Item
    {
        public int ID { get; set; }
        // public int Price { get; set; }
        public string IDescription { get; set; } // 변수 설명용
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

