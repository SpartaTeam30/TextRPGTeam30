using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class Frey : UtilitySkill // 저주 해제 마법 스킬
    {
        public Frey()
        {
            name = "프레이";
            dAttack = (int) 1.0f;
            cost = 15;
            count = 1;
        }

        public Frey(int cost, int dAttack, int dDefense, int count) : base(cost, dAttack, dDefense, count)
        {

        }
    }
}
