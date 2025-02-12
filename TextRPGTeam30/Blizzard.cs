using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPGTeam30
{
    internal class Blizzard : OffensiveSkill
    {
        public Blizzard() // 시전자 주위로 폭풍을 일으킨다. 광역기.
        {
            name = "블리자드";
            damageModifier = 3.0f;
            cost = 25;
        }

        public Blizzard(float damageModifier, int cost) : base(damageModifier, cost)
        {

        }
    }
}
