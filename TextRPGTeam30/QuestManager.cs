using Newtonsoft.Json;

namespace TextRPGTeam30
{
    public class QuestManager
    {
        public Player player { get; set; }
        private static QuestManager _instance;
        public static QuestManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QuestManager();
                }
                return _instance;
            }
        }

        private string QuestFilePath;
        private Dictionary<string, List<Quest>> QuestCategories;
        private string CharacterName; // 캐릭터 이름 저장

        private QuestManager() { } // 외부에서 생성 못하도록 private 생성자 추가

        public void Initialize(string characterName)
        {
            CharacterName = characterName;
            QuestFilePath = $"{CharacterName}_Quest.json";
            QuestCategories = LoadQuestsFromJson();
        }

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
                return loadedQuests ?? GetDefaultQuests();
            }
            catch (Exception e)
            {
                Console.WriteLine($"퀘스트 로딩 중 오류 발생: {e.Message}");
                return GetDefaultQuests();
            }
        }

        private Dictionary<string, List<Quest>> GetDefaultQuests()
        {
            var defaultQuests = new Dictionary<string, List<Quest>>
        {
            { "몬스터", new List<Quest>
                {// ID, 퀘스트 명칭, 설명, 완료조건,진행상황, 보상(아이템, 골드, 경험치, 퀘스트 상태, 퀘스트타입)
                    new Quest(1, "일반 몬스터 처치", " 몬스터가 너무 많아 10마리를 처치하세요.", 10, 0,
                        new Armor("녹색 망토", 10, "방어력", "숲에서 몸을 숨기고 기습하는 데에 최적인 녹색 망토.", 100), 50, 3, 0, 0),
                    
                    new Quest(2, "보스 처치", " 보스를 처치하여 위협을 제거하세요.", 1, 0, null, 50, 3, 0, 1)
                }
            },
            { "장비", new List<Quest>
                {
                    new Quest(3, "무기 장비 장착", " 무기를 장착하여 전투 준비를 하세요.", 1, 0, null, 50, 3, 0, 3),
                    new Quest(4, "방어구 장비 장착", " 방어구를 장착하여 방어력을 높이세요.", 1, 0, null, 50, 3, 0, 4)
                }
            },
            { "레벨업", new List<Quest>
                {
                    new Quest(5, "레벨 5 달성", " 캐릭터 레벨을 5까지 올리세요.", 5, 1, null, 0, 0, 1, 5),
                    new Quest(6, "레벨 10 달성", " 캐릭터 레벨을 10까지 올리세요.", 10, 1, null, 0, 0, 1, 5)
                }
            }
        };

            SaveQuestsToJson();
            return defaultQuests;
        }

        private void SaveQuestsToJson()
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(QuestCategories, Formatting.Indented);
                File.WriteAllText(QuestFilePath, jsonData);
                //Console.WriteLine($"✅ {QuestFilePath} 저장 완료!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"퀘스트 저장 중 오류 발생: {e.Message}");
            }
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
                Console.WriteLine("0. 퀘스트 창 나가기");
                Console.Write("\n>> ");

                GameManager.CheckWrongInput(out int behavior, 0, 3);

                if (behavior == 0)
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
                    SaveQuestsToJson();
                    break;
                }

                Quest selectedQuest = quests[select - 1];

                // 🔥 퀘스트 수락 시, 플레이어 레벨 및 장착 상태 확인
                if (selectedQuest.Status == 1) 
                {
                    // 현재 플레이어 상태를 체크하여 즉시 퀘스트 진행도 반영
                    if (selectedQuest.Type == 3 && GameManager.Instance.player.equipWeapon != null)
                    {
                        Console.WriteLine("현재 무기를 장착 중이므로 퀘스트 완료!");
                        OnWeaponEquipped();
                    }
                    else if (selectedQuest.Type == 4 && GameManager.Instance.player.equipArmor != null)
                    {
                        Console.WriteLine("현재 방어구를 장착 중이므로 퀘스트 완료!");
                        OnArmorEquipped();
                    }
                    else if (selectedQuest.Type == 5) // 🔥 레벨업 퀘스트
                    {
                        int playerLevel = GameManager.Instance.player.Level;
                        Console.WriteLine($"현재 레벨: {playerLevel}");

                        if (playerLevel >= selectedQuest.Condition) // 목표 레벨 도달 여부 체크
                        {
                            Console.WriteLine($"레벨 {selectedQuest.Condition} 달성! 퀘스트 완료!");
                            UpdateQuestProgress(5, playerLevel);
                        }
                    }

                    SaveQuestsToJson();
                }

                bool isupdate = selectedQuest.ShowQuestDetails();

                if (isupdate)
                {
                    SaveQuestsToJson();
                }
            }
        }



        public void UpdateQuestProgress(int questType, int increase)
        {
            bool hasUpdated = false;

            foreach (var questList in QuestCategories.Values)
            {
                foreach (var quest in questList)
                {
                    if (quest.Status == 1 && quest.Type == questType && quest.Progress < quest.Condition)
                    {
                        quest.Progress = Math.Min(quest.Condition, quest.Progress + increase);
                        hasUpdated = true;

                        if (quest.Progress >= quest.Condition)
                        {
                            quest.Status = 2;
                            Console.WriteLine($"🎉 {quest.Name} 퀘스트 완료!");
                        }
                    }
                }
            }

            if (hasUpdated)
            {
                SaveQuestsToJson();
            }
        }

        public void OnMonsterKilled(bool isBoss)
        {
            UpdateQuestProgress(isBoss ? 1 : 0, 1);
        }

        public void OnWeaponEquipped()
        {
            UpdateQuestProgress(3, 1);
        }

        public void OnArmorEquipped()
        {
            UpdateQuestProgress(4, 1);
        }

        public void OnPlayerLevelUp()
        {
            UpdateQuestProgress(5, 1);
        }
    }
}
