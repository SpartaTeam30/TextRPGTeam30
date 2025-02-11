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

        public string Name { get; set; }
        public int Level { get; set; }
<<<<<<< Updated upstream
=======
        public int Defense { get; set; }
>>>>>>> Stashed changes
        public int Hp { get; set; }
        public int Defense { get; set; } 
        public int CritRate { get; set; }
        public float Attack { get; set; }
<<<<<<< Updated upstream
        public int Evasion { get; set; }

        public Player(string name, int level, int hp, int mp, float attackl, int defense, int critRate, Job job, int gold, int exp)
=======
        public float DAttack { get; set; }
        public int MaxHp { get; set; }
        public int Evasion { get; set; }
        public int ManaPotionCount { get; set; }
        public int HealingPotionCount { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHp { get; set; }
        public int Potions { get; set; }

        public List<Item> inventory = new List<Item>();
        public Player() 
        {
            inventory = new List<Item>()
            {
                new Item("본 헬름", 30, "방어력", "동물의 뼈를 이용하여 악마의 머리 모양으로 깎아놓은 투구."),
                    new Item("아론다이트", 40, "공격력", "원탁의 기사단 단장 란슬롯이 사용했다는 중세 시대의 검."),
                    new Item("브리간딘 갑옷", 35, "방어력", "부드러운 가죽이나 천 안쪽에 작은 쇠판을 리벳으로 고정시킨 형태의 갑옷."),
                    new Item("건틀렛", 25, "방어력", "철로 만들어진 전투용 장갑."),
                    new Item("이더 부츠", 10, "방어력", "가죽으로 만든 목이 긴 부츠."),
                    new Item("녹색 망토", 20, "방어력", "숲에서 몸을 숨기고 기습하는 데에 최적인 녹색 망토.")
            };
        }
        

        public Player(string name, int level, int hp, int mp, int gold, 
            int exp, int critRate, float attack, Job job, 
            int defense, int maxHp, int potions, int currentHp)

>>>>>>> Stashed changes
        {
            this.Name = name;
            this.Level = level;
            this.Hp = hp;
            this.mp = mp;
            this.Attack = attackl;
            this.Defense = defense;
            this.CritRate = critRate;
<<<<<<< Updated upstream
            this.job = job;
            this.exp = exp;
            this.gold = gold;

            // EquippedWeapon = null;
            // EquippedArmor = null;
=======
            this.Attack = attack;
            this.MaxHp = maxHp;
            this.CurrentHp = currentHp;
            this.Potions = potions;

            // 게임 시작 시 체력은 최대 체력으로 설정
>>>>>>> Stashed changes
            //equipment = new List<Equipable>();  // 장비 가능 리스트
            //consumables = new List<Consumable>(); // 소모품 리스트 
            this.job = job;
            job.ResetStat(this);
<<<<<<< Updated upstream
=======


            inventory = new List<Item>()
            {
                new Item("본 헬름", 30, "방어력", "동물의 뼈를 이용하여 악마의 머리 모양으로 깎아놓은 투구."),
                    new Item("아론다이트", 40, "공격력", "원탁의 기사단 단장 란슬롯이 사용했다는 중세 시대의 검."),
                    new Item("브리간딘 갑옷", 35, "방어력", "부드러운 가죽이나 천 안쪽에 작은 쇠판을 리벳으로 고정시킨 형태의 갑옷."),
                    new Item("건틀렛", 25, "방어력", "철로 만들어진 전투용 장갑."),
                    new Item("이더 부츠", 10, "방어력", "가죽으로 만든 목이 긴 부츠."),
                    new Item("녹색 망토", 20, "방어력", "숲에서 몸을 숨기고 기습하는 데에 최적인 녹색 망토.")
            };
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
=======
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

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======

        public void RestoreHealth(int amount)
        {
            Health += amount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;  // 최대 체력을 넘지 않도록 제한
            }
            Console.WriteLine($"{Name}의 체력이 {Health}/{MaxHealth}로 회복되었습니다.");
        }

        public void ShowHealth() // 체력 확인 메서드
        {
            Console.WriteLine($"{Name}의 체력: {Health}/{MaxHealth}");
        }

        public void UsePotion()
        {
            if (Potions > 0)
            {
                int recovery = 30;
                // 회복 후 체력이 최대 체력을 넘지 않도록 함
                CurrentHp = Math.Min(MaxHp, CurrentHp + recovery);
                Potions--;
                Console.WriteLine($"회복! 남은 포션: {Potions}");
                Console.WriteLine($"현재 체력: {CurrentHp}/{MaxHp}");
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
>>>>>>> Stashed changes
    }
}
