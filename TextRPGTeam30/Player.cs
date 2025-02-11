namespace TextRPGTeam30
{
    public class Player : ICharacter
    {
        public int mp;
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
                new Armor("본 헬름", 30, "방어력", "동물의 뼈를 이용하여 악마의 머리 모양으로 깎아놓은 투구.", 10),
                new Weapon("아론다이트", 40, "공격력", "원탁의 기사단 단장 란슬롯이 사용했다는 중세 시대의 검.", 10),
                new Armor("브리간딘 갑옷", 35, "방어력", "부드러운 가죽이나 천 안쪽에 작은 쇠판을 리벳으로 고정시킨 형태의 갑옷.", 15),
                new Armor("건틀렛", 25, "방어력", "철로 만들어진 전투용 장갑.", 5),
                new Armor("이더 부츠", 10, "방어력", "가죽으로 만든 목이 긴 부츠.", 7),
                new Armor("녹색 망토", 20, "방어력", "숲에서 몸을 숨기고 기습하는 데에 최적인 녹색 망토.", 20)
            };
        }

        public Player(string name, Job job)
        {
            this.Name = name;
            this.job = job;
            job.ResetStat(this);

            this.Level = 1;
            this.Hp = 100;
            this.Defense = 5;
            this.mp = 50;
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
            this.mp = mp;
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
                new Armor("본 헬름", 30, "방어력", "동물의 뼈를 이용하여 악마의 머리 모양으로 깎아놓은 투구.", 10),
                new Weapon("아론다이트", 40, "공격력", "원탁의 기사단 단장 란슬롯이 사용했다는 중세 시대의 검.", 10f),
                new Armor("브리간딘 갑옷", 35, "방어력", "부드러운 가죽이나 천 안쪽에 작은 쇠판을 리벳으로 고정시킨 형태의 갑옷.", 15),
                new Armor("건틀렛", 25, "방어력", "철로 만들어진 전투용 장갑.", 5),
                new Armor("이더 부츠", 10, "방어력", "가죽으로 만든 목이 긴 부츠.", 7),
                new Armor("녹색 망토", 20, "방어력", "숲에서 몸을 숨기고 기습하는 데에 최적인 녹색 망토.", 20)
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
            Console.WriteLine($"Lv. {Level}");
            Console.WriteLine($"{Name}, ({job.name})");
            if (equipWeapon != null)
            {
                Console.WriteLine($"공격력 : {Attack - equipWeapon.attack} + {equipWeapon.attack}");
            }
            else
            {
                Console.WriteLine($"공격력 : {Attack}");
            }
            if (equipArmor != null)
            {
                Console.WriteLine($"방어력 : {Defense - equipArmor.defense} + {equipArmor.defense}");
            }
            else
            {
                Console.WriteLine($"방어력 : {Defense}");
            }
            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"Gold : {gold} G");
            if (equipWeapon != null)
            {
                Console.WriteLine($"장착 무기 : {equipWeapon.itName}");
            }
            if (equipArmor != null)
            {
                Console.WriteLine($"장착 방어구 : {equipArmor.itName}");
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
                damage = (float)new Random().NextDouble() * 0.1f * attack + attack;
            }
            int critical_probabiliy = new Random().Next(1, 101);
            bool isCrit = false;

            if (critical_probabiliy <= crit)
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

        public void Equip(Equipable equipable)
        {
            equipable.Toggle();//equipable의 장착 상태 변경

            if (equipable is Weapon)//무기일때
            {
                if (equipWeapon == equipable)//장착해제
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
                    equipWeapon = (Weapon)equipable;
                    this.Attack += equipWeapon.attack;
                }

            }
            else//방어구 일때
            {
                if (equipArmor == equipable)//장착해제
                {
                    this.Defense -= equipArmor.defense;
                    equipArmor = null;
                }
                else
                {//장착
                    if (equipArmor != null)
                    {
                        this.Defense -= equipArmor.defense;
                        equipArmor.Toggle();
                    }
                    equipArmor = (Armor)equipable;
                    this.Defense += equipArmor.defense;

                }
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
                            Console.Write("[E]");
                        }
                        Console.WriteLine($" 이름: {item.itName}, 설명: {item.itInfo}");
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
