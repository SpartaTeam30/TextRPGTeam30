namespace TextRPGTeam30
{
    internal class Monster
    {
        public float DAttack { get; set; }
        public int DDefense { get; set; }

        public void ResetdStat()
        {
            DAttack = 0;
            DDefense = 0;
        }

        public void ApplydStat(UtilitySkill s)
        {
            DAttack += s.dAttack;
            DDefense += s.dDefense;
        }
    }
}
