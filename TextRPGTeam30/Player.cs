using System;
using System.Numerics;
using System.Runtime.CompilerServices;

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

        public string Name { get; set; }
        public int Level { get; set; }
        public int Defense { get; set; }
        public int Hp { get; set; }
        public int CritRate { get; set; }
        public float Attack { get; set; }
        public int Evasion { get; set; }
        public float DAttack { get; set; }

        public List<Item> inventory = new List<Item>();
        public Player() 
        {
            
           
        }
        
        public Player(string name, int level, int hp, int mp, int gold, 
            int exp, int critRate, float attack, Job job, 
            int defense, int maxHp, int potions, int currentHp)
        {
            this.Name = name;
            this.Level = level;
            this.Hp = hp;
            this.mp = mp;
            this.Attack = attack;
            this.Defense = defense;
            this.CritRate = critRate;

            this.job = job;
            this.exp = exp;
            this.gold = gold;

            // EquippedWeapon = null;
            // EquippedArmor = null;
            this.Attack = attack;

            // 게임 시작 시 체력은 최대 체력으로 설정

            //equipment = new List<Equipable>();  // 장비 가능 리스트
            //consumables = new List<Consumable>(); // 소모품 리스트 
            this.job = job;
            job.ResetStat(this);

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

        public void ResetdStat()
        {
            Attack = 0;
            Defense = 0;
        }

        public void ApplydStat(UtilitySkill s)
        {
            Attack += s.dAttack;
            Defense += s.dDefense;
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
        
        public void UsePotion(Consumable consumable)
        {
            if (consumable is HealingPotion HPotion && HPotion.itemCount > 0)
            {
                int recovery = HPotion.HealAmount;
                int CurrentHp = 100;
                int MaxHp = 100;

                // 회복 후 체력이 최대 체력을 넘지 않도록 함
                CurrentHp = Math.Min(MaxHp, CurrentHp + recovery);
                Console.WriteLine($"회복! 남은 포션: {--HPotion.itemCount}");
                Console.WriteLine($"현재 체력: {CurrentHp}/{MaxHp}");
            }
            else if (consumable is ManaPotion MPotion && MPotion.itemCount > 0)
            {
                int MaxMp = 50;
                int CurrentMp = 50;
                int recovery = MPotion.ManaAmount;
                CurrentMp = Math.Min(MaxMp, CurrentMp + recovery);
                Console.WriteLine($"회복! 남은 포션: {--MPotion.itemCount}");
                Console.WriteLine($"현재 체력: {CurrentMp}/{MaxMp}");
            }             
            else
            {
                Console.WriteLine("포션이 부족합니다.");
            }
        }

        public void DisplayInventory() // 인벤토리 상태
        {
            Console.WriteLine();
            Console.WriteLine("================인벤토리===========================");
            if (inventory.Count == 0) 
            {
                Console.WriteLine("인벤토리가 비어 있습니다.");
            }
            else
            {
                Console.WriteLine("\n인벤토리 아이템 목록\n");
                foreach (var item in inventory)
                {
                    Console.WriteLine($"ID: {item.ID}, 이름: {item.itName}, 설명: {item.itInfo}");
                }
                Console.WriteLine();
                Console.WriteLine("=================================================");       
            }            
        }
    }
}
