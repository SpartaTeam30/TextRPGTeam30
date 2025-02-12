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
            dAttack = (int)0.5f;
            cost = 5;
            count = 3;
        }
        public Sand(int cost, int dAttack, int dDefense, int count) : base(cost, dAttack, dDefense, count)
        {

        }
    }
}
