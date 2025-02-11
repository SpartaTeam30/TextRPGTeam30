namespace TextRPGTeam30
{
    public class DungeonManager
    {
        int stage;//현재 스테이지
        int deadMonster;//죽은 몬스터
        Player player;//플레이어
        Dungeon? dungeon;//현재 던전
        List<Monster> monsters;//출현가능한 몬스터
        List<Monster> bossMonsters;//보스 몬스터

        public DungeonManager(Player player)//생성자 함수
        {
            this.player = player;
            stage = player.Stage;
            monsters = new List<Monster>()
            {
                new Monster("슬라임", 1, 10, 3), new Monster("고블린", 1, 15, 5), new Monster("코볼트", 1, 20, 7), new Monster("고스트", 1, 15, 13),
                new Monster("스켈레톤", 1, 35, 8), new Monster("리치", 1, 40, 15)
            };
            bossMonsters = new List<Monster>()
            {
                new Monster("트롤", 1 , 200, 10), new Monster("오우거", 1, 130, 25)
            };
        }

        public void CreateDungeon()//던전 생성
        {
            //스테이지에 따라 출현가능한 몬스터의 범위가 달라짐
            int maxRangeMonster = 3 + stage / 5;
            int minRangeMonster = 1 + stage / 5;
            int randomBoss = new Random().Next(0, bossMonsters.Count);
            if (maxRangeMonster >= monsters.Count)
            {
                minRangeMonster = maxRangeMonster - 3;
                maxRangeMonster = monsters.Count - 1;
            }
            //던전 생성
            dungeon = new Dungeon(player, stage, monsters.GetRange(minRangeMonster, maxRangeMonster), bossMonsters[randomBoss]);
        }

        public void DungeonStart()//던전 시작화면
        {
            CreateDungeon();//던전 생성
            deadMonster = 0;//죽은 몬스터 수 초기화

            while (true)
            {
                AttackMenu();//플레이어의 공격

                if (deadMonster == dungeon.monsters.Count)//죽은 몬스터 수와 던전의 몬스터수가 같을 때
                {
                    dungeon.DungeonSuccess();//던전클리어
                    GameSaveManager saveManager = new GameSaveManager();
                    saveManager.SaveDungeonClearData(player);
                    break;
                }

                MonsterAttack();//몬스터의 공격

                if (player.Hp <= 0)//플레이어 사망 여부
                {
                    stage = 1;//스테이지 초기화
                    dungeon.DungeonFail();//던전 실패
                    return;
                }
            }

            //출력
            PrintReward();
        }

        public void PrintTitle()
        {
            Console.Clear();
            if (stage % 20 == 0)
            {
                GameManager.PrintColoredLine("\nBoss Battle!!\n", ConsoleColor.Yellow);
            }
            else
            {
                GameManager.PrintColoredLine("\nBattle!!\n", ConsoleColor.Yellow);
            }
        }

        public void PrintDungeonUI()//던전 정보 출력
        {
            PrintTitle();

            foreach (Monster monster in dungeon.monsters)
            {
                if (monster.Hp > 0)
                {
                    PrintMonster(monster);
                }
                else
                {
                    GameManager.PrintColoredLine($"Lv.{monster.Level} {monster.Name} Dead", ConsoleColor.DarkGray);
                }
            }

            PrintPlayer();
        }

        public void PrintPlayer()
        {
            Console.WriteLine("\n[내정보]");
            Console.Write($"Lv.");
            GameManager.PrintColored($"{player.Level}", ConsoleColor.Magenta);
            Console.WriteLine($"  {player.Name} ({player.job.name})");
            Console.Write("HP ");
            GameManager.PrintColoredLine($"{player.Hp}/{player.MaxHP}", ConsoleColor.Magenta);
            Console.Write("MP ");
            GameManager.PrintColoredLine($"{player.mp}/{player.maxMp}\n", ConsoleColor.Magenta);
        }

        public void PrintMonster(Monster monster)
        {
            if (monster.isUnique == true)
            {
                Console.Write("[U] ");
            }
            Console.Write("Lv.");
            GameManager.PrintColored($"{monster.Level}", ConsoleColor.Magenta);
            Console.Write($" {monster.Name} HP ");
            GameManager.PrintColoredLine($"{monster.Hp}", ConsoleColor.Magenta);
        }

        public void PrintReward()
        {
            Console.Clear();
            GameManager.PrintColoredLine("\nBattle!! - Result\n\n", ConsoleColor.Yellow);
            GameManager.PrintColoredLine("Victory\n", ConsoleColor.Green);
            Console.Write("던전에서 몬스터 ");
            GameManager.PrintColored($"{dungeon.monsterNum}", ConsoleColor.Magenta);
            Console.WriteLine("마리를 잡았습니다.\n");
            Console.WriteLine("\n[캐릭터 정보]");
            Console.Write($"Lv.");
            GameManager.PrintColored($"{player.Level}", ConsoleColor.Magenta);
            Console.WriteLine($"  {player.Name} ()");
            Console.Write("HP ");
            GameManager.PrintColoredLine($"{player.Hp}/{player.MaxHP}", ConsoleColor.Magenta);
            Console.Write("MP ");
            GameManager.PrintColoredLine($"{player.mp}/{player.maxMp}\n", ConsoleColor.Magenta);
        }

        public void AttackMenu()
        {
            PrintDungeonUI();

            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");

            GameManager.CheckWrongInput(out int con, 1, 2);

            switch (con)
            {
                case 1:
                    SelectTarget();
                    break;
                case 2:
                    SkillMenu();
                    break;
            }
        }

        public void SkillMenu()
        {
            PrintDungeonUI();

            //스킬 출력
            Console.WriteLine("0. 취소");
            GameManager.CheckWrongInput(out int con, 0, 2);

            switch (con)
            {
                case 0:
                    AttackMenu();
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }

        public void SelectTarget()
        {
            PrintTitle();

            int num = 0;

            foreach (Monster monster in dungeon.monsters)
            {
                if (monster.Hp > 0)
                {
                    GameManager.PrintColored($"{++num} ", ConsoleColor.Cyan);
                    PrintMonster(monster);
                }
                else
                {
                    GameManager.PrintColoredLine($"{++num} Lv.{monster.Level} {monster.Name} Dead", ConsoleColor.DarkGray);
                }
            }
            PrintPlayer();
            Console.WriteLine("0. 취소");
            Monster target;
            int con;

            while (true)
            {
                GameManager.CheckWrongInput(out con, 0, num);

                if (con == 0)
                {
                    AttackMenu();
                    return;
                }

                target = dungeon.monsters[con - 1];

                if (target.Hp <= 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                else
                {
                    break;
                }
            }

            int targetHp = target.Hp;

            PrintTitle();
            Console.WriteLine($"{player.Name}의 공격!");

            target.TakeDamage(player.Attack, player.CritRate, false);

            Console.Write("Lv.");
            GameManager.PrintColored($"{player.Level}", ConsoleColor.Magenta);
            Console.WriteLine($" {target.Name}");
            Console.Write("HP ");
            GameManager.PrintColored($"{targetHp}", ConsoleColor.Magenta);
            Console.Write(" -> ");

            if (target.Hp > 0)
            {
                GameManager.PrintColored($"{target.Hp}", ConsoleColor.Magenta);
            }
            else
            {
                deadMonster++;
                Console.WriteLine("Dead");
            }

            Console.WriteLine("\n0. 다음\n");
            GameManager.CheckWrongInput(out con, 0, 0);

            if (target.Hp <= 0)
            {
                target.Dead();
            }
        }

        public void MonsterAttack()
        {
            foreach (Monster monster in dungeon.monsters)
            {
                if (monster.Hp > 0)
                {
                    int playerHp = player.Hp;

                    PrintTitle();
                    Console.Write($"Lv. ");
                    GameManager.PrintColored($"{monster.Level}", ConsoleColor.Magenta);
                    Console.WriteLine($" {monster.Name} 의 공격");

                    player.TakeDamage(monster.Attack, monster.CritRate, false);

                    if (player.Hp <= 0)
                    {
                        return;
                    }

                    Console.WriteLine($"Lv. {player.Level} {player.Name}");
                    Console.Write("HP ");
                    GameManager.PrintColored($"{playerHp}", ConsoleColor.Magenta);
                    Console.Write(" -> ");
                    GameManager.PrintColored($"{player.Hp}\n", ConsoleColor.Magenta);
                    Console.WriteLine("0. 다음\n");
                    GameManager.CheckWrongInput(out int con, 0, 0);
                }
            }
        }
    }
}
