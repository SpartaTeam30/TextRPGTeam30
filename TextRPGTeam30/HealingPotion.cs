using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class HealingPotion : Consumable
    {
        public int RestoreAmount { get; set; }
        public int HealAmount { get; set; }

        public int heal;
<<<<<<< Updated upstream
        // public HealingPotion(string name, int id, string description, int heal);
        // public override void Use()
        //  Console.WriteLine($"{Name}을(를) 사용하여 체력을 회복했습니다.");
=======

        public HealingPotion(int healAmount)
        {
            HealAmount = healAmount;
        }

        public void UsePotion(ICharacter character)
        {
            Console.WriteLine($"{character.Name}은 힐링 포션을 사용했습니다.");
            character.RestoreHealth(RestoreAmount); // 캐릭터의 체력 회복
        }

        public void UseHealingPotion(ref int healingPotionCount, ICharacter character)
        {
            if (healingPotionCount <= 0)
            {
                Console.WriteLine("힐링 포션이 없습니다.");
            }
            else
            {
                UsePotion(character);  // 포션 사용
                healingPotionCount--; // 힐링 포션 수 감소
                Console.WriteLine($"보유한 힐링 포션 수: {healingPotionCount}개");
            }
        }

        public HealingPotion(string itName, int itAbility, string itType, string itInfo) : base(itName, itAbility, itType, itInfo)
        {

        }
>>>>>>> Stashed changes
    }
}
