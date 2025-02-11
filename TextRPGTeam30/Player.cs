using System;

namespace TextRPGTeam30
{
    public class Player : ICharacter
    {
        public int mp;
        public int maxMp;
        public int gold;
        public int exp;
        public Job job;
        public List<Equipable> equipment { get; set; }
        public List<Consumable> consumables { get; set; }
        public Weapon? equipWeapon { get; set; }
        public Armor? equipArmor { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Defense { get; set; }
        public int DDefense { get; set; }
        public int Hp { get; set; }
        public int MaxHP { get; set; }
        public int CritRate { get; set; }
        public float Attack { get; set; }
        public float DAttack { get; set; }
        public int Evasion { get; set; }
        public int JobType { get; set; }

        public List<Item> inventory = new List<Item>();

        public Player()
        {
            inventory = new List<Item>()
            {
                new Armor("본 헬름", 30, "방어력", "동물의 뼈를 이용하여 악마의 머리 모양으로 깎아놓은 투구.", 10, 100),
                new Weapon("아론다이트", 40, "공격력", "원탁의 기사단 단장 란슬롯이 사용했다는 중세 시대의 검.", 10, 100),
                new Armor("브리간딘 갑옷", 35, "방어력", "부드러운 가죽이나 천 안쪽에 작은 쇠판을 리벳으로 고정시킨 형태의 갑옷.", 15, 100),
                new Armor("건틀렛", 25, "방어력", "철로 만들어진 전투용 장갑.", 5, 100),
                new Armor("이더 부츠", 10, "방어력", "가죽으로 만든 목이 긴 부츠.", 7, 100),
                new Armor("녹색 망토", 20, "방어력", "숲에서 몸을 숨기고 기습하는 데에 최적인 녹색 망토.", 20, 100)
            };
        }

        public Player(string name, Job job)
        {
            this.Name = name;
            this.job = job;
            job.ResetStat(this);

            this.Level = 1;
            this.Hp = 100;
            MaxHP = Hp;
            this.Defense = 5;
            this.mp = 50;
            maxMp = mp;
            this.gold = 100;
            this.exp = 0;
            this.CritRate = 15;
            this.Attack = 10;
            equipment = new List<Equipable>();  // 장비 가능 리스트
            consumables = new List<Consumable>(); // 소모품 리스트 
            this.Evasion = 10;
            this.equipWeapon = null;
            this.equipArmor = null;
            job.ResetStat(this);
        }

        public Player(string name, int level, int hp, int mp, int gold, int exp, int critRate, float attack, int jobType, int defense)
        {
            this.Name = name;
            this.Level = level;
            this.Hp = hp;
            MaxHP = Hp;
            this.mp = mp;
            maxMp = mp;
            this.gold = gold;
            this.exp = exp;
            this.CritRate = critRate;
            this.Attack = attack;
            this.Defense = defense;
            this.JobType = jobType; //타입 0전사 1마법사
            this.job = ConvertJob(JobType);  // 직업 변환
            equipment = new List<Equipable>();  // 장비 가능 리스트
            consumables = new List<Consumable>(); // 소모품 리스트 
            this.equipWeapon = null;
            this.equipArmor = null;
            job.ResetStat(this);

            inventory = new List<Item>()
            {
                new Armor("본 헬름", 30, "방어력", "동물의 뼈를 이용하여 악마의 머리 모양으로 깎아놓은 투구.", 10, 100),
                new Weapon("아론다이트", 40, "공격력", "원탁의 기사단 단장 란슬롯이 사용했다는 중세 시대의 검.", 10, 100),
                new Armor("브리간딘 갑옷", 35, "방어력", "부드러운 가죽이나 천 안쪽에 작은 쇠판을 리벳으로 고정시킨 형태의 갑옷.", 15, 100),
                new Armor("건틀렛", 25, "방어력", "철로 만들어진 전투용 장갑.", 5, 100),
                new Armor("이더 부츠", 10, "방어력", "가죽으로 만든 목이 긴 부츠.", 7, 100),
                new Armor("녹색 망토", 20, "방어력", "숲에서 몸을 숨기고 기습하는 데에 최적인 녹색 망토.", 20, 100)
            };

            this.Defense = defense;
            this.JobType = jobType; //타입 0전사 1마법사
            this.job = ConvertJob(jobType);  // 직업 변환
            job.ResetStat(this);
        }

        //직업 변환
        private Job ConvertJob(int jobType)
        {
            return jobType == 0 ? new Warrior() : new Mage();
        }

        public void DisplayStatus()
        {
            Console.Clear();
            GameManager.PrintColoredLine("상태 보기\n",ConsoleColor.Yellow);
            Console.Write("Lv. ");
            GameManager.PrintColoredLine($"{Level}", ConsoleColor.Magenta);
            Console.Write("이름 : ");
            GameManager.PrintColored($"{Name}",ConsoleColor.Magenta);
            Console.WriteLine($", ({job.name})");
            if (equipWeapon != null)
            {
                Console.Write($"공격력 : ");
                GameManager.PrintColoredLine($"{Attack - equipWeapon.attack} (+{equipWeapon.attack})", ConsoleColor.Magenta);
            }
            else
            {
                Console.Write("공격력 : ");
                GameManager.PrintColoredLine($"{Attack}", ConsoleColor.Magenta);
            }
            if (equipArmor != null)
            {
                Console.Write("방어력 : ");
                GameManager.PrintColoredLine($"{Defense - equipArmor.defense} (+{equipArmor.defense})", ConsoleColor.Magenta);
            }
            else
            {
                Console.Write($"방어력 : ");
                GameManager.PrintColoredLine($"{Defense}", ConsoleColor.Magenta);
            }
            Console.Write($"체력 : ");
            GameManager.PrintColoredLine($"{Hp}/{MaxHP}", ConsoleColor.Magenta);
            Console.Write($"마나 : ");
            GameManager.PrintColoredLine($"{mp}/{maxMp}", ConsoleColor.Magenta);
            Console.Write($"Gold : ");
            GameManager.PrintColoredLine($"{gold} G", ConsoleColor.Magenta);
            Console.Write("장착 무기 : ");
            if (equipWeapon != null)
            {
                GameManager.PrintColoredLine($"{equipWeapon.itName}", ConsoleColor.Magenta);
            }
            else
            {
                Console.WriteLine("없음");
            }

            Console.Write("장착 방어구 : ");
            if (equipArmor != null)
            {
                
                GameManager.PrintColoredLine($"{equipArmor.itName}", ConsoleColor.Magenta);
            }
            else
            {
                Console.WriteLine("없음");
            }
            Console.WriteLine("0. 돌아가기");
            GameManager.CheckWrongInput(out int select, 0, 0);
            return;
        }

        public void TakeDamage(float attack, int crit, bool isSkill = false)
        {
            int evasion_probability = new Random().Next(1, 101);
            if (evasion_probability <= Evasion && isSkill == false)
            {
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
                damage = (new Random().NextSingle() * 2 - 1) * attack + attack;
            }

            int critical_probabiliy = new Random().Next(1, 101);
            bool isCrit = false;
            if (critical_probabiliy <= crit)
            {
                isCrit = true;
                damage *= 1.6f;
            }

            damage *= 200f / (200 + Defense);

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

        public void LevelUp(int e)
        {
            int requiredAmount = Level == 1 ? 10 : Level * 5 + 25;
            exp += e;

            if (exp >= requiredAmount)
            {
                int levelAdd = exp / requiredAmount;

                Console.WriteLine($"축하합니다! 레벨이 {levelAdd} 올랐습니다!");
                Console.WriteLine("체력과 마나가 회복되었습니다!");
                
                Level += levelAdd;
                exp = e % requiredAmount;
                Attack += levelAdd * 0.5f;
                Defense += levelAdd * 1;
                Hp = MaxHP += levelAdd * 5;
                mp = maxMp += levelAdd * 2;

                Thread.Sleep(500);
            }
        }

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

        //public void attack(float Attack)
        //{
        //    //   if (equipWeapon != null)
        //    // {
        //    //      Console.WriteLine($"{Name} 공격 시 {equipWeapon.Name}, Power: {equipWeapon.AttackPower}");
        //    //  }
        //    //  else
        //    {
        //        Console.WriteLine($"{Name} 가 공격합니다!");
        //    }
        //}

        public void Dead()
        {
            Console.WriteLine($"{Name} 가 죽었습니다.");
        }

        //public void DisplayInfo()
        //{
        //    Console.WriteLine($"Name: {Name}, Level: {Level}, HP: {Hp}");
        //}

        public void EquipWeapon(Weapon weapon)
        {
            if (equipWeapon == weapon)//장착해제
            {
                this.Attack -= equipWeapon.attack;
                equipWeapon = null;
            }
            else//장착
            {
                if (equipWeapon != null)
                {
                    this.Attack -= equipWeapon.attack;
                    equipWeapon.Toggle();
                }
                equipWeapon = weapon;
                this.Attack += equipWeapon.attack;
            }
        }

        public void EquipArmor(Armor armor)
        {
            if (equipArmor == armor)//장착해제
            {
                this.Defense -= equipArmor.defense;
                equipArmor = null;
            }
            else//장착
            {
                if (equipArmor != null)
                {
                    this.Defense -= equipArmor.defense;
                    equipArmor.Toggle();
                }
                equipArmor = armor;
                this.Defense += equipArmor.defense;
            }
        }

        public void Equip(Equipable equipable)
        {
            equipable.Toggle();//equipable의 장착 상태 변경

            if (equipable is Weapon)//무기일때
            {
                EquipWeapon((Weapon) equipable);
            }
            else//방어구 일때
            {
                EquipArmor((Armor) equipable);
            }
        }

        public void DisplayInventory() // 인벤토리 상태
        {
            int num = 0;
            Console.Clear();
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
                    Console.Write($"{++num}. ");
                    if (item is Equipable equipable)
                    {
                        if (equipable.isEquip)
                        {
                            GameManager.PrintColored("[E]", ConsoleColor.Magenta);
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                        Console.WriteLine($"이름: {item.itName}, 설명: {item.itInfo}");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("=================================================");
            }

            Console.WriteLine("0. 돌아가기");
            GameManager.CheckWrongInput(out int select, 0, num);
            if (select == 0)
            {
                return;
            }

            Equip((Equipable)inventory[select - 1]);
            DisplayInventory();
        }

        public bool UseGold(int price)
        {
            if (price > gold)
            {
                Console.WriteLine("Gold가 부족합니다.");
                Thread.Sleep(500);
                return false;
            }
            else
            {
                gold -= price;
                Console.WriteLine($"{price} Gold를 지불했습니다.");
                Thread.Sleep(500);
                return true;
            }
        }
    }
}
