namespace TextRPGTeam30
{
    internal class Monster : ICharacter
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public int CritRate { get; set; }
        public float Attack { get; set; }
        public int CritDamage { get; set; }
        public int Evasosion { get; set; }

        public Monster(string _name, int _level, int _hp, int _attack)
        {
            Name = _name;
            Level = _level;
            Hp = _hp;
            Attack = _attack;
        }

        public void TakeDamage(float attack)
        {

        }
        public void SetLevel(int level) 
        {
            this.Level = level;
            Attack += level * 0.5f;
        }
        public void Dead()
        {

        }
    }
}
