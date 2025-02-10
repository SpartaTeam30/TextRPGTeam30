namespace TextRPGTeam30
{
    internal class OffensiveSkill : Skill
    {
        public float damageModifier = 1f;

        public OffensiveSkill()
        {

        }

        public OffensiveSkill(float damageModifier, int cost) : base(cost)
        {
            this.damageModifier = damageModifier;
        }

        public int UseSkill(int damage)
        {
            base.UseSkill();

            return (int)(damageModifier * damage);
        }
    }
}
