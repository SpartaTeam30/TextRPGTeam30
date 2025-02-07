namespace TextRPGTeam30
{
    internal interface ICharacter
    {
        public Character(string name, int level, int hp)
        {
            Name = name;
            Level = level;
            Hp = hp;
        }

        public int Level { get; set; }

        public Character(int level)
        {
            Level = level;
        }
        public void DisplayInfo()
        {
            Console.WriteLine($"Character Level: {Level}");
        }

        public string Name { get; set; }
        public Character(string name)
        {
            Name = name;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Character Name: {Name}");
        }


        public int Hp { get; set; }
        public Character(int hp)
        {
            Hp = hp;
        }

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

        public void TakeDamage(int damage)
        {

        }

        public void Dead()
        {
            Console.WriteLine($"{Name}가 죽었습니다.");
            Hp = 0;
        }






    }
}

