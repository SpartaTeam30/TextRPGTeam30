using System.ComponentModel;

namespace TextRPGTeam30
{
    internal class DungeonManager
    {
        int stage;
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
            int max_range_monster = 3 + stage/5;
            int min_range_monster = 1 + stage/5;
            if(max_range_monster >= monsters.Count)
            {
                max_range_monster = monsters.Count - 1;
                min_range_monster = max_range_monster - 3;
            }
            dungeon = new Dungeon(stage, monsters.GetRange(min_range_monster, max_range_monster));
        }
        public void DungeonStart()
        {
            int deadMonster = 0;
            while (true) { 
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
            
        }
        public void PrintDungeonUI()
        {
            Console.Clear();
            Console.WriteLine("\nBattle!!\n");
            foreach (Monster monster in dungeon.monsters)
            {
                Console.WriteLine($"Lv.{monster.Level} {monster.Name} HP {monster.Hp}");
            }
            Console.WriteLine("\n\n[내정보]");
            Console.WriteLine($"Lv.{player.Level}  {player.Name} ()");
            Console.WriteLine($"HP {player.Hp}/100");
            Console.WriteLine($"MP {player.mp}/50\n");
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
            Console.WriteLine("\nBattle!!\n");
            int num = 0;
            foreach (Monster monster in dungeon.monsters)
            {
                Console.WriteLine($"{++num}. Lv.{monster.Level} {monster.Name} HP {monster.Hp}");
            }
            Console.WriteLine("\n\n[내정보]");
            Console.WriteLine($"Lv.{player.Level}  Chad ()");
            Console.WriteLine($"HP {player.Hp}/100");
            Console.WriteLine($"MP {player.mp}/50\n");
            Console.WriteLine("0. 취소");

            int con;
            GameManager.CheckWrongInput(out con, 0, num);

            Monster target = dungeon.monsters[con - 1];
            target.TakeDamage(player.Attack);
            if(target.Hp <= 0)
            {
                target.Dead();
            }
        }
        public void MonsterAttack()
        {
            Console.WriteLine("Battle!!");
            foreach (Monster monster in dungeon.monsters)
            {
                if(monster.Hp > 0)
                {
                    int playerHp = player.Hp;
                    Console.WriteLine($"Lv. {monster.Level} {monster.Name} 의 공격");
                    player.TakeDamage((int)monster.Attack);
                    if (player.Hp <= 0)
                    {
                        return;
                    }

                    Console.WriteLine($"Lv. {player.Level} {player.Name}");
                    Console.WriteLine($"HP {playerHp} -> {player.Hp}\n");
                    Console.WriteLine("0. 다음\n");
                    GameManager.CheckWrongInput(out int con, 0, 0);
                }
            }
        }
    }
}
