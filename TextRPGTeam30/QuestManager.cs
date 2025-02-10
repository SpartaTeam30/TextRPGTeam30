using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace TextRPGTeam30
{
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
    }
}
