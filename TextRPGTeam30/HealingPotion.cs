using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class HealingPotion : Consumable
    {
        public int heal;

        public HealingPotion(string itName, int itAbility, string itType, string itInfo) : base(itName, itAbility, itType, itInfo)
        {

        }
    }
}
