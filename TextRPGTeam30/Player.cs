using System;
using System.Numerics;

namespace TextRPGTeam30
{
    public class Player : ICharacter
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
        public int Evasion { get; set; }
        public int JobType { get; set; }

        public Player()
        {

        }
        public Player(string name, int level, int hp, int mp, int gold, int exp, int critRate, float attack, int jobType, int defense)
        {
            this.Name = name;
            this.Level = level;
            this.Hp = hp;
            this.mp = mp;
            this.gold = gold;
            this.exp = exp;
            this.CritRate = critRate;
            this.Attack = attack;
            this.Defense = defense; 
            this.JobType = jobType; //타입 0전사 1마법사
            this.job = ConvertJob(jobType);  // 직업 변환
        }

        //직업 변환
        private Job ConvertJob(int jobType)
        {
            return jobType == 0 ? new Warrior() : new Mage();
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"Lv. {Level}");
            Console.WriteLine($"{Name}, ({job.name})");
            Console.WriteLine($"공격력 : {Attack}");
            Console.WriteLine($"방어력 : {Defense}");
            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"Gold : {gold} G");
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

        //public void DisplayInfo()
        //{
        //    Console.WriteLine($"Name: {Name}, Level: {Level}, HP: {Hp}");
        //}

        public void EquipWeapon(string name, int attackPower)
        {
            Name = name;
        }

        public void EquipArmor(string name)
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
