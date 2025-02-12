namespace TextRPGTeam30
{
    public class Growl : UtilitySkill
    {
        public Growl()
        {
            name = "울부짖기";
            cost = 15;
            dAttack = -1;
            count = 1;
        }

        public Growl(int cost, int dAttack, int dDefense, int count) : base(cost, dAttack, dDefense, count)
        {

        }

        public override void PrintUseSkill(ICharacter target)
        {
            Console.WriteLine($"{name} 스킬 사용! (MP {cost} 소모)");

            Console.WriteLine($" {target.Name}의 공격력 {dAttack}");
            
            return;
        }
    }
}
