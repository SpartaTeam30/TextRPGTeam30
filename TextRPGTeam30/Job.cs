namespace TextRPGTeam30
{
    internal class Job
    {
        public string name;
        private Skill _skill;
        private int _hp;
        private float _attack;
        private float _defense;

        public Job(Skill skill, int hp, float attack, float defense)
        {
            this._skill = skill;
            this._hp = hp;
            this._attack = attack;
            this._defense = defense;
        }

        public void UseSkill()
        {
            _skill.UseSkill();
        }

        public void ResetStat(Player player)
        {
            player.Hp = _hp;
            player.Attack = _attack;
            //player.Defense = _defense;
        }
    }
}