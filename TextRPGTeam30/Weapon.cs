﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal class Weapon : Equipable
    {
        public Weapon(string _ItName, int _ItAbility, string _ItType, string _ItInfo) : base(_ItName, _ItAbility, _ItType, _ItInfo)
        {

        }

        public float Attack;

        // public Weapon()
        
        public void Toggle()
        {

        }
    }
}
