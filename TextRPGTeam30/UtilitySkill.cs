namespace TextRPGTeam30
{
    public class UtilitySkill : Skill
    {
        public int dAttack;
        public int dDefense;

        public UtilitySkill()
        {

        }

        public UtilitySkill(int cost, int dAttack, int dDefense, int count) : base(cost, count)
        {
            this.dAttack = dAttack;
            this.dDefense = dDefense;
        }

        public void UseSkill(ICharacter target)
        {
            PrintUseSkill(target);
            target.ApplydStat(this);
        }

        public void UseSkill(List<ICharacter> targets)
        {
            foreach (ICharacter target in targets)
            {
                PrintUseSkill(target);
                UseSkill(target);
            }
        }

        public void UseSkill(List<ICharacter> targets, int count)
        {
            targets.OrderBy(x => new Random().Next());
            for(int i = 0; i < count; i++)
            {
                PrintUseSkill(targets[i]);
                UseSkill(targets[i]);
            }
        }

        public virtual void PrintUseSkill(ICharacter target)
        {

        }
    }
}
