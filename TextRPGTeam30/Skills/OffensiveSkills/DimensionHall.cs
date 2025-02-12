using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class DimensionHall : OffensiveSkill // 단일 공격
    {
        public DimensionHall() // 바닥에 마법진을 설치하고 이 마법진에 닿으면 적이 증발해버린다. 밟으면 아군 즉사.
        {
            name = "디멘션 홀";
            description = "적을 증발시키는 마법진을 설치한다.";
            damageModifier = 6.0f;
            cost = 100;
            count = 1;
        }

        public DimensionHall(string description, float damageModifier, int cost, int count) : base(description, damageModifier, cost, count)
        {

        }
    }
}
