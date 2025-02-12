namespace TextRPGTeam30
{
    public class Harden : UtilitySkill
    {
        public Harden()
        {
            name = "단단해지기";
            cost = 15;
            dDefense = 1;
            count = 1;
        }

        public Harden(int cost, int dAttack, int dDefense, int count) : base(cost, dAttack, dDefense, count)
        {

        }
        public override void PrintUseSkill(ICharacter target)
        {
            Console.WriteLine($"{name} 스킬 사용! (MP {cost} 소모)");

            Console.WriteLine($" {target.Name}의 방어력 + {dDefense}");
        }
    }
}