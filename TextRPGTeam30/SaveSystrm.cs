using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TextRPGTeam30
{
    public class PlayerData
    {
        public string Name { get; set; }
        public int JobType { get; set; }
        public float Attack { get; set; }
        public int Level { get; set; }
        public int Defense { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int CritRate { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int Evasion { get; set; }
    }

    public class GameSaveManager
    {
        private static readonly string SaveFilePath = "save.json";

        public void SaveGame(Player player)
        {
            PlayerData playerData = new PlayerData
            {
                Name = player.Name,
                JobType = player.JobType,
                Level = player.Level,
                Hp = player.Hp,
                Mp = player.mp,
                Attack = player.Attack,
                Defense = player.Defense,
                CritRate = player.CritRate,
                Evasion = player.Evasion,
                Gold = player.gold,
                Exp = player.exp
            };

            string jsonData = JsonConvert.SerializeObject(playerData, Formatting.Indented);
            File.WriteAllText(SaveFilePath, jsonData);
            Console.WriteLine("게임이 저장되었습니다.");
        }

        public Player LoadCharacter()
        {
            PlayerData characterData;

            if (File.Exists(SaveFilePath))
            {
                string jsonData = File.ReadAllText(SaveFilePath);
                characterData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
                Console.WriteLine("저장된 캐릭터를 불러왔습니다.");
            }
            else
            {
                Console.WriteLine("저장된 캐릭터가 없습니다. 캐릭터를 새로 만들어야 합니다.");
                Console.Write("당신의 이름을 입력하세요: ");
                string name = Console.ReadLine();

                Console.Write("직업을 선택하세요 (0: 전사, 1: 마법사): ");
                int jobType;
                while (!int.TryParse(Console.ReadLine(), out jobType) || (jobType != 0 && jobType != 1))
                {
                    Console.WriteLine("올바른 숫자를 입력하세요. (0: 전사, 1: 마법사)");
                }

                characterData = new PlayerData
                {
                    Name = name,
                    JobType = jobType,
                    Level = 1,
                    Hp = jobType == 0 ? 150 : 75,
                    Mp = jobType == 0 ? 50 : 150,
                    Attack = jobType == 0 ? 10.0f : 15.0f,
                    Defense = jobType == 0 ? 10 : 5,
                    CritRate = jobType == 0 ? 10 : 20,
                    Evasion = jobType == 0 ? 5 : 10,
                    Gold = 100,
                    Exp = 0
                };

                SaveGame(new Player(
                    characterData.Name,
                    characterData.Level,
                    characterData.Hp,
                    characterData.Mp,
                    characterData.Gold,
                    characterData.Exp,
                    characterData.CritRate,
                    characterData.Attack,
                    characterData.JobType,
                    characterData.Defense
                ));
            }

            return new Player(
                characterData.Name,
                characterData.Level,
                characterData.Hp,
                characterData.Mp,
                characterData.Gold,
                characterData.Exp,
                characterData.CritRate,
                characterData.Attack,
                characterData.JobType,
                characterData.Defense
            );
        }
    }
}
