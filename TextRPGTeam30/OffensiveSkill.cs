﻿namespace TextRPGTeam30
{
    public class OffensiveSkill : Skill
    {
        public float damageModifier = 1f;

        public OffensiveSkill()
        {

        }

        public OffensiveSkill(string description, float damageModifier, int cost, int count) : base(description, cost, count)
        {
            this.damageModifier = damageModifier;
        }

        public float UseSkill(float damage)
        {
            base.UseSkill();

            return (int)(damageModifier * damage);
        }
    }
}
