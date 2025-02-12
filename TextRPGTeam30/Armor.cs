using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    public class Armor : Equipable
    {
        public int defense;
        // public Armor(string name, int id, string description, int defenseBonus) 
        // public override void Use()
        // 방어구 사용 로직 Console.WriteLine($"{Name}을(를) 착용했습니다. 방어력 +{DefenseBonus}");

        public Armor(string _ItName, int _ItAbility, string _ItType, string _ItInfo, int price) : base(_ItName, _ItAbility, _ItType, _ItInfo, price)
        {
            defense = itAbility;
        }

        public void Toggle()
        {
            isEquip = !isEquip;
        }               
    }
}
