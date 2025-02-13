using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TextRPGTeam30
{
    public class Armor : Equipable
    {
        public Armor(string Name, int ItAbility, string ItType, string ItInfo, int price) : base(Name, ItAbility, ItType, ItInfo, price)
        {

        }

        // public Armor(string name, int id, string description, int defenseBonus)
        // public override void Use()
        // 방어구 사용 로직 Console.WriteLine($"{Name}을(를) 착용했습니다. 방어력 +{DefenseBonus}");
        public void Toggle()
        {
            isEquip = !isEquip;
        }
    }
}