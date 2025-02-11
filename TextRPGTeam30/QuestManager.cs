using Newtonsoft.Json;

namespace TextRPGTeam30;

internal class QuestManager
{
    //클래스 퀘스트 변수
    public class Quest
    {
        private QuestManager questManager;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Condition { get; set; }
        public int Progress { get; set; }
        public string RewardItem { get; set; }
        public int RewardGold { get; set; }
        public int RewardExp { get; set; }
        public int Status { get; set; }

        public int Type { get; set; }

        public Quest(QuestManager questManager, int id, string name, string description, int condition, int progress, string rewardItem, int rewardGold, int rewardExp, int status, int type)
        {
            this.questManager = questManager;
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

        public void ShowQuestDetails()
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
                this.questManager.SaveQuestsToJson(); // 🔥 변경 즉시 저장
            }

            Console.WriteLine("\n보상");
            Console.WriteLine($"아이템: {RewardItem ?? "없음"}");
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
                Console.WriteLine("보상을 받았습니다!");
                isUpdated = true;
            }

            // 🔥 변경 사항이 있을 경우만 저장
            if (isUpdated)
            {
                questManager.SaveQuestsToJson();
            }
        }

    }

    private readonly string QuestFilePath;
    private Dictionary<string, List<Quest>> QuestCategories;
    private string CharacterName; // 캐릭터 이름 저장

    public QuestManager(string characterName)
    {
        CharacterName = characterName;
        QuestFilePath = $"{CharacterName}_퀘스트.json"; // 🔥 동적 파일명 설정
        QuestCategories = LoadQuestsFromJson();
    }

    // JSON에서 퀘스트 불러오기
    private Dictionary<string, List<Quest>> LoadQuestsFromJson()
    {
        if (!File.Exists(QuestFilePath))
        {
            Console.WriteLine($"{QuestFilePath} 퀘스트 파일을 찾을 수 없습니다. 기본 퀘스트를 생성합니다.");
            return GetDefaultQuests();
        }

        try
        {
            string jsonData = File.ReadAllText(QuestFilePath);
            var loadedQuests = JsonConvert.DeserializeObject<Dictionary<string, List<Quest>>>(jsonData);

            // 🔥 JSON 로딩 후, null 체크 추가
            if (loadedQuests == null)
            {
                Console.WriteLine("⚠️ 퀘스트 데이터를 불러오는 데 실패했습니다. 기본 퀘스트를 생성합니다.");
                return GetDefaultQuests();
            }

            return loadedQuests;
        }
        catch (Exception e)
        {
            Console.WriteLine($"퀘스트 로딩 중 오류 발생: {e.Message}");
            return GetDefaultQuests();
        }
    }

    // 기본 퀘스트 데이터 설정
    private Dictionary<string, List<Quest>> GetDefaultQuests()
    {
        var defaultQuests = new Dictionary<string, List<Quest>>
        {
            { "몬스터", new List<Quest>
                {
                    new Quest(this, 1, "미니언 처치", " 몬스터가 너무 많아 10마리를 처치하세요.", 10, 0, "나무방패", 5, 3, 0, 0),
                    new Quest(this, 2, "보스 처치", " 보스를 처치하여 위협을 제거하세요.", 1, 0, "나무 칼", 5, 3, 0, 1)
                }
            },
            { "장비", new List<Quest>
                {
                    new Quest(this, 3, "무기 장비 장착", " 무기를 장착하여 전투 준비를 하세요.", 1, 0, null, 5, 3, 0, 2),
                    new Quest(this, 4, "방어구 장비 장착", " 방어구를 장착하여 방어력을 높이세요.", 1, 0, null, 5, 3, 0, 3)
                }
            },
            { "레벨업", new List<Quest>
                {
                    new Quest(this, 5, "레벨 5 달성", " 캐릭터 레벨을 5까지 올리세요.", 5, 0, "목장갑", 15, 0, 0, 5),
                    new Quest(this, 6, "레벨 10 달성", " 캐릭터 레벨을 10까지 올리세요.", 10, 0, "나무견갑", 15, 0, 0, 5)
                }
            }
        };

        SaveQuestsToJson();
        return defaultQuests;
    }

    // JSON으로 퀘스트 저장
    private void SaveQuestsToJson()
    {
        try
        {
            string jsonData = JsonConvert.SerializeObject(QuestCategories, Formatting.Indented);
            File.WriteAllText(QuestFilePath, jsonData);
            Console.WriteLine($"✅ {QuestFilePath} 저장 완료!");
        }
        catch (IOException)
        {
            Console.WriteLine($"⚠️ {QuestFilePath} 파일이 사용 중입니다. 나중에 다시 시도하세요.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"⚠️ {QuestFilePath}에 대한 쓰기 권한이 없습니다. 관리자 권한으로 실행하세요.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"퀘스트 저장 중 오류 발생: {e.Message}");
        }
    }

    //표시 메뉴 선택
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
            Console.Write("\n>> ");

            GameManager.CheckWrongInput(out int behavior, 1, 4);

            if (behavior == 4)
            {
                Console.WriteLine("퀘스트 창을 종료합니다.");
                return;
            }

            string category = behavior switch
            {
                1 => "몬스터",
                2 => "장비",
                _ => "레벨업"
            };

            if (!QuestCategories.ContainsKey(category))
            {
                Console.WriteLine($"⚠️ '{category}' 카테고리를 찾을 수 없습니다.");
                return;
            }
            ShowQuestList(category, QuestCategories[category]);

        }
    }

    //표시 메뉴 - 퀘스트 목록
    private void ShowQuestList(string category, List<Quest> quests)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"============= [{category} 퀘스트 목록] =============");

            for (int i = 0; i < quests.Count; i++)
            {
                Quest q = quests[i];
                string statusText = q.Status switch
                {
                    0 => "[미수락]",
                    1 => "[진행중]",
                    2 => "[완료]",
                    3 => "[보상 수령 완료]"
                };

                Console.WriteLine($"{i + 1}. {statusText} {q.Name} - 진행도: {q.Progress}/{q.Condition}");
            }

            Console.WriteLine("\n0. 이전 화면으로 돌아가기");
            Console.Write(">> ");

            GameManager.CheckWrongInput(out int select, 0, quests.Count);

            if (select == 0)
            {
                SaveQuestsToJson(); // 🔥 JSON에 변경 사항 저장
                break;
            }

            Quest selectedQuest = quests[select - 1];
            selectedQuest.ShowQuestDetails();

            SaveQuestsToJson(); // 🔥 퀘스트 진행 후 상태를 저장
        }
    }

    // 진행 중인 퀘스트 업데이트
    public void UpdateQuestProgress(int questType, int increase)
    {
        bool hasUpdated = false;

        foreach (var questList in QuestCategories.Values)
        {
            foreach (var quest in questList)
            {
                // 해당 타입의 퀘스트만 업데이트
                if (quest.Status == 1 && quest.Type == questType && quest.Progress < quest.Condition)
                {
                    quest.Progress = Math.Min(quest.Condition, quest.Progress + increase);
                    hasUpdated = true;

                    // 퀘스트 완료 시 상태 변경
                    if (quest.Progress >= quest.Condition)
                    {
                        quest.Status = 2;
                        Console.WriteLine($"🎉 {quest.Name} 퀘스트 완료!");
                    }
                }
            }
        }

        // 변경 사항이 있을 때만 저장
        if (hasUpdated)
        {
            SaveQuestsToJson();
        }
    }


    //조건들
    //몬스터&보스
    public void OnMonsterKilled(bool isBoss)
    {
        if (isBoss)
        {
            this.UpdateQuestProgress(1, 1); // 보스 처치 퀘스트 업데이트
        }
        else
        {
            this.UpdateQuestProgress(0, 1); // 일반 몬스터 처치 퀘스트 업데이트
        }
    }

    public void OnWeaponEquipped()
    {
        this.UpdateQuestProgress(3, 1); // 무기 장착 퀘스트 업데이트
    }

    public void OnArmorEquipped()
    {
        this.UpdateQuestProgress(4, 1); // 방어구 장착 퀘스트 업데이트
    }

    public void OnPlayerLevelUp(int newLevel)
    {
        this.UpdateQuestProgress(5, newLevel); // 현재 레벨을 증가값으로 전달
    }


}
