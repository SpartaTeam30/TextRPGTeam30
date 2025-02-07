using System;
using System.Numerics;

namespace TextRPGTeam30
{
    internal class Player : ICharacter
    {
        public int mp;
        public int gold;
        public int exp;
        public Job job;
        // public List<IEquipable> equipment { get; set; } 
       // public List<Consumable> consumables { get; set; } 
       // public Weapon equipWeapon { get; set; }
      // public Armor equipArmor { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public int Defense { get; set; }
        public int Hp { get; set; }
        public int CritRate { get; set; }
        public float Attack { get; set; }
        public int CritDamage { get; set; }
        public int Evasion { get; set; }

        public Player(string name, int level, int hp, int mp, int gold, int exp, int critRate, float attack, Job job, int defense)
        {
            this.Name = name;
            this.Level = level;
            this.Hp = hp;
            this.Defense = defense;
            this.job = job;
            this.mp = mp;
            this.gold = gold;
            this.exp = exp;
            this.CritRate = critRate;
            this.Attack = attack;
            //equipment = new List<Equipable>();  // 장비 가능 리스트
            //consumables = new List<Consumable>(); // 소모품 리스트 
            this.job = job;
            job.ResetStat(this);
        }
        public void DisplayStatus()
        {
            Console.WriteLine($"Lv. {Level} {Name} ({job.name})");
            Console.WriteLine($"공격력 : {Attack}");
            Console.WriteLine($"방어력 : {Defense}");
            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"Gold : {gold} G");

            Console.WriteLine("Lv. 01");
            Console.WriteLine("Chad (전사)");
            Console.WriteLine("공격력 : 10");
            Console.WriteLine("방어력 : 5");
            Console.WriteLine("체력 : 100");
            Console.WriteLine("Gold : 1500 G");
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

        public void attack(float Attack)
        {
         //   if (equipWeapon != null)
         // {
         //      Console.WriteLine($"{Name} 공격 시 {equipWeapon.Name}, Power: {equipWeapon.AttackPower}");
         //  }
          //  else
            {
                Console.WriteLine($"{Name} 가 공격합니다!");
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

        public void EquipWeapon (string name, int attackPower)
        {
            Name = name;
        }

        public void EquipArmor (string name)
        {
            Name = name;
        }

        public void Equip()
        {
            Player EquipWeapon = this;
            Console.WriteLine($"{Name}를 장착했습니다.");
        }
    }
}
