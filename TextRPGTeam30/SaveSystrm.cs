using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace TextRPGTeam30
{
    //플레이어 변수
    public class PlayerData
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public float Attack { get; set; }
        public int Crit { get; set; }
        public int Mp { get; set; }
        public int Gold { get; set; }
        public int Gob { get; set; }
        public int Exp { get; set; }

        public List<ItemData> Inventory { get; set; } // 플레이어 아이템 리스트
        //public List<ItemData> Quest { get; set; }

        public PlayerData()
        {
            Inventory = new List<ItemData>(); // 기본값으로 빈 리스트 생성
        }
    }

    //아이템 저장 (예시)
    public class ItemData
    {
        public string Name { get; set; }        // 아이템 이름
        public string Explanation { get; set; } // 아이템 설명
        public string Type { get; set; }        // 아이템 타입 (소비형, 장비형 등)
        public int ItemCount { get; set; }      // 아이템 개수
        public bool IsEquipped { get; set; }    // 장비 착용 여부
    }

    //게임 저장 클래스 저장&불러오기
    public class GameSaveManager
    {
        private static readonly string SaveFilePath = "save.json"; // 저장 파일 경로

        // 게임 데이터를 JSON 파일에 저장
        public void SaveGame(PlayerData player)
        {
            string jsonData = JsonConvert.SerializeObject(player, Formatting.Indented);
            File.WriteAllText(SaveFilePath, jsonData);
            Console.WriteLine("게임이 저장되었습니다.");
        }

        // JSON 파일에서 게임 데이터를 불러오기
        public PlayerData LoadCharacter()
        {
            if (File.Exists(SaveFilePath))
            {
                string jsonData = File.ReadAllText(SaveFilePath);
                PlayerData character = JsonConvert.DeserializeObject<PlayerData>(jsonData);
                Console.WriteLine("저장된 캐릭터를 불러왔습니다.");
                return character;
            }
            else
            {
                Console.WriteLine("저장된 캐릭터가 없습니다. 캐릭터를 새로 만들어야 합니다.");
                Console.Write("당신의 이름을 입력하세요: ");
                string name = Console.ReadLine();

                // 새로운 캐릭터 생성
                PlayerData newCharacter = new PlayerData
                {
                    Name = name,
                    Hp = 100,
                    Attack = 10.0f,
                    Crit = 5,
                    Mp = 50,
                    Gold = 100,
                    Gob = 0,
                    Exp = 0,
                    Inventory = new List<ItemData>()
                    //퀘스트 변수리스트
                };

                SaveGame(newCharacter); // 새 캐릭터 즉시 저장
                return newCharacter;
            }
        }

    }
}
