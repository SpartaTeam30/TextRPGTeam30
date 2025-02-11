using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class ManaPotion : Consumable
    {
        public int mana;
<<<<<<< Updated upstream
=======
        public int RestoreAmount { get; set; }

        public ManaPotion(int restoreAmount)
        {
            RestoreAmount = restoreAmount;
        }

        public void UsePotion(ICharacter character)
        {
            Console.WriteLine($"{character.Name}은 마나 포션을 사용했습니다.");
            character.RestoreMana(RestoreAmount); // 캐릭터의 마나를 회복
        }

        public void UseManaPotion(ref int ManaPotionCount, ICharacter character)
        {
            if (ManaPotionCount <= 0)
            {
                Console.WriteLine("마나 포션이 없습니다.");
            }
            else
            {
                UsePotion(character);  // 포션 사용
                ManaPotionCount--; // 마나 포션 수 감소
                Console.WriteLine($"보유한 마나 포션 수: {ManaPotionCount}개");
            }
        }

        public ManaPotion(string itName, int itAbility, string itType, string itInfo) : base(itName, itAbility, itType, itInfo)
        {

        }
>>>>>>> Stashed changes
    }
    // public ManaPotion(string name, int id, string description, int mana);
    // public override void Use()
    // Console.WriteLine($"{Name}을(를) 사용하여 마나를 회복했습니다.");
}



