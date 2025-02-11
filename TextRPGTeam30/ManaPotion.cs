using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class ManaPotion : Consumable
    {
        public int ManaAmount { get; set; }
   
        public ManaPotion(string itName, int itAbility, string itType, string itInfo) : base(itName, itAbility, itType, itInfo)
        {
            ManaAmount = 30;
        }

        public void UsePotion(ICharacter character)
        {
            Console.WriteLine($"{character.Name}은 마나 포션을 사용했습니다.");
        }
    }
}



