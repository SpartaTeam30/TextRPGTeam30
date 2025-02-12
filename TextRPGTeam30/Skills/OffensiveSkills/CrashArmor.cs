using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class CrashArmor : OffensiveSkill
    {
        public CrashArmor() // 방어를 무시하는 맹렬한 공격을 한다. 1인 타격
        {
            name = "크래쉬 아머";
            description = "방어를 무시하는 맹렬한 공격을 한다.";
            damageModifier = 3.5f;
            cost = 40;
            count = 1;
        }

        public CrashArmor(string description, float damageModifier, int cost, int count) : base(description, damageModifier, cost, count)
        {

        }
    }
}
