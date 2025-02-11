using Newtonsoft.Json;

namespace TextRPGTeam30
{
    public class QuestManager
    {
        public static readonly string QuestFilePath = "quests.json"; // JSON 파일 경로
                                                                     // 카테고리별 퀘스트를 Dictionary로 관리
        Dictionary<string, List<Quest>> QuestCategories;

        public QuestManager()

        {
            QuestCategories = new Dictionary<string, List<Quest>>()
            {
                { "몬스터", new List<Quest>
                    {// ID, 이름, 설명, 완료조건, 진행상황, 보상(아이템, 골드, 경험치), 퀘스트 상태 순으로 나열됌
                        new Quest(1, "미니언 처치", " 몬스터가 너무 많아 10마리를 처치하세요. ", 10, 0, "나무방패", 5, 3, 0),
                        new Quest(2, "보스 처치", " 보스를 처치하여 위협을 제거하세요.", 1, 0, "나무 칼", 5, 3, 0)
                    }
                },
                { "장비", new List<Quest>
                    {
                        new Quest(3, "무기 장비 장착"," 무기를 장착하여 전투 준비를 하세요.", 1, 0, null, 5, 3, 0),
                        new Quest(4, "방어구 장비 장착"," 방어구를 장착하여 방어력을 높이세요.", 1, 0, null, 5, 3, 0)
                    }
                },
                { "레벨업", new List<Quest>
                    {
                        new Quest(5, "레벨 5 달성"," 캐릭터 레벨을 5까지 올리세요.",5, 0, "목장갑", 15, 0, 0),
                        new Quest(6, "레벨 10 달성", " 캐릭터 레벨을 10까지 올리세요.",10, 0, "나무견갑", 15, 0, 0)
                    }
                }
            };
        }

        public void Questscreen()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("============================================================");
                Console.WriteLine("                        [퀘스트 목록]");
                Console.WriteLine("============================================================");

                Console.WriteLine("1. 몬스터 퀘스트");
                Console.WriteLine("2. 장비 퀘스트");
                Console.WriteLine("3. 레벨업 퀘스트");
                Console.WriteLine("4. 퀘스트 창 나가기");
                Console.WriteLine("");
                Console.Write(">> ");

                GameManager.CheckWrongInput(out int behavior, 1, 4);

                switch (behavior)
                {
                    case 1:
                        ShowQuestList("몬스터", QuestCategories["몬스터"]);
                        break;
                    case 2:
                        ShowQuestList("장비", QuestCategories["장비"]);
                        break;
                    case 3:
                        ShowQuestList("레벨업", QuestCategories["레벨업"]);
                        break;
                    case 4:
                        Console.WriteLine("퀘스트 창을 종료합니다.");
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                        break;
                }
            }
        }
        public void ShowQuestList(string category, List<Quest> quests) // 특정 카테고리의 퀘스트 목록을 보여주는 함수
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"============= [{category} 퀘스트 목록] =============");

                for (int i = 0; i < quests.Count; i++)
                {
                    Quest q = quests[i];
                    string statusText = q.Status == 0 ? "[미수락]" :
                                        q.Status == 1 ? "[진행중]" :
                                        q.Status == 2 ? "[완료]" :
                                        "[보상 수령 완료]";

                    Console.WriteLine($"{i + 1}. {statusText} {q.Name} - 진행도: {q.Progress}/{q.Condition}");
                }

                Console.WriteLine("0. 이전 화면으로 돌아가기");
                Console.Write(">> ");

                int select;
                bool isValid = int.TryParse(Console.ReadLine(), out select);

                if (!isValid || select < 0 || select > quests.Count)
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    continue;
                }

                if (select == 0)
                {
                    Console.WriteLine("이전 화면으로 돌아갑니다.");
                    break;
                }

                quests[select - 1].ShowQuestDetails();
            }
        }
    }

    // 퀘스트 클래스
    public class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Condition { get; set; }
        public int Progress { get; set; }
        public string Description { get; set; }
        public string RewardItem { get; set; }
        public int RewardGold { get; set; }
        public int RewardExp { get; set; }
        public int Status { get; set; }

        public Quest(int id, string name, string description, int condition, int progress, string rewarditem, int rewardgold, int rewardexp, int status)
        {
            Id = id;
            Name = name;
            Description = description;
            Condition = condition;
            Progress = progress;
            RewardItem = rewarditem;
            RewardGold = rewardgold;
            RewardExp = rewardexp;
            Status = status;
        }
        public void GiveReward()     // 보상을 지급하는 함수 (181번줄 사용)
        {
            Console.WriteLine($"[보상 지급] {RewardGold} G, {RewardExp} EXP 획득!");
            if (!string.IsNullOrEmpty(RewardItem))
            {
                Console.WriteLine($"[아이템 획득] {RewardItem}");
            }
            Status = 3; // 보상 지급 완료 상태 (완료 후 다시 못 받게)
        }

        public void ShowQuestDetails() // 퀘스트 상세 정보 및 수락/거절 기능
        {
            Console.Clear();
            Console.WriteLine("============= [퀘스트 상세 정보] =============");
            Console.WriteLine($"퀘스트 이름: {Name}");
            Console.WriteLine($"설명: {Description}");
            Console.WriteLine($"진행도: {Progress}/{Condition}");

            // 진행도가 목표를 달성하면 자동으로 완료 상태로 변경
            if (Progress >= Condition && Status == 1)
            {
                Console.WriteLine("\n[!] 퀘스트 목표를 달성했습니다!");
                Status = 2; // 완료 상태로 변경
            }

            Console.WriteLine("");
            Console.WriteLine("보상");
            Console.WriteLine($"아이템: {RewardItem ?? "없음"}");
            Console.WriteLine($"골드: {RewardGold} G");
            Console.WriteLine($"경험치: {RewardExp} EXP");
            Console.WriteLine("==============================================");

            // 상태에 따른 선택지 변경
            if (Status == 0) // 미수락 상태
            {
                Console.WriteLine("1. 수락");
                Console.WriteLine("2. 거절");
            }
            else if (Status == 1) // 진행 중 상태
            {
                Console.WriteLine("1. 포기하기");
                Console.WriteLine("2. 돌아가기");
            }
            else if (Status == 2) // 완료 상태
            {
                Console.WriteLine("1. 보상받기");
                Console.WriteLine("2. 나중에 받기");
            }
            else if (Status == 3) // 보상 지급 완료 상태
            {
                Console.WriteLine("[보상을 이미 받았습니다.]");
                Console.WriteLine("1. 돌아가기");
            }
            ChoiceList();
        }

        public void ChoiceList() // 상태에 따른 선택지 변경(141번 줄)
        {
            Console.Write(">> ");
            GameManager.CheckWrongInput(out int choice, 1, 2);

            if (Status == 0) // 미수락 상태
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine($"{Name} 퀘스트를 수락하였습니다.");
                        Status = 1; // 진행 중 상태로 변경
                        break;
                    case 2:
                        Console.WriteLine($"{Name} 퀘스트를 거절하였습니다.");
                        return; // 거절 후 돌아가기

                }
                ShowQuestDetails();
            }
            else if (Status == 1) // 진행 중 상태
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine($"{Name} 퀘스트를 포기하였습니다.");
                        Status = 0; // 다시 미수락 상태로 변경
                        break;
                    case 2:
                        Console.WriteLine("이전 화면으로 돌아갑니다.");
                        return;

                }
                ShowQuestDetails();
            }
            else if (Status == 2) // 완료 상태
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine($"{Name} 퀘스트 보상을 받았습니다.");
                        GiveReward(); // 보상 지급 함수 호출
                        return;
                    case 2:
                        Console.WriteLine("나중에 보상을 받기로 했습니다.");
                        return;

                }
                ShowQuestDetails();
            }
            else if (Status == 3) // 보상 지급 완료 상태
            {
                Console.WriteLine("이전 화면으로 돌아갑니다.");
                return;
            }
        }
    }
}



