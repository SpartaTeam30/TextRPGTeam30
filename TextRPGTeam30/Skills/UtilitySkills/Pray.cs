using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class Pray : UtilitySkill // 저주 해제 마법 스킬
    {
        public Pray()
        {
            name = "프레이";
            description = "간절한 기도로 공격력이 증가한다.";
            dAttack = 2;
            cost = 20;
        }

        public Pray(string description, int cost, int dAttack, int dDefense, int count) : base(description, cost, dAttack, dDefense, count)
        {

        }

        public override void PrintUseSkill(ICharacter target)
        {
            Console.WriteLine($"{name} 스킬 사용! (MP {cost} 소모)");

            Console.WriteLine($" {target.Name}의 공격력 + {dAttack}");
        }
    }
}
