using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class Dimension_Hall : OffensiveSkill // 단일 공격
    {
        public Dimension_Hall() // 바닥에 마법진을 설치하고 이 마법진에 닿으면 적이 증발해버린다. 밟으면 아군 즉사.
        {
            name = "디멘션 홀";
            damageModifier = 6.0f;
            cost = 100;
            count = 1;
        }
        public Dimension_Hall(float damageModifier, int cost, int count) : base(damageModifier, cost, count)
        {

        }
    }
}
