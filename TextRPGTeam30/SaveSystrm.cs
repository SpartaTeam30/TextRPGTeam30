using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace TextRPGTeam30
{
    //플레이어 변수
    public class PlayerData
    {
        public string Name { get; set; } //이름
        public string job { get; set; } //직업
        public float Attack { get; set; } //공격력
        public int Level { get; set; } //레벨
        public int Defense { get; set; } //방어력
        public int Hp { get; set; } // 체력
        public int Mp { get; set; } //마나
        public int CritRate { get; set; } //크리티컬
        public int Gold { get; set; } //골드
        public int Exp { get; set; } //경험치
        // public int CritDamage { get; set; } //크리티컬 데미지
        public int Evasion { get; set; } // 회피

        //public List<ItemData> Inventory { get; set; } // 플레이어 아이템 리스트
        //public List<ItemData> Quest { get; set; }
        /*
           public PlayerData()
            {
             Inventory = new List<ItemData>(); // 기본값으로 빈 리스트 생성
            }
        */
    }

    /*
    //아이템 저장 (예시)
    public class ItemData
    {
        public string Name { get; set; }        // 아이템 이름
        public string Explanation { get; set; } // 아이템 설명
        public string Type { get; set; }        // 아이템 타입 (소비형, 장비형 등)
        public int ItemCount { get; set; }      // 아이템 개수
        public bool IsEquipped { get; set; }    // 장비 착용 여부
    }
    */
    //저장&불러오기 스택틱으로수정예정

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
                    Job = Warrior,
                    Level = 1,
                    Hp = 150, // 전사는 체력 많음
                    Mp = 80, // 마법사는 마나 많음
                    Attack = 10.0f, // 전사는 공격력 높음
                    Defense = 5, // 전사는 방어력 높음
                    CritRate = 10, // 마법사는 크리티컬 확률 높음
                    //CritDamage = 50, // 기본 크리티컬 데미지
                    Evasion = 5, // 마법사는 회피율 높음
                    Gold = 100,
                    Exp = 0
                    //Inventory = new List<ItemData>()
                    //퀘스트 변수리스트
                };

                SaveGame(newCharacter); // 새 캐릭터 즉시 저장
                return newCharacter;
            }
        }

    }
}


