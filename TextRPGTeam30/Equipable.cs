﻿namespace TextRPGTeam30
{
    public class Equipable : Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        // public int Price { get; set; }
        public string IDescription { get; set; } // 변수 설명용

        public bool isEquip;

        public Equipable(string Name, int ItAbility, string ItType, string ItInfo) : base(Name, ItAbility, ItType, ItInfo)
        {

        }

        public void Toggle(Player player)
        {

        }
    }
}