// 퀘스트 클래스
public class Quest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Condition { get; set; }
    public int Progress { get; set; }
    public string Description { get; set; }
    public string RewardItem { get; set; }
    public int RewardGold { get; set; }
    public int RewardExp { get; set; }
    public int Status { get; set; }

    public Quest(int id, string name, string description, int condition, int progress, string rewarditem, int rewardgold, int rewardexp, int status)
    {
        Id = id;
        Name = name;
        Description = description;
        Condition = condition;
        Progress = progress;
        RewardItem = rewarditem;
        RewardGold = rewardgold;
        RewardExp = rewardexp;
        Status = status;
    }
    public void GiveReward()     // 보상을 지급하는 함수 (181번줄 사용)
    {
        Console.WriteLine($"[보상 지급] {RewardGold} G, {RewardExp} EXP 획득!");
        if (!string.IsNullOrEmpty(RewardItem))
        /*
        // JSON에서 퀘스트 불러오기 (파일이 없으면 빈 리스트 반환)
        public List<Quest> LoadQuests()
        {
            if (!File.Exists(QuestFilePath))
            {
                Console.WriteLine("퀘스트 파일을 찾을 수 없습니다. 빈 리스트를 반환합니다.");
                return new List<Quest>();
            }

            string jsonData = File.ReadAllText(QuestFilePath);
            return JsonConvert.DeserializeObject<List<Quest>>(jsonData);
        }

        // JSON으로 퀘스트 저장
        public void SaveQuests(List<Quest> quests)
        {
            string jsonData = JsonConvert.SerializeObject(quests, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(QuestFilePath, jsonData);
            Console.WriteLine("퀘스트 데이터가 저장되었습니다.");
        }
        */
        // 특정 퀘스트의 진행 상태 업데이트
        public void UpdateQuestProgress(Quest quest, int progressIncrease)
        {
            if (quest.Status != 1)
            {
                Console.WriteLine("진행 중인 퀘스트가 아닙니다.");
                return;
            }

            quest.Progress = Math.Min(quest.Condition, quest.Progress + progressIncrease);

            if (quest.Progress >= quest.Condition)
            {
                quest.Status = 2; // 완료 상태 변경
                Console.WriteLine($"{quest.Name} 퀘스트를 완료했습니다!");
            }
        }
    }
}