using System.Threading;

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
                new Monster("마왕", 1 , 200, 10)/*, new Monster("오우거", 1, 130, 25)*/
            };
        }

        public void CreateDungeon()//던전 생성
        {
            //스테이지에 따라 출현가능한 몬스터의 범위가 달라짐
            int minRangeMonster = 0 + stage / 5;

            int randomBoss = new Random().Next(0, bossMonsters.Count);
            if (minRangeMonster + 3 >= monsters.Count)
            {
                minRangeMonster -= 3;
            }
            //던전 생성
            dungeon = new Dungeon(player, stage, monsters.GetRange(minRangeMonster, 3), bossMonsters[randomBoss]);
        }

        public void DungeonStart()//던전 시작화면
        {
            SoundManager.Instance.PlaySound("dungeonBGM");
            CreateDungeon();//던전 생성
            deadMonster = 0;//죽은 몬스터 수 초기화


            if (stage == 20) GameManager.PrintMeetStory();

                while (true)
                {
                    AttackMenu();//플레이어의 공격

                    if (deadMonster == dungeon.monsters.Count)//죽은 몬스터 수와 던전의 몬스터수가 같을 때
                    {
                        if (stage == 10) GameManager.PrintRememberStory();
                        else if (stage == 20)
                        {
                            GameManager.PrintEndStory();
                            GameClear();
                            player.Hp = 0;
                            return;
                        }
                        dungeon.DungeonSuccess();//던전클리어
                        player.Stage++;
                        stage++;
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

        public void PrintTitle()//던전 전투 이름 출력
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

        public void PrintPlayer()//플레이어 정보 출력
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

        public void PrintMonster(Monster monster)//몬스터 정보 출력
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

        public void PrintReward()//보상출력
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
            Console.WriteLine($"  {player.Name}({player.job.name})");
            Console.Write("HP ");
            GameManager.PrintColoredLine($"{player.Hp}/{player.MaxHP}", ConsoleColor.Magenta);
            Console.Write("MP ");
            GameManager.PrintColoredLine($"{player.mp}/{player.maxMp}\n", ConsoleColor.Magenta);
            Console.WriteLine("\n[획득아이템]");
            GameManager.PrintColored("500", ConsoleColor.Magenta);
            Console.WriteLine(" Gold");

            if (dungeon.rewardConsume != null)
            {
                Console.Write($"{dungeon.rewardConsume.itName} - ");
                GameManager.PrintColoredLine($"{dungeon.rewardConsume.itemCount}", ConsoleColor.Magenta);
            }
            if (dungeon.rewardEquip != null)
            {
                Console.Write($"{dungeon.rewardEquip.itName} - ");
                GameManager.PrintColoredLine("1", ConsoleColor.Magenta);
            }
            SoundManager.Instance.StopSound("dungeonBGM");
            Console.WriteLine("\n0. 다음\n");
            GameManager.CheckWrongInput(out int con, 0, 0);
        }

        public int PrintSelectMonster()//몬스터 선택창 출력
        {
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

            return num;
        }

        public void AttackMenu()//공격메뉴
        {
            PrintDungeonUI();

            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");

            GameManager.CheckWrongInput(out int con, 1, 2);

            switch (con)
            {
                case 1:
                    SelectTarget(player.GetAttack(), false);
                    break;
                case 2:
                    SkillMenu();
                    break;
            }
        }

        public void SkillMenu()//스킬메뉴
        {
            PrintDungeonUI();
            //스킬 출력
            int num = 0;
            foreach (var skill in player.job.skills)
            {
                Console.WriteLine($"{++num}. {skill.name} - MP {skill.cost}");
                if (skill is OffensiveSkill offensive1)
                {
                    Console.WriteLine($"공격력 * {offensive1.damageModifier}");
                }
            }

            Console.WriteLine("0. 취소");
            GameManager.CheckWrongInput(out int con, 0, num);

            if (con == 0)
            {
                AttackMenu();
                return;
            }

            if (player.job.skills != null && player.job.skills[con - 1] is OffensiveSkill offensive2)
            {
                if (player.mp >= player.job.skills[con - 1].cost)
                {
                    player.mp -= player.job.skills[con - 1].cost;
                    if (offensive2.count == 0)
                    {
                        SelectTarget(offensive2.UseSkill(player.GetAttack()), true);
                    }
                    else
                    {
                        SelectTargetMulti(offensive2.UseSkill(player.GetAttack()), true, offensive2.count);
                    }
                }
                else
                {
                    Console.WriteLine("mp가 부족합니다.");
                    Thread.Sleep(500);
                    SkillMenu();
                    return;
                }
            }
            else if (player.job.skills != null && player.job.skills[con - 1] is UtilitySkill utility)
            {
                if (player.mp >= player.job.skills[con - 1].cost)
                {
                    player.mp -= player.job.skills[con - 1].cost;
                    if (utility.count == 0)
                    {
                        SelectTargetUtility(utility);
                    }
                    else
                    {
                        SelectTargetUtilityMulti(utility, utility.count);
                    }
                }
                else
                {
                    Console.WriteLine("mp가 부족합니다.");
                    Thread.Sleep(500);
                    SkillMenu();
                    return;
                }
            }

        }

        public void SelectTarget(float _attack, bool isSkill)//타겟 설정
        {
            PrintTitle();

            int num = PrintSelectMonster();

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

            PlayerAttack(target, _attack, isSkill);
        }

        public void SelectTargetMulti(float _attack, bool isSkill, int count)//타겟 설정
        {
            if (dungeon.monsterNum - deadMonster <= count)
            {
                foreach (var monster in dungeon.monsters)
                {
                    if (monster.Hp > 0)
                    {
                        PlayerAttack(monster, _attack, isSkill);
                    }
                }
                return;
            }
            PrintTitle();

            int num = PrintSelectMonster();

            PrintPlayer();
            Console.WriteLine("0. 취소");
            Monster target;
            int con;
            int selectNum = 0;
            List<bool> selectList = Enumerable.Repeat(false, dungeon.monsterNum).ToList();

            while (selectNum < count)
            {
                GameManager.CheckWrongInput(out con, 0, num);

                if (con == 0)
                {
                    AttackMenu();
                    return;
                }

                target = dungeon.monsters[con - 1];

                if (target.Hp <= 0 || selectList[con - 1] == true)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                else
                {
                    selectList[con - 1] = true;
                    selectNum++;
                }
            }

            for (int i = 0; i < selectList.Count; i++)
            {
                if (selectList[i] == true)
                {
                    PlayerAttack(dungeon.monsters[i], _attack, isSkill);
                }
            }
        }

        public void PlayerAttack(Monster monster, float _attack, bool isSkill)
        {
            int monsterHp = monster.Hp;
            monster.TakeDamage(_attack, player.CritRate, isSkill);
            PrintTitle();
            Console.WriteLine($"{player.Name}의 공격!");
            Console.Write("Lv.");
            GameManager.PrintColored($"{player.Level}", ConsoleColor.Magenta);
            Console.WriteLine($" {monster.Name}");
            Console.Write("HP ");
            GameManager.PrintColored($"{monsterHp}", ConsoleColor.Magenta);
            Console.Write(" -> ");
            if (monster.Hp > 0)
            {
                GameManager.PrintColored($"{monster.Hp}", ConsoleColor.Magenta);
            }
            else
            {
                deadMonster++;
                Console.WriteLine("Dead");

                //  퀘스트 진행도 업데이트
                bool isBoss = bossMonsters.Contains(monster); // 보스 몬스터인지 확인
                QuestManager.Instance.OnMonsterKilled(isBoss);
            }

            Console.WriteLine("\n0. 다음\n");
            GameManager.CheckWrongInput(out int con, 0, 0);

            if (monster.Hp <= 0)
            {
                monster.Dead();
            }
        }

        public void SelectTargetUtility(UtilitySkill utilitySkill)//유틸리티스킬 타겟 설정
        {
            PrintTitle();
            int num = PrintSelectMonster();
            Console.WriteLine($"{num + 1} {player.Name}");

            PrintPlayer();
            Console.WriteLine("0. 취소");
            Monster target;
            int con;

            while (true)
            {
                GameManager.CheckWrongInput(out con, 0, num + 1);

                if (con == 0)
                {
                    AttackMenu();
                    return;
                }

                if (con == num + 1)
                {
                    utilitySkill.UseSkill(player);
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


            PrintTitle();
            Console.WriteLine($"{player.Name}의 {utilitySkill.name}!");
            float atk = target.GetAttack();
            utilitySkill.UseSkill(target);

            Console.WriteLine("\n0. 다음\n");
            GameManager.CheckWrongInput(out con, 0, 0);

        }

        public void SelectTargetUtilityMulti(UtilitySkill utilitySkill, int count)//유틸리티스킬 타겟 설정
        {
            if (dungeon.monsterNum - deadMonster <= count)
            {
                foreach (var monster in dungeon.monsters)
                {
                    if (monster.Hp > 0)
                    {
                        PlayerUseUtilitySkill(utilitySkill, monster);
                    }
                }
                return;
            }

            PrintTitle();
            int num = PrintSelectMonster();
            Console.WriteLine($"{num + 1} {player.Name}");

            PrintPlayer();
            Console.WriteLine("0. 취소");
            Monster target;
            int con;
            int selectNum = 0;
            List<bool> selectList = Enumerable.Repeat(false, dungeon.monsterNum).ToList();

            while (selectNum < count)
            {
                GameManager.CheckWrongInput(out con, 0, num);

                if (con == 0)
                {
                    AttackMenu();
                    return;
                }

                target = dungeon.monsters[con - 1];

                if (target.Hp <= 0 || selectList[con - 1] == true)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                else
                {
                    selectList[con - 1] = true;
                    selectNum++;
                }
            }

            for (int i = 0; i < selectList.Count; i++)
            {
                if (selectList[i] == true)
                {
                    PlayerUseUtilitySkill(utilitySkill, dungeon.monsters[i]);
                }
            }

        }

        public void PlayerUseUtilitySkill(UtilitySkill utilitySkill, Monster target)
        {
            PrintTitle();
            Console.WriteLine($"{player.Name}의 {utilitySkill.name}!");
            float atk = target.GetAttack();
            utilitySkill.UseSkill(target);
            Console.WriteLine("\n0. 다음\n");
            GameManager.CheckWrongInput(out int con, 0, 0);
        }

        public void MonsterAttack()//몬스터의 공격
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

                    player.TakeDamage(monster.GetAttack(), monster.CritRate, false);

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

        public void GameClear()//게임클리어
        {
            GameSaveManager gameSaveManager = new GameSaveManager();
            gameSaveManager.DeleteCharacter(player.Name);
            Console.Clear();
            Console.WriteLine("Game Clear");
        }
    }
}
