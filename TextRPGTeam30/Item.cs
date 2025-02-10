using System;
using System.Runtime;

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
    // Console.WriteLine("========인벤토리========");
    // private List<EquipItem> equipItemList; // 1번
    // public Inven()
    // EquipItemList = new List<EquipItem>(); //2번

    // class EquipItem // 장비목록
    // public string itName { get; set;}
    // public int itAbility { get; set;}
    // public string itType { get; set;}
    // public string itInfo { get; set;}
    // public EquipItem(string _ItName, int _ItAbility, string _ItType, string _ItInfo)

    //  itName = _ItName;
    //  itAbility = _ItAbility;
    //  itType = _ItType;
    //  itInfo = _ItInfo;

    // static void Data()
    // equipInven = new Inven(); // 객체
    // equipItem1 = new EquipItem("본 헬름", 30, "방어력", "동물의 뼈를 이용하여 악마의 머리 모양으로 깎아놓은 투구."); // 장비
    // equipItem2 = new EquipItem("아론다이트", 40, "공격력", "원탁의 기사단 단장 란슬롯이 사용했다는 중세 시대의 검."); // 장비 
    // equipItem3 = new EquipItem("브리간딘 갑옷", 35, "방어력", "부드러운 가죽이나 천 안쪽에 작은 쇠판을 리벳으로 고정시킨 형태의 갑옷."); // 장비

    // equipItem4 = new EquipItem("건틀렛", 25, "방어력", "철로 만들어진 전투용 장갑.") // 장비
    // equipItem5 = new EquipItem("이더 부츠", 10, "방어력", "가죽으로 만든 목이 긴 부츠. 작업할 때나 숲을 지날 때 편하다.") // 장비
    // equipItem6 = new EquipItem("녹색 망토", 20, "방어력", "숲에서 몸을 숨기고 기습하는 데에 최적인 녹색 망토.") // 장비

    // 마법사 - "현자의 돌", 60, "마력", "중세 연금사들이 비금속을 황금으로 바꿀 수 있는 재료.") // 장신구

    // for (int i = 0; i < equipItemList.Count; i++)
    // Console.WriteLine($" {i+1}. {equipItemList[i].itName}    | {equipItemList[i].itType} + {equipItemList[i].itAbility}    | {equipItemList[i].itInfo}");
    // Console.WriteLine();
}



