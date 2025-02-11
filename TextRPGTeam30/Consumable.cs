using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class Consumable(string itName, int itAbility, string itType, string itInfo) : Item(itName, itAbility, itType, itInfo)
    {
        public int ID { get; set; }
        public string Name { get; set; }
        // public int Price { get; set; }
        public string IDescription { get; set; } // 변수 설명용      

        public int itemCount;

        public void Use(Player player)
        {

        }
        public override void Use()
        {
            Console.WriteLine($"포션 {Name} 사용!");
        }
    }
}
