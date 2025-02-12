using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace TextRPGTeam30
{
    public class Player : ICharacter
    {
        public int mp;
        public int maxMp;
        public int gold;
        public int exp;
        public Job job;
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
        public int Stage { get; set; }

        public List<Item> inventory = new List<Item>();

        public Player()
        {
        }

        public Player(string name, Job job)
        {
            this.Name = name;
            this.job = job;
            this.Level = 1;
            this.Hp = 100;
            MaxHP = 100;
            this.Defense = 5;
            this.mp = 50;
            maxMp = 50;
            this.gold = 100;
            this.exp = 0;
            this.CritRate = 15;
            this.Attack = 10;
            this.Evasion = 10;
            this.equipWeapon = null;
            this.equipArmor = null;
            this.DAttack = 0;
            this.DDefense = 0;
            job.ResetStat(this);
        }

        public Player(string name, int level, int hp, int maxHp, int mp, int maxMp, int gold, int exp, int critRate, float attack, int jobType, int defense, int stage = 1)
        {
            this.Name = name;
            this.Level = level;
            this.Hp = hp;
            this.MaxHP = maxHp;
            this.mp = mp;
            this.maxMp = maxMp;
            this.gold = gold;
            this.exp = exp;
            this.CritRate = critRate;
            this.Attack = attack;
            this.Defense = defense; 
            this.Stage = stage;
            this.JobType = jobType; //타입 0전사 1마법사
            this.job = ConvertJob(jobType, hp, attack, defense);//직업변환
            this.equipWeapon = null;
            this.equipArmor = null;
            this.DAttack = 0;
            this.DDefense = 0;
            job.ResetStat(this);
            inventory = new List<Item>()
            {
            };
        }

        //직업 변환
        private Job ConvertJob(int jobType, int savedHp, float savedAttack, int savedDefense)
        {
            return jobType == 0
                ? new Warrior(null, savedHp, savedAttack, savedDefense)
                : new Mage(null, savedHp, savedAttack, savedDefense);
        }


        public void DisplayStatus()
        {
            Console.Clear();
            GameManager.PrintColoredLine("상태 보기\n",ConsoleColor.Yellow);
            Console.Write("Lv. ");
            GameManager.PrintColoredLine($"{Level} ({exp} / {Level * 5 + 5})", ConsoleColor.Magenta);
            Console.Write("이름 : ");
            GameManager.PrintColored($"{Name}",ConsoleColor.Magenta);
            Console.WriteLine($", ({job.name})");
            if (equipWeapon != null)
            {
                Console.Write($"공격력 : ");
                GameManager.PrintColoredLine($"{Attack} (+{equipWeapon.attack})", ConsoleColor.Magenta);
            }
            else
            {
                Console.Write("공격력 : ");
                GameManager.PrintColoredLine($"{Attack}", ConsoleColor.Magenta);
            }
            if (equipArmor != null)
            {
                Console.Write("방어력 : ");
                GameManager.PrintColoredLine($"{Defense} (+{equipArmor.defense})", ConsoleColor.Magenta);
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

            damage *= 200f / (200 + GetDefense());

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
            int requiredAmount = Level * 5 + 5;
            exp += e;

            if (exp >= requiredAmount)
            {
                int levelAdd = exp / requiredAmount;

                Console.WriteLine($" 축하합니다! 레벨이 {levelAdd} 올랐습니다!");
                Console.WriteLine(" 체력과 마나가 회복되었습니다!");

                Level += levelAdd;
                exp = e % requiredAmount;
                QuestManager.Instance.OnPlayerLevelUp();

                // ✅ 레벨업 시에만 최대 체력 & 최대 마나 증가
                MaxHP += levelAdd * 5;
                maxMp += levelAdd * 2;

                Hp = MaxHP;  // ✅ 레벨업 후 체력 회복
                mp = maxMp;  // ✅ 레벨업 후 마나 회복
                Attack += levelAdd * 0.5f;
                Defense += levelAdd * 1;

                // ✅ 레벨업할 때만 `MaxHP`, `MaxMP` 업데이트 저장
                GameSaveManager saveManager = new GameSaveManager();
                saveManager.SaveMaxHPMP(this);

                Console.WriteLine($"새로운 상태: HP={Hp}/{MaxHP}, MP={mp}/{maxMp}");
                Thread.Sleep(1000);
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

        public void Dead()
        {
            Console.WriteLine($"{Name} 가 죽었습니다.");
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (equipWeapon == weapon) // 장착 해제
            {
                equipWeapon = null;
            }
            else // 장착
            {
                if (equipWeapon != null)
                {
                    equipWeapon.Toggle();
                }
                equipWeapon = weapon;

                // 🔥 퀘스트 진행 체크
                QuestManager.Instance.OnWeaponEquipped();
            }
        }


        public void EquipArmor(Armor armor)
        {
            if (equipArmor == armor) // 장착 해제
            {
                equipArmor = null;
            }
            else // 장착
            {
                if (equipArmor != null)
                {
                    equipArmor.Toggle();
                }
                equipArmor = armor;

                // 🔥 퀘스트 진행 체크
                QuestManager.Instance.OnArmorEquipped();
            }
        }


        public void Equip(Equipable equipable)
        {
            if (equipable == null)
            {
                Console.WriteLine("선택한 아이템을 장착할 수 없습니다.");
                return;
            }

            equipable.Toggle(); //장비 상태 변경

            if (equipable is Weapon weapon)
            {
                EquipWeapon(weapon);
            }
            else if (equipable is Armor armor)
            {
                EquipArmor(armor);
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

                    if (item is Equipable)
                    {
                        // 착용 여부 확인 후 [E] 표시
                        if (equipWeapon == item || equipArmor == item)
                        {
                            GameManager.PrintColored("[E] ", ConsoleColor.Magenta);
                        }
                        else
                        {
                            Console.Write("    ");
                        }
                        Console.WriteLine($"이름: {item.itName}({item.itType} + {item.itAbility}), 설명: {item.itInfo}");
                    }
                    else if (item is Consumable consumable)
                    {
                        Console.WriteLine($"    이름: {item.itName}, 남은 갯수: {consumable.itemCount}, 설명: {item.itInfo}");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("=================================================");
            }

            while (true) // 유효한 선택을 받을 때까지 반복
            {
                Console.WriteLine("0. 돌아가기");
                GameManager.CheckWrongInput(out int select, 0, num);

                if (select == 0)
                {
                    return;
                }

                Item selectedItem = inventory[select - 1];

                // 선택한 아이템이 장비 가능한 경우에만 캐스팅
                if (selectedItem is Equipable equipableItem)
                {
                    Equip(equipableItem);
                    break; // 정상적으로 장비했으면 루프 탈출
                }
                else if (selectedItem is Consumable consumableItem)
                {
                    UsePotion(consumableItem);
                    break;
                }
                else
                {
                    Console.WriteLine("이 아이템은 장비할 수 없습니다. 다시 선택하세요.");
                }
            }

            DisplayInventory(); // 인벤토리 화면 갱신
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
        
        public void UsePotion(Consumable consumable)
        {
            if (consumable is HealingPotion HPotion && HPotion.itemCount > 0)
            {
                int recovery = HPotion.HealAmount;
                // 회복 후 체력이 최대 체력을 넘지 않도록 함
                Hp = Math.Min(MaxHP, Hp + recovery);
                Console.WriteLine($"회복! 남은 포션: {--HPotion.itemCount}");
                Console.WriteLine($"현재 체력: {Hp}/{MaxHP}");
                if(HPotion.itemCount == 0)
                {
                    inventory.Remove(HPotion);
                }
            }
            else if (consumable is ManaPotion MPotion && MPotion.itemCount > 0)
            {
     
                int recovery = MPotion.ManaAmount;
                mp = Math.Min(maxMp, mp + recovery);
                Console.WriteLine($"회복! 남은 포션: {--MPotion.itemCount}");
                Console.WriteLine($"현재 체력: {mp}/{maxMp}");
                if (MPotion.itemCount == 0)
                {
                    inventory.Remove(MPotion);
                }
            }             
            else
            {
                Console.WriteLine("포션이 부족합니다.");
            }
        }
        
        public float GetAttack()
        {
            if (equipWeapon != null)
            {
                return Attack + equipWeapon.attack + DAttack;
            }
            return Attack + DAttack;
        }

        public float GetDefense()
        {
            if (equipArmor != null)
            {
                return Defense + equipArmor.defense + DDefense;
            }
            return Defense + DDefense;
        }
    }
}
