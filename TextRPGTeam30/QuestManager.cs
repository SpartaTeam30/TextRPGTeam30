using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TextRPGTeam30;

internal class QuestManager
{
    private static readonly string QuestFilePath = "quests.json";
    private Dictionary<string, List<Quest>> QuestCategories;

    public QuestManager()
    {
        QuestCategories = LoadQuestsFromJson();
    }

    // JSON에서 퀘스트 불러오기
    private Dictionary<string, List<Quest>> LoadQuestsFromJson()
    {
        if (!File.Exists(QuestFilePath))
        {
            Console.WriteLine("퀘스트 파일을 찾을 수 없습니다. 기본 퀘스트를 생성합니다.");
            return GetDefaultQuests();
        }

        try
        {
            string jsonData = File.ReadAllText(QuestFilePath);
            return JsonConvert.DeserializeObject<Dictionary<string, List<Quest>>>(jsonData) ?? GetDefaultQuests();
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
                    new Quest(1, "미니언 처치", " 몬스터가 너무 많아 10마리를 처치하세요.", 10, 0, "나무방패", 5, 3, 0),
                    new Quest(2, "보스 처치", " 보스를 처치하여 위협을 제거하세요.", 1, 0, "나무 칼", 5, 3, 0)
                }
            },
            { "장비", new List<Quest>
                {
                    new Quest(3, "무기 장비 장착", " 무기를 장착하여 전투 준비를 하세요.", 1, 0, null, 5, 3, 0),
                    new Quest(4, "방어구 장비 장착", " 방어구를 장착하여 방어력을 높이세요.", 1, 0, null, 5, 3, 0)
                }
            },
            { "레벨업", new List<Quest>
                {
                    new Quest(5, "레벨 5 달성", " 캐릭터 레벨을 5까지 올리세요.", 5, 0, "목장갑", 15, 0, 0),
                    new Quest(6, "레벨 10 달성", " 캐릭터 레벨을 10까지 올리세요.", 10, 0, "나무견갑", 15, 0, 0)
                }
            }
        };

        SaveQuestsToJson(defaultQuests);
        return defaultQuests;
    }

    // JSON으로 퀘스트 저장
    private void SaveQuestsToJson(Dictionary<string, List<Quest>> quests)
    {
        try
        {
            string jsonData = JsonConvert.SerializeObject(quests, Formatting.Indented);
            File.WriteAllText(QuestFilePath, jsonData);
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
            Console.WriteLine("4. 퀘스트 창 나가기");
            Console.Write("\n>> ");

            GameManager.CheckWrongInput(out int behavior, 1, 4);

            if (behavior == 4)
            {
                Console.WriteLine("퀘스트 창을 종료합니다.");
                return;
            }

            string category = behavior == 1 ? "몬스터" :
                              behavior == 2 ? "장비" : "레벨업";

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
                    _ => "[보상 수령 완료]"
                };

                Console.WriteLine($"{i + 1}. {statusText} {q.Name} - 진행도: {q.Progress}/{q.Condition}");
            }

            Console.WriteLine("\n0. 이전 화면으로 돌아가기");
            Console.Write(">> ");

            GameManager.CheckWrongInput(out int select, 0, quests.Count);

            if (select == 0) break;

            quests[select - 1].ShowQuestDetails();
        }
    }
}

public class Quest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Condition { get; set; }
    public int Progress { get; set; }
    public string RewardItem { get; set; }
    public int RewardGold { get; set; }
    public int RewardExp { get; set; }
    public int Status { get; set; }

    public Quest(int id, string name, string description, int condition, int progress, string rewardItem, int rewardGold, int rewardExp, int status)
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
    }

    public void ShowQuestDetails()
    {
        Console.Clear();
        Console.WriteLine("============= [퀘스트 상세 정보] =============");
        Console.WriteLine($"퀘스트 이름: {Name}");
        Console.WriteLine($"설명: {Description}");
        Console.WriteLine($"진행도: {Progress}/{Condition}");

        if (Progress >= Condition && Status == 1)
        {
            Console.WriteLine("\n[!] 퀘스트 목표를 달성했습니다!");
            Status = 2;
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

        if (Status == 0 && choice == 1) Status = 1;
        else if (Status == 1 && choice == 1) Status = 0;
        else if (Status == 2 && choice == 1) { Status = 3; Console.WriteLine("보상을 받았습니다!"); }

        ShowQuestDetails();
    }
}
