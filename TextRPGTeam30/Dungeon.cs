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
            monsterNum = new Random().Next(1, 5);
            equipables = new List<Equipable>()
            {
                new Weapon("숏 소드", 5, "공격력", "편하게 사용할 수 있는 짧고 가벼운 소드.", 10),
                new Weapon("목검", 7, "공격력", "나무로 만들어진 검술 연습용 목검.", 8),
                new Weapon("커틀러스", 12, "공격력", "해적들이 사용하던 폭이 넓은 검.", 15),
                new Weapon("바스타드 소드", 20, "공격력", "길고 곧은 날이 평평한 손잡이에 연결되어 있는 형태를 가진 검.", 20),
                new Weapon("브로드 소드", 30, "공격력", "기사들이 가장 일반적으로 사용했던 양날검.", 25),
                new Weapon("아론다이트", 40, "공격력", "원탁의 기사단 단장 란슬롯이 사용했다는 중세 시대의 검.", 40),
                new Armor("플레이트 헬멧", 8, "방어력", "플레이트 메일과 세트로 이루는 무거운 투구.", 10),
                new Armor("본 헬름", 20, "방어력", "동물의 뼈를 이용하여 악마의 머리 모양으로 깎아놓은 투구.", 3),
                new Armor("합판", 4, "방어력", "나무로 만들어진 방패.", 5),
                new Armor("레더 쉴드", 13, "방어력", "가죽으로 만들어진 원형 방패.", 10),
                new Armor("플레이트 메일", 5, "방어력", "판금과 사슬로 만들어진 갑옷.", 7),
                new Armor("스케일 아머", 10, "방어력", "금속 조각을 물고기 비늘처럼 붙여 만든 갑옷.", 12),
                new Armor("브리간딘 갑옷", 25, "방어력", "부드러운 가죽이나 천 안쪽에 작은 쇠판을 리벳으로 고정시킨 형태의 갑옷.", 40),
                new Armor("체인메일 글러브", 8, "방어력", "촘촘한 망사로 만든 글러브.", 10),
                new Armor("건틀렛", 15, "방어력", "철로 만들어진 전투용 장갑.", 20),
                new Armor("파피루스 샌들", 2, "방어력", "무게가 가벼우나 내구성이 약한 신발.", 5),
                new Armor("스파이크 슈즈", 6, "방어력", "눈길에 미끄러지지 않는 미끄럼 방지 신발.", 8),
                new Armor("이더 부츠", 9, "방어력", "가죽으로 만든 목이 긴 부츠.", 12),
                new Armor("천 망토", 3, "방어력", "튼튼한 천이 소재인 몸을 보호하는 망토.", 15),
                new Armor("녹색 망토", 10, "방어력", "숲에서 몸을 숨기고 기습하는 데에 최적인 녹색 망토.", 20)
            };
            consumables = new List<Consumable>()
            {
                new HealingPotion("체력 하급포션", 30, "회복력", "HP 30 회복", 5, 1),
                new HealingPotion("체력 중급 포션", 50, "회복력", "HP 50 회복", 10, 1),
                new HealingPotion("체력 상급포션", 100, "회복력", "HP 100 회복", 30, 1),
                new ManaPotion("마나 포션", 30, "회복력", "MP 30 회복", 5, 1),
                new ManaPotion("마나 포션", 50, "회복력", "MP 50 회복", 10, 1),
                new ManaPotion("마나 포션", 100, "회복력", "MP 50 회복", 30, 1),
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
