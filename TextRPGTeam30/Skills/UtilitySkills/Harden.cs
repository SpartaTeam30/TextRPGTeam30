namespace TextRPGTeam30
{
    public class Harden : UtilitySkill
    {
        public Harden()
        {
            name = "단단해지기";
            description = "몸에 힘을 주어 방어력이 증가한다.";
            cost = 15;
            dDefense = 1;
            count = 1;
        }

        public Harden(string description, int cost, int dAttack, int dDefense, int count) : base(description, cost, dAttack, dDefense, count)
        {

        }

        public override void PrintUseSkill(ICharacter target)
        {
            Console.WriteLine($"{name} 스킬 사용! (MP {cost} 소모)");

            Console.WriteLine($" {target.Name}의 방어력 + {dDefense}");
        }
    }
}