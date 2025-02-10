﻿namespace TextRPGTeam30
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
            Evasosion = 10;
        }

        public Monster(Monster other) 
        {
            this.Name = other.Name;
            this.Level = other.Level;
            this.Hp = other.Hp;
            this.CritRate = other.CritRate;
            this.Attack = other.Attack;
            this.CritDamage = other.CritDamage;
            this.Evasosion = other.Evasosion;
        }

        public void TakeDamage(float attack, int crit, bool isSkill)
        {
            int evasion_probability = new Random().Next(1, 101);
            
            if (evasion_probability <= Evasosion && isSkill == false) {
                Console.Write("Lv.");
                GameManager.PrintColored($"{Level}", ConsoleColor.Magenta);
                Console.WriteLine($" {Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.\n");
                return;
            }
            float damage;
            if (isSkill)
            {
                damage = attack;
            }
            else
            {
                damage = (float)new Random().NextDouble() * 0.1f * attack + attack;
            }
            int critical_probabiliy = new Random().Next(1, 101);
            bool isCrit = false;

            if(critical_probabiliy <= crit)
            {
                isCrit = true;
                damage *= 1.6f;
            }

            int finalDamage = (int)Math.Round(damage);

            Hp -= finalDamage;

            Console.Write("Lv.");
            GameManager.PrintColored($"{Level}", ConsoleColor.Magenta);
            Console.Write($" {Name} 을(를) 맞췄습니다. [데미지 : ");
            GameManager.PrintColored($"{finalDamage}", ConsoleColor.Magenta);
            Console.Write("]");

            if (isCrit)
            {
                Console.Write(" - ");
                GameManager.PrintColored("치명타", ConsoleColor.Black, ConsoleColor.Yellow);
                Console.WriteLine(" 공격!!\n");
            }
            else
            {
                Console.WriteLine("\n");
            }
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
