using System.ComponentModel;

namespace TextRPGTeam30
{
    internal class DungeonManager
    {
        int stage;
        int deadMonster;
        Player player;
        Dungeon? dungeon;
        List<Monster> monsters;
        public DungeonManager(Player player)
        {
            this.player = player;
            stage = 1;
            monsters = new List<Monster>()
            {
                new Monster("공허충", 1, 10 , 3), new Monster("미니언", 1, 15, 5), new Monster("대포미니언", 1, 25, 10)
            };
        }
        public void CreateDungeon()
        {
            int max_range_monster = 3 + stage / 5;
            int min_range_monster = 1 + stage / 5;
            if (max_range_monster >= monsters.Count)
            {
                min_range_monster = max_range_monster - 3;
                max_range_monster = monsters.Count - 1;
            }
            dungeon = new Dungeon(stage, monsters.GetRange(min_range_monster, max_range_monster));
        }
        public void DungeonStart()
        {
            CreateDungeon();
            deadMonster = 0;
            while (true)
            {
                AttackMenu();
                if (deadMonster == dungeon.monsters.Count)
                {
                    dungeon.DungeonSuccess();
                    break;
                }
                MonsterAttack();
                if (player.Hp <= 0)
                {
                    dungeon.DungeonFail();
                    return;
                }
            }
            dungeon.DungeonSuccess();

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
            GameManager.PrintColoredLine($"{player.Hp}/100", ConsoleColor.Magenta);
            Console.Write("MP ");
            GameManager.PrintColoredLine($"{player.mp}/50\n", ConsoleColor.Magenta);
            stage++;

        }
        public void PrintDungeonUI()
        {
            Console.Clear();
            GameManager.PrintColoredLine("\nBattle!!\n", ConsoleColor.Yellow);
            foreach (Monster monster in dungeon.monsters)
            {
                if (monster.Hp > 0)
                {
                    Console.Write("Lv.");
                    GameManager.PrintColored($"{monster.Level}",ConsoleColor.Magenta);
                    Console.Write($" {monster.Name} HP ");
                    GameManager.PrintColoredLine($"{monster.Hp}", ConsoleColor.Magenta);
                }
                else
                {
                    GameManager.PrintColoredLine($"Lv.{monster.Level} {monster.Name} Dead", ConsoleColor.DarkGray);
                }
            }
            Console.WriteLine("\n[내정보]");
            Console.Write($"Lv.");
            GameManager.PrintColored($"{player.Level}", ConsoleColor.Magenta);
            Console.WriteLine($"  {player.Name} ()");
            Console.Write("HP ");
            GameManager.PrintColoredLine($"{player.Hp}/100", ConsoleColor.Magenta);
            Console.Write("MP ");
            GameManager.PrintColoredLine($"{player.mp}/50\n", ConsoleColor.Magenta);
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
            Console.Clear();
            GameManager.PrintColoredLine("\nBattle!!\n", ConsoleColor.Yellow);
            int num = 0;
            foreach (Monster monster in dungeon.monsters)
            {
                if (monster.Hp > 0)
                {
                    GameManager.PrintColored($"{++num}", ConsoleColor.Cyan);
                    Console.Write($" Lv.");
                    GameManager.PrintColored($"{monster.Level}", ConsoleColor.Magenta);
                    Console.Write($" {monster.Name} HP ");
                    GameManager.PrintColoredLine($"{monster.Hp}", ConsoleColor.Magenta);
                }
                else
                {
                    GameManager.PrintColoredLine($"{++num} Lv.{monster.Level} {monster.Name} Dead", ConsoleColor.DarkGray);
                }
            }
            Console.WriteLine("\n[내정보]");
            Console.Write($"Lv.");
            GameManager.PrintColored($"{player.Level}", ConsoleColor.Magenta);
            Console.WriteLine($"  {player.Name} ()");
            Console.Write("HP ");
            GameManager.PrintColoredLine($"{player.Hp}/100", ConsoleColor.Magenta);
            Console.Write("MP ");
            GameManager.PrintColoredLine($"{player.mp}/50\n", ConsoleColor.Magenta);
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
            Console.Clear();
            GameManager.PrintColoredLine("\nBattle!!\n", ConsoleColor.Yellow);
            Console.WriteLine($"{player.Name}의 공격!");

            target.TakeDamage(player.Attack, player.Evasosion, false);
            Console.Write("Lv.");
            GameManager.PrintColored($"{player.Level}", ConsoleColor.Magenta);
            Console.WriteLine($" {target.Name}");
            Console.Write("HP ");
            GameManager.PrintColored($"{targetHp}",ConsoleColor.Magenta);
            Console.Write(" -> ");
            if(target.Hp > 0)
            {
                Console.WriteLine($"{target.Hp}");
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
                    Console.Clear();
                    GameManager.PrintColoredLine("\nBattle!!\n", ConsoleColor.Yellow);
                    Console.Write($"Lv. ");
                    GameManager.PrintColored($"{monster.Level}", ConsoleColor.Magenta);
                    Console.WriteLine($" {monster.Name} 의 공격");
                    player.TakeDamage((int)monster.Attack);
                    if (player.Hp <= 0)
                    {
                        return;
                    }

                    Console.WriteLine($"Lv. {player.Level} {player.Name}");
                    Console.WriteLine($"HP {player.Hp} -> {player.Hp}\n");
                    Console.WriteLine("0. 다음\n");
                    GameManager.CheckWrongInput(out int con, 0, 0);
                }
            }
        }
    }
}
