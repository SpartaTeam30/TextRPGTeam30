﻿namespace TextRPGTeam30
{
    public class Harden : UtilitySkill
    {
        public string name = "단단해지기";
        public Harden()
        {
            cost = 15;
            dDefense = 1;
            count = 1;
        }

        public Harden(int cost, int dAttack, int dDefense, int count) : base(cost, dAttack, dDefense, count)
        {

        }
    }
}
