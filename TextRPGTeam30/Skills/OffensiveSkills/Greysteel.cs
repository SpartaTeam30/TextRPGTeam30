using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class Greysteel : OffensiveSkill
    {
        public Greysteel() // 검날이 창백하게 빛나면서 거대한 폭발을 일으키는 다인 스킬.
        {
            name = "그레이스틸";
            description = "검날이 빛나면서 거대한 폭발을 일으킨다.";
            damageModifier = 4.0f;
            cost = 30;
            count = 3;
        }

        public Greysteel(string description, float damageModifier, int cost, int count) : base(description, damageModifier, cost, count)
        {

        }
    }
}
