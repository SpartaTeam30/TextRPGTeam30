namespace TextRPGTeam30
{
    internal class Player : ICharacter
    {
        public int mp;
        public int gold;
        public int exp;
        //public lJob job;
        //public List<Equipable> equipment;
        //public List<Consumable> consumables;
        //public Weapon? equipWeapon;
        //public Armor? equipArmor;
        public int Level { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public int CritRate { get; set; }
        public Character(int critRate)
        {
            CritRate = critRate;
        }
        public float Attack { get; set; }
        public Character(float attack)
        {
            Attack = attack;
        }
        public int CritDamage { get; set; }
        public int Evasion { get; set; }

        public Player(string name, int level, int hp, int mp, int gold, int exp, Job job)
        {
            Name = name;
            Level = level;
            Hp = hp;
            Mp = mp;
            Gold = gold;
            Exp = exp;
            Job = job;
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;

            if (Hp <= 0)
            {
                Hp = 0;
                Dead();  // 체력이 0 이하일 시
            }

        }

        public void Dead()
        {
            Console.WriteLine($"{Name} 가 죽었습니다.");
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}, Level: {Level}, HP: {Hp}");
        }
}
