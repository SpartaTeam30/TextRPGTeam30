using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

internal class QuestManager
{
    static void Main(string[] args)
    {
        string behavior = "";

        // 카테고리별 퀘스트를 Dictionary로 관리
        Dictionary<string, List<Quest>> QuestCategories = new Dictionary<string, List<Quest>>()
            {
                { "몬스터", new List<Quest>
                    {
                        new Quest(1, "미니언 처치", 10, 0, "나무방패", 5, 3, 0),
                        new Quest(2, "보스 처치", 1, 0, "나무 칼", 5, 3, 0)
                    }
                },
                { "장비", new List<Quest>
                    {
                        new Quest(3, "무기 장비 장착", 1, 0, null, 5, 3, 0),
                        new Quest(4, "방어구 장비 장착", 1, 0, null, 5, 3, 0)
                    }
                },
                { "레벨업", new List<Quest>
                    {
                        new Quest(5, "레벨 5 달성", 5, 0, "목장갑", 15, 0, 0),
                        new Quest(6, "레벨 10 달성", 10, 0, "나무견갑", 15, 0, 0)
                    }
                }
            };

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

            behavior = Console.ReadLine();

            switch (behavior)
            {
                case "1":
                    ShowQuestList("몬스터", QuestCategories["몬스터"]);
                    break;
                case "2":
                    ShowQuestList("장비", QuestCategories["장비"]);
                    break;
                case "3":
                    ShowQuestList("레벨업", QuestCategories["레벨업"]);
                    break;
                case "4":
                    Console.WriteLine("퀘스트 창을 종료합니다.");
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
                    break;
            }
        }
    }

    // 특정 카테고리의 퀘스트 목록을 보여주는 함수
    static void ShowQuestList(string category, List<Quest> quests)
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
                                                    "[완료]";

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

            ShowQuestDetails(quests[select - 1]);
        }
    }

    // 퀘스트 상세 정보 및 수락/거절 기능
    static void ShowQuestDetails(Quest quest)
    {
        Console.Clear();
        Console.WriteLine("============= [퀘스트 상세 정보] =============");
        Console.WriteLine($"퀘스트 이름: {quest.Name}");
        Console.WriteLine($"진행도: {quest.Progress}/{quest.Condition}");
        Console.WriteLine("");
        Console.WriteLine("보상");
        Console.WriteLine($"아이템: {quest.RewardItem ?? "없음"}");
        Console.WriteLine($"골드: {quest.RewardGold} G");
        Console.WriteLine($"경험치: {quest.RewardExp} EXP");
        Console.WriteLine("==============================================");
        Console.WriteLine("1. 수락");
        Console.WriteLine("2. 거절");
        Console.Write(">> ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine($"{quest.Name} 퀘스트를 수락하였습니다.");
                quest.Status = 1;
                break;
            case "2":
                Console.WriteLine($"{quest.Name} 퀘스트를 거절하였습니다.");
                quest.Status = 0;
                break;
            default:
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                break;
        }
    }

    // 퀘스트 클래스
    public class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Condition { get; set; }
        public int Progress { get; set; }
        public string RewardItem { get; set; }
        public int RewardGold { get; set; }
        public int RewardExp { get; set; }
        public int Status { get; set; }

        public Quest(int id, string name, int condition, int progress, string rewarditem, int rewardgold, int rewardexp, int status)
        {
            Id = id;
            Name = name;
            Condition = condition;
            Progress = progress;
            RewardItem = rewarditem;
            RewardGold = rewardgold;
            RewardExp = rewardexp;
            Status = status;
        }
    }
}
