using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    public class Consumable : Item 
    {   
        public int itemCount;

        public Consumable(string itName, int itAbility, string itType, string itInfo, int price) : base(itName, itAbility, itType, itInfo, price)
        {

        }

        public void Use(Player player)
        {

        }
    }
}
