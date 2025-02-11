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

        public Armor(string _ItName, int _ItAbility, string _ItType, string _ItInfo, int _defense) : base(_ItName, _ItAbility, _ItType, _ItInfo)
        {
            defense = _defense;
        }

        public void Toggle()
        {
            isEquip = !isEquip;
        }               
    }
}
