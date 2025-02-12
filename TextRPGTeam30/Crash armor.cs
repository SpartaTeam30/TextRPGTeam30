using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class Crash_armor : OffensiveSkill
    {
        public Crash_armor() // 방어를 무시하는 맹렬한 공격을 한다. 1인 타격
        {
            name = "크래쉬 아머";
            damageModifier = 3.5f;
            cost = 40;
        }

        public Crash_armor(float damageModifier, int cost) : base(damageModifier, cost)
        {

        }              
    }
}
