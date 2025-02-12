namespace TextRPGTeam30
{
    public class Quest
    //클래스 퀘스트 변수
    {
        public int Id { get; set; }                            // 퀘스트 아이디
        public string Name { get; set; }                       // 퀘스트 명칭
        public string Description { get; set; }                // 퀘스트 설명
        public int Condition { get; set; }                     //완료조건
        public int Progress { get; set; }                      //진행 상황
        public Item RewardItem { get; set; }                 //보상 아이템
        public int RewardGold { get; set; }                    //보상 골드
        public int RewardExp { get; set; }                     //보상 경험치
        public int Status { get; set; }                        //퀘스트 상태 0=미수락,1=진행중,2=완료,3=보상수령완료

        public int Type { get; set; }                          //퀘스트 타입 0=일반 몬스터, 1 = 보스몬스터, 3= 무기 장착, 4= 방어구 장착, 5= 레벨링 

        public Quest(int id, string name, string description, int condition, int progress, Item rewardItem, int rewardGold, int rewardExp, int status, int type)
        {
            Id = id;
            Name = name;
            Description = description;
            Condition = condition;
            Progress = progress;
            RewardItem = rewardItem;
            RewardGold = rewardGold;
            RewardExp = rewardExp;
            Status = status;
            Type = type;
        }

        public bool ShowQuestDetails()
        {
            Console.Clear();
            Console.WriteLine("============= [퀘스트 상세 정보] =============");
            Console.WriteLine($"퀘스트 이름: {Name}");
            Console.WriteLine($"설명: {Description}");
            Console.WriteLine($"진행도: {Progress}/{Condition}");

            // 목표 달성 여부 자동 업데이트
            if (Progress >= Condition && Status == 1)
            {
                Console.WriteLine("\n[!] 퀘스트 목표를 달성했습니다!");
                Status = 2; // 완료 상태로 변경
            }

            Console.WriteLine("\n보상");
            if (RewardItem != null)
            {
                Console.WriteLine($"아이템: {RewardItem.itName}");
            }
           
            Console.WriteLine($"골드: {RewardGold} G");
            Console.WriteLine($"경험치: {RewardExp} EXP");
            Console.WriteLine("==============================================");

            if (Status == 0)
                Console.WriteLine("1. 수락\n2. 거절");
            else if (Status == 1)
                Console.WriteLine("1. 포기하기\n2. 돌아가기");
            else if (Status == 2)
                Console.WriteLine("1. 보상받기\n2. 나중에 받기");
            else
                Console.WriteLine("[보상을 이미 받았습니다.]\n1. 돌아가기");

            Console.Write(">> ");
            GameManager.CheckWrongInput(out int choice, 1, 2);

            bool isUpdated = false; // 변경 여부 체크

            if (Status == 0 && choice == 1)
            {
                Status = 1; // 퀘스트 수락
                isUpdated = true;
            }
            else if (Status == 1 && choice == 1)
            {
                Status = 0; // 퀘스트 포기
                isUpdated = true;
            }
            else if (Status == 2 && choice == 1)
            {
                Status = 3; // 보상 수령 완료
                QuestReward();
                Console.WriteLine("보상을 받았습니다!");
                isUpdated = true;
            }
            return isUpdated;
        }

        public void QuestReward()
        {
            QuestManager.Instance.player.inventory.Add(RewardItem);
            QuestManager.Instance.player.LevelUp(RewardExp);
            QuestManager.Instance.player.gold += RewardGold;
            return;
        }
    }
}
