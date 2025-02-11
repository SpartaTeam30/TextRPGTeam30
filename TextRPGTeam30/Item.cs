namespace TextRPGTeam30
{
    public class Item
    {
        public int ID { get; set; }
        // public int Price { get; set; }    

        public string itName { get; set; }
        public int itAbility { get; set; }
        public string itType { get; set; }
        public string itInfo { get; set; }

        public Item(string itName, int itAbility, string itType, string itInfo)
        {
            this.itName = itName;
            this.itType = itType;
            this.itInfo = itInfo;
            this.itAbility = itAbility;
        }

        // 마법사 - "현자의 돌", 60, "마력", "중세 연금사들이 비금속을 황금으로 바꿀 수 있는 재료.") // 장신구
        // "아크 완드", 50, "마법 공격력", "마력 증폭을 위한 수정 구슬이 달려있는 나무 지팡이.") // 무기
        // "실크 로브", 30, "방어력", "비단 재질의 고급스러워 보이는 로브.") // 장비
        // "서클릿", 40, "마력", "마녀들이 애용했다는 이마에 댈 수 있게 만들어진 장신구") // 방어구
    }
}

