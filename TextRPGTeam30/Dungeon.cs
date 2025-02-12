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
        public Equipable? rewardEquip;
        public Consumable? rewardConsume;
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
            rewardEquip = null;
            rewardConsume = null;
            monsterNum = new Random().Next(1, 4);
            equipables = new List<Equipable>()
            {
                new Armor("본 헬름", 20, "방어력", "동물의 뼈를 이용하여 악마의 머리 모양으로 깎아놓은 투구.", 30, 100),
                new Weapon("아론다이트", 30, "공격력", "원탁의 기사단 단장 란슬롯이 사용했다는 중세 시대의 검.", 40, 100),
                new Armor("브리간딘 갑옷", 25, "방어력", "부드러운 가죽이나 천 안쪽에 작은 쇠판을 리벳으로 고정시킨 형태의 갑옷.", 15, 100),
                new Armor("건틀렛", 15, "방어력", "철로 만들어진 전투용 장갑.", 10, 100),
                new Armor("이더 부츠", 7, "방어력", "가죽으로 만든 목이 긴 부츠.", 5, 100),
                new Armor("녹색 망토", 10, "방어력", "숲에서 몸을 숨기고 기습하는 데에 최적인 녹색 망토.", 10, 100),
            };
            consumables = new List<Consumable>()
            {
                new HealingPotion("체력 물약", 30, "체력 회복","마시면 체력이 회복된다.", 100, 1),
                new ManaPotion("마나 물약", 30, "마나 회복","마시면 마나가 회복된다.", 100, 1)
            };

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

            player.gold += rewardGold;
            //exp
            player.LevelUp(rewardExp);

            //item
            int rewardItemRate = new Random().Next(1, 101);
            int rewardConsumeRate = new Random().Next(1, 101);

            if (rewardItemRate <= 90)
            {
                int rewardIndex = new Random().Next(0, equipables.Count);
                rewardEquip = equipables[rewardIndex];
                player.inventory.Add(rewardEquip);
            }
            if (rewardConsumeRate <= 90) 
            {
                int rewardIndex = new Random().Next(0, consumables.Count);
                rewardConsume = consumables[rewardIndex];
                Consumable? playerConsume = (Consumable?)player.inventory.Find(p => p.itName == rewardConsume.itName);
                if (playerConsume != null) {
                    playerConsume.itemCount += rewardConsume.itemCount;
                }
                else
                {
                    player.inventory.Add(rewardConsume);
                }
            }

        }

        public void DungeonFail()
        {
            Console.Clear();
            GameManager.PrintColoredLine("Game Over");
            Console.WriteLine($"{player.Name}가 죽었습니다.");
            GameSaveManager gameSaveManager = new GameSaveManager();
            gameSaveManager.DeleteCharacter(player.Name);
        }
    }
}
