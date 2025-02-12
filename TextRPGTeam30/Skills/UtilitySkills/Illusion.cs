using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class Illusion : UtilitySkill // 환영을 만들어 적을 혼란시키는 마법
    {
        public Illusion()
        {
            name = "일루전";
            description = "환영을 만들어 적을 혼란시키고 방어력을 감소시킨다.";
            dDefense = -1;
            cost = 15;
            count = 1;
        }

        public Illusion(string description, int cost, int dAttack, int dDefense, int count) : base(description, cost, dAttack, dDefense, count)
        {

        }

        public override void PrintUseSkill(ICharacter target)
        {
            Console.WriteLine($"{name} 스킬 사용! (MP {cost} 소모)");

            Console.WriteLine($" {target.Name}의 방어력 {dDefense}");
        }
    }
}
