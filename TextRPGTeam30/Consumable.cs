using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    public class Consumable : Item 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        // public int Price { get; set; }
        public string IDescription { get; set; } // 변수 설명용      

        public int itemCount;

        public Consumable(string itName, int itAbility, string itType, string itInfo) : base(itName, itAbility, itType, itInfo)
        {

        }

        public void Use(Player player)
        {

        }
    }
}
