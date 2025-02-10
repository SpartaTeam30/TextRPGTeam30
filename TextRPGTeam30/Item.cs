namespace TextRPGTeam30
{
    internal interface IItem
    {
        int ID { get; set; }
        string Name { get; set; }
        // public int Price { get; set; }
        string IDescription { get; set; } // 변수 설명용          
    } 
        public string Description { get; set; }
        // Inventory inventory = new Inventory(); // 인벤토리 생성

        // public void DisplayInventory() // 인벤토리 상태
        //  Console.WriteLine("인벤토리가 비어 있습니다.");
        //      return;
        // Console.WriteLine("인벤토리 아이템 목록:");
        // foreach (var item in items)
        // Console.WriteLine($"ID: {item.ID}, 이름: {item.Name}, 설명: {item.Description}");
        // public List<Item> items;
        // public Inventory()
        // items = new List<Item>();
    }
}

