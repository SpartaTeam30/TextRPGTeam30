using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class Illusion : UtilitySkill // 환영을 만들어 적을 혼란시키는 마법
    {
        public Illusion()
        {
            name = "일루전";
            dAttack = (int)1.5f;
            cost = 20;
        }
        public Illusion(int cost, int dAttack, int dDefense) : base(cost, dAttack, dDefense)
        {

        }
    }
}
