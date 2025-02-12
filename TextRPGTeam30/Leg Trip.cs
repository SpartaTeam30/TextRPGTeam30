using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class Leg_Trip : UtilitySkill // 상대의 다리를 걸어 움직이지 못 하게 하는 스턴 기술.
    {
        public Leg_Trip()
        {
            name = "다리 걸기";
            dAttack = (int)1.0f;
            cost = 5;
            count = 1;
        }
        public Leg_Trip(int cost, int dAttack, int dDefense, int count) : base(cost, dAttack, dDefense, count)
        {

        }
    }
}
