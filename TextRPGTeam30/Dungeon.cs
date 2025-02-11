namespace TextRPGTeam30
{
    public class Dungeon
    {
        public Player player;
        public int stage;
        public int rewardExp;
        public int rewardGold;
        public int monsterNum;
        public int uniqueRate;
        public List<Monster> monsters;
        public List<Equipable> equipables;
        public List<Consumable> consumables;

        public Dungeon(Player player, int _stage, List<Monster> _monsters, Monster _BossMonster)
        {
            this.player = player;
            stage = _stage;
            rewardExp = 0;
            monsters = new List<Monster>();
            rewardGold = 0;
            uniqueRate = 5;
            monsterNum = new Random().Next(1, 4);

            if (stage % 20 == 0)
            {
                monsters.Add(_BossMonster);
                monsters[0].SetLevel(new Random().Next(stage / 5 + 1, stage / 5 + 6));
            }

            else
            {
                for (int i = 0; i < monsterNum; i++)
                {
                    int index = new Random().Next(0, _monsters.Count);
                    Monster monster = new Monster(_monsters[index]);
                    int unique = new Random().Next(1, 101);

                    if (unique <= uniqueRate)
                    {
                        monster.isUnique = true;
                        monster.Hp += 10;
                        monster.Attack += 5;
                    }

                    monsters.Add(monster);
                    monsters[i].SetLevel(new Random().Next(stage / 5 + 1, stage / 5 + 6));
                }
            }
        }

        public void DungeonSuccess()
        {
            foreach (Monster monster in monsters)
            {
                if (monster.isUnique || stage % 20 == 0)
                {
                    rewardExp += monster.Level;
                    rewardGold += 50;
                }
                rewardExp += monster.Level;
            }

            if (stage % 20 == 0)
            {
                rewardGold += new Random().Next(stage * 200, stage * 400);
            }
            else
            {
                rewardGold += new Random().Next(stage * 100, stage * 200);
            }

            //exp
            player.LevelUp(rewardExp);

            //item

        }

        public void DungeonFail()
        {
            Console.Clear();
            GameManager.PrintColoredLine("Game Over");
            Console.WriteLine($"{player.Name}�� �׾����ϴ�.");
            GameSaveManager gameSaveManager = new GameSaveManager();
            gameSaveManager.DeleteCharacter(player.Name);
        }
    }
}
