namespace TextRPGTeam30
{
    public class HealingPotion : Consumable
    {
        public int HealAmount { get; set; } 
        
        public HealingPotion(string itName, int itAbility, string itType, string itInfo, int price, int count) : base(itName, itAbility, itType, itInfo, price)
        {
            HealAmount = 30;
            itemCount = count;
        }

        public HealingPotion(string itName, int itAbility, string itType, string itInfo, int price) : base(itName, itAbility, itType, itInfo, price)
        {
            
        }

        public void UsePotion(ICharacter character)
        {
            SoundManager.Instance.PlaySound("potion");
            Console.WriteLine($"{character.Name}은 힐링 포션을 사용했습니다.");
        }
    }
}
