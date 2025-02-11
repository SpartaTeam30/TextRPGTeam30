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
                new Armor("본 헬름", 30, "방어력", "동물의 뼈를 이용하여 악마의 머리 모양으로 깎아놓은 투구.", 20, 100),
                new Weapon("아론다이트", 40, "공격력", "원탁의 기사단 단장 란슬롯이 사용했다는 중세 시대의 검.", 30, 100),
                new Armor("브리간딘 갑옷", 35, "방어력", "부드러운 가죽이나 천 안쪽에 작은 쇠판을 리벳으로 고정시킨 형태의 갑옷.", 25, 100),
                new Armor("건틀렛", 25, "방어력", "철로 만들어진 전투용 장갑.", 15, 100),
                new Armor("이더 부츠", 10, "방어력", "가죽으로 만든 목이 긴 부츠.", 7, 100),
                new Armor("녹색 망토", 20, "방어력", "숲에서 몸을 숨기고 기습하는 데에 최적인 녹색 망토.", 10, 100),
                new HealingPotion("체력 물약", 30,"체력 회복","마시면 체력이 회복된다.",100, 1),
                new ManaPotion("마나 물약", 30,"마나 회복","마시면 마나가 회복된다.",100 ,1)
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
                Console.Write($"{item.itInfo,-15} | {(isSelled && item is Equipable ? "구매완료" : $"{item.Price} G")}\n");
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
            else if (items[select - 1] is Equipable) BuyEquipableItem(select);
            else BuyConsumableItem(select);
        }

        public void BuyEquipableItem(int select)//아이템 구매
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

        public void BuyConsumableItem(int select)//아이템 구매
        {
            bool isSelled = false;
            Consumable consumable = null;

            if (player.UseGold(items[select - 1].Price))
            {
                foreach (Item eq in player.inventory)
                {
                    if (eq.itName == items[select - 1].itName)
                    {
                        isSelled = true;
                        consumable = (Consumable)eq;
                        break;
                    }
                }
                if (isSelled)
                {
                    consumable.itemCount++;
                }
                if (!isSelled)
                {
                    player.inventory.Add(items[select - 1]);
                    player.inventory.Last().Price = (int)(player.inventory.Last().Price * 0.85f);
                }
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
                Console.Write(item.itInfo + " | ");
                Console.WriteLine(item.Price + " G");
            }

            Console.WriteLine("\n0. 나가기");

            GameManager.CheckWrongInput(out int select, 0, player.inventory.Count);
            if (select == 0) PrintShop();
            else if (player.inventory[select - 1] is Equipable) SellEquipItem(select);
            else SellConsumableItem(select);
        }

        public void SellEquipItem(int select)//아이템 판매
        {
            Item item = player.inventory[select - 1];
            if (player.equipWeapon is not null && player.equipWeapon.itName == item.itName) player.equipWeapon = null;
            if (player.equipArmor is not null && player.equipArmor.itName == item.itName) player.equipArmor = null;
            //판매
            player.gold += item.Price;
            player.inventory.Remove(item);

            SelectSellItems();
        }

        public void SellConsumableItem(int select)//아이템 판매
        {
            Item item = player.inventory[select - 1];
            if(item is not null && item is Consumable c)
            {
                player.gold += item.Price;
                if (c.itemCount > 1)
                {
                    c.itemCount--;
                }
                else
                {
                    player.inventory.Remove(item);
                }
            }

            SelectSellItems();
        }
    }
}
