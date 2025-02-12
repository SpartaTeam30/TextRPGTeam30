using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class LegTrip : UtilitySkill // 상대의 다리를 걸어 움직이지 못 하게 하는 스턴 기술.
    {
        public LegTrip()
        {
            name = "다리 걸기";
            description = "상대의 다리를 걸어 움직임을 방해해 공격력을 감소시킨다.";
            dAttack = -2;
            cost = 25;
            count = 1;
        }

        public LegTrip(string description, int cost, int dAttack, int dDefense, int count) : base(description, cost, dAttack, dDefense, count)
        {

        }

        public override void PrintUseSkill(ICharacter target)
        {
            Console.WriteLine($"{name} 스킬 사용! (MP {cost} 소모)");

            Console.WriteLine($" {target.Name}의 공격력 {dAttack}");
        }
    }
}
