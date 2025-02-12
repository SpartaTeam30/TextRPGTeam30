namespace TextRPGTeam30
{
    public class Growl : UtilitySkill
    {
        public Growl()
        {
            name = "울부짖기";
            description = "커다란 소리를 내어 상대의 공격력을 감소시킨다.";
            cost = 15;
            dAttack = -1;
            count = 1;
        }

        public Growl(string description, int cost, int dAttack, int dDefense, int count) : base(description, cost, dAttack, dDefense, count)
        {

        }

        public override void PrintUseSkill(ICharacter target)
        {
            Console.WriteLine($"{name} 스킬 사용! (MP {cost} 소모)");

            Console.WriteLine($" {target.Name}의 공격력 {dAttack}");
        }
    }
}
