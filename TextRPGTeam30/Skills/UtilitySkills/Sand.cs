using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class Sand : UtilitySkill // 적에게 흙을 뿌려서 적의 공격을 방해한다. 명중률 하락.
    {
        public Sand()
        {
            name = "흙뿌리기";
            description = "적에게 흙을 뿌려 공격력을 감소시킨다.";
            dAttack = -1;
            cost = 25;
            count = 3;
        }
        public Sand(string description,int cost, int dAttack, int dDefense, int count) : base(description, cost, dAttack, dDefense, count)
        {

        }

        public override void PrintUseSkill(ICharacter target)
        {
            Console.WriteLine($"{name} 스킬 사용! (MP {cost} 소모)");

            Console.WriteLine($" {target.Name}의 공격력 {dAttack}");
        }
    }
}
