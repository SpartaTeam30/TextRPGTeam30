using System;

namespace TextRPGTeam30
{
    internal class UtilitySkill : Skill
    {
        public int dAttack;
        public int dDefense;

        public UtilitySkill()
        {

        }

        public UtilitySkill(int cost, int dAttack, int dDefense) : base(cost)
        {
            this.dAttack = dAttack;
            this.dDefense = dDefense;
        }

        public void UseSkill(ICharacter target)
        {
            base.UseSkill();
            
            target.ApplydStat(this);
        }

        public void UseSkill(List<ICharacter> targets, int count)
        {
            base.UseSkill();

            targets.OrderBy(x => new Random().Next());
            for(int i = 0; i < count; i++)
            {
                UseSkill(targets[i]);
            }
        }
    }
}
