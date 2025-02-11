namespace TextRPGTeam30
{
    public class ManaPotion : Consumable
    {
        public int ManaAmount { get; set; }
   
        public ManaPotion(string itName, int itAbility, string itType, string itInfo, int price) : base(itName, itAbility, itType, itInfo, price)
        {
            ManaAmount = 30;
        }

        public void UsePotion(ICharacter character)
        {
            Console.WriteLine($"{character.Name}은 마나 포션을 사용했습니다.");
        }
    }
}



