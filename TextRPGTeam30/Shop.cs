namespace TextRPGTeam30
{
    public class Shop
    {
        public Player player;
        public List<Item> items = new List<Item>();

        public Shop()
        {

        }

        public Shop(Player player)
        {
            this.player = player;
            items = new List<Item>()
            {
                new Armor("플레이트 헬멧", 10, "방어력", "플레이트 메일과 세트로 이루는 무거운 투구.", 8),
                new Armor("본 헬름", 30, "방어력", "동물의 뼈를 이용하여 악마의 머리 모양으로 깎아놓은 투구.", 20),
                new Weapon("숏 소드", 4, "공격력", "편하게 사용할 수 있는 짧고 가벼운 소드.", 5),
                new Weapon("목검", 5, "공격력", "나무로 만들어진 검술 연습용 목검.", 7),
                new Weapon("커틀러스", 9, "공격력", "해적들이 사용하던 폭이 넓은 검.", 12),
                new Weapon("바스타드 소드", 13, "공격력", "길고 곧은 날이 평평한 손잡이에 연결되어 있는 형태를 가진 검.",20),
                new Weapon("브로드 소드", 20, "공격력", "기사들이 가장 일반적으로 사용했던 양날검.",30),
                new Weapon("아론다이트", 40, "공격력", "원탁의 기사단 단장 란슬롯이 사용했다는 중세 시대의 검.", 40),
                new Armor("합판", 7, "방어력", "나무로 만들어진 방패.", 4),
                new Armor("레더 쉴드", 10, "방어력", "가죽으로 만들어진 원형 방패.", 13),
                new Armor("플레이트 메일", 8, "방어력", "판금과 사슬로 만들어진 갑옷.",5),
                new Armor("스케일 아머", 15, "방어력", "금속 조각을 물고기 비늘처럼 붙여 만든 갑옷.",10),
                new Armor("브리간딘 갑옷", 35, "방어력", "부드러운 가죽이나 천 안쪽에 작은 쇠판을 리벳으로 고정시킨 형태의 갑옷.", 25),
                new Armor("체인메일 글러브", 14, "방어력", "촘촘한 망사로 만든 글러브.", 8),
                new Armor("건틀렛", 25, "방어력", "철로 만들어진 전투용 장갑.", 15),
                new Armor("파피루스 샌들", 4, "방어력", "무게가 가벼우나 내구성이 약한 신발.", 2),
                new Armor("스파이크 슈즈", 7, "방어력", "눈길에 미끄러지지 않는 미끄럼 방지 신발.", 6),
                new Armor("이더 부츠", 10, "방어력", "가죽으로 만든 목이 긴 부츠.", 9),
                new Armor("천 망토", 12, "방어력", "튼튼한 천이 소재인 몸을 보호하는 망토.",3),
                new HealingPotion("녹색 망토", 20, "방어력", "숲에서 몸을 숨기고 기습하는 데에 최적인 녹색 망토.", 10),
                new HealingPotion("체력 하급포션", 30, "회복력", "HP 30 회복", 5),
                new HealingPotion("체력 중급 포션", 50, "회복력", "HP 50 회복", 10),
                new HealingPotion("체력 상급포션", 100, "회복력", "HP 100 회복", 30),
                new ManaPotion("마나 포션", 30, "회복력", "MP 30 회복", 5),
                new ManaPotion("마나 포션", 50, "회복력", "MP 50 회복", 10),
                new ManaPotion("마나 포션", 100, "회복력", "MP 50 회복", 30),


            };
        }

        public void PrintItemList(bool adjustFlag) // 아이템 목록 출력
        {
            Console.WriteLine("[보유 골드]");
            Console.WriteLine(player.gold + " G\n");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];

                Console.Write("- ");
                if (adjustFlag) Console.Write($"{i + 1} ");
                Console.Write($"{item.itName,-10} | ");
                Console.Write(item is Weapon ? $"공격력 +{item.itAbility} | " : $"방어력 +{item.itAbility} | ");
                bool isSelled = player.inventory.Exists(eq => eq.itName == item.itName);
                Console.Write($"{item.itInfo,-15} | {(isSelled ? "구매완료" : $"{item.Price} G")}\n");
            }
        }

        public void PrintShop()
        {
            Console.Clear();
            Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n");

            PrintItemList(false);

            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");

            GameManager.CheckWrongInput(out int select, 0, 2);
            switch (select)
            {
                case 0:
                    return;
                case 1:
                    SelectBuyItems();
                    break;
                case 2:
                    SelectSellItems();
                    break;
            }
        }

        public void SelectBuyItems()//구매할 아이템 선택
        {
            Console.Clear();
            Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("해당 숫자 아이템을 선택해 구매할 수 있습니다.\n");

            PrintItemList(true);

            Console.WriteLine("\n0. 나가기");

            GameManager.CheckWrongInput(out int select, 0, items.Count);
            if (select == 0) PrintShop();
            else BuyItem(select);
        }

        public void BuyItem(int select)//아이템 구매
        {
            bool isSelled = false;
            foreach (Item eq in player.inventory)
            {
                if (eq.itName == items[select - 1].itName)
                {
                    isSelled = true;
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    Thread.Sleep(500);
                    break;
                }
            }
            if (!isSelled && player.UseGold(items[select - 1].Price))
            {
                player.inventory.Add(items[select - 1]);
                player.inventory.Last().Price = (int)(player.inventory.Last().Price * 0.85f);
            }

            SelectBuyItems();
        }

        public void SelectSellItems()//판매할 아이템 선택
        {
            Console.Clear();
            Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("해당 숫자 아이템을 선택해 판매할 수 있습니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine(player.gold + " G\n");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < player.inventory.Count; i++)
            {
                Item item = player.inventory[i];
                Console.Write($"- {i + 1} ");
                if (item is Equipable equip && equip.isEquip) Console.Write("[E]");
                Console.Write($"{item.itName,-10} | ");
                if (item is Weapon) Console.Write($"공격력 +{item.itAbility} | ");
                else Console.Write($"방어력 +{item.itAbility} | ");
                Console.Write(item.itInfo  + " | ");
                Console.WriteLine(item.Price + " G");
            }

            Console.WriteLine("\n0. 나가기");

            GameManager.CheckWrongInput(out int select, 0, player.inventory.Count);
            if (select == 0) PrintShop();
            else SellItem(select);
        }

        public void SellItem(int select)//아이템 판매
        {
            Item item = player.inventory[select - 1];
            if (player.equipWeapon is not null && player.equipWeapon.itName == item.itName) player.equipWeapon = null;
            if (player.equipArmor is not null && player.equipArmor.itName == item.itName) player.equipArmor = null;
            //판매
            player.gold += item.Price;
            player.inventory.Remove(item);

            SelectSellItems();
        }
    }
}
