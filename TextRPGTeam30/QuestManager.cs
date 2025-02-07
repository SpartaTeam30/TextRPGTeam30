using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Quest
{
    public int Id { get; set; }            // 퀘스트 ID
    public string Name { get; set; }       // 퀘스트 이름
    public int Condition { get; set; }     // 완료 조건
    public int Progress { get; set; }      // 진행 상황
    public string RewardItem { get; set; } // 보상 아이템
    public int RewardGold { get; set; }    // 보상 골드
    public int Status { get; set; }        // 0: 미수락, 1: 진행중, 2: 완료
}

public class QuestManager
{
    private static readonly string QuestFilePath = "quests.json"; // JSON 파일 경로

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
    public class GuildMenu
    {
        public void ShowMenu(List<Quest> quests)
        {
            Console.WriteLine("\n========= 길드 ==========");
            foreach (var quest in quests)
            {
                string status = quest.Status switch
                {
                    0 => "",
                    1 => "[진행중]",
                    2 => "[완료]",
                    _ => ""
                };

                Console.WriteLine($"{quest.Id}. {status}{quest.Name} ({quest.Progress}/{quest.Condition})");
            }

            Console.WriteLine("\n어떤 퀘스트를 진행하시겠습니까? (선택:  )");
            Console.WriteLine("==========================");
        }

        public int SelectQuest(List<Quest> quests)
        {
            if (!int.TryParse(Console.ReadLine(), out int questId))
            {
                Console.WriteLine("올바른 숫자를 입력하세요.");
                return -1;
            }

            var quest = quests.Find(q => q.Id == questId);
            if (quest == null)
            {
                Console.WriteLine("존재하지 않는 퀘스트입니다.");
                return -1;
            }

            if (quest.Status == 2)
            {
                Console.WriteLine("이미 완료된 퀘스트입니다.");
                return -1;
            }

            if (quest.Status == 1)
            {
                Console.WriteLine("이미 진행 중인 퀘스트입니다.");
                return -1;
            }

            quest.Status = 1; // 퀘스트를 진행 상태로 변경
            Console.WriteLine($"{quest.Name} 퀘스트를 수락했습니다!");
            return quest.Id;
        }
    }
    class Program
    {
        static void Main()
        {
            QuestManager questManager = new QuestManager();
            GuildMenu guildMenu = new GuildMenu();

            List<Quest> quests = questManager.LoadQuests();

            while (true)
            {
                guildMenu.ShowMenu(quests);

                int selectedQuestId = guildMenu.SelectQuest(quests);
                if (selectedQuestId > 0)
                {
                    Console.WriteLine($"퀘스트 ID {selectedQuestId}가 진행 중입니다.");
                }

                Console.WriteLine("\n1. 퀘스트 진행 (몬스터 사냥 +3)");
                Console.WriteLine("2. 퀘스트 저장");
                Console.WriteLine("3. 종료");
                Console.Write("선택: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        var activeQuest = quests.Find(q => q.Status == 1);
                        if (activeQuest != null)
                        {
                            questManager.UpdateQuestProgress(activeQuest, 3);
                        }
                        else
                        {
                            Console.WriteLine("진행 중인 퀘스트가 없습니다.");
                        }
                        break;
                    case "2":
                        questManager.SaveQuests(quests);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("올바른 번호를 입력하세요.");
                        break;
                }
            }
        }
    }

}
