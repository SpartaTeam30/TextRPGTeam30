using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class HealingPotion : Consumable
    {
        public int HealAmount { get; set; } 

        public HealingPotion(string itName, int itAbility, string itType, string itInfo) : base(itName, itAbility, itType, itInfo)
        {
            HealAmount = 30;
        }

        public void UsePotion(ICharacter character)
        {
            Console.WriteLine($"{character.Name}은 힐링 포션을 사용했습니다.");
        }
    }
}
