using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TextRPGTeam30
{
    //캐릭터 데이터 변수
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

        public DateTime LastLogin { get; set; } //시간 관련 변수
    }

    //게임 저장 시스템 매니져 클래스
    public class GameSaveManager
    {
        //저장 시스템 
        public void SaveGame(Player player)
        {
            string saveFilePath = $"{player.Name}.json"; // 캐릭터별 저장 파일

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
                Exp = player.exp,
                LastLogin = DateTime.Now
            };

            string jsonData = JsonConvert.SerializeObject(playerData, Formatting.Indented);
            File.WriteAllText(saveFilePath, jsonData);
            Console.WriteLine($"캐릭터 {player.Name}가 저장되었습니다.");

            // 캐릭터 리스트에 추가
            SaveCharacterList(player.Name, player.JobType);
        }

        //로드캐릭터 불러오기
        public Player LoadCharacter()
        {
            List<(string Name, int JobType, DateTime LastLogin)> characters = LoadCharacterList();

            while (true)
            {
                if (characters.Count > 0)
                {
                    Console.WriteLine("🔹 기존 캐릭터 목록:");
                    for (int i = 0; i < characters.Count; i++)
                    {
                        string jobName = characters[i].JobType == 0 ? "전사" : "마법사";
                        string timeAgo = FormatTimeAgo(characters[i].LastLogin);
                        Console.WriteLine($"[{i + 1}] {characters[i].Name} ({jobName}) {timeAgo}");
                    }
                    Console.WriteLine("0. 새 캐릭터 만들기");
                    Console.WriteLine("D. 캐릭터 삭제");

                    Console.Write("모험할 캐릭터를 선택하세요 (1~" + characters.Count + " / 0: 새 캐릭터 / D: 삭제): ");
                    string input = Console.ReadLine().Trim().ToUpper();

                    if (input == "D")
                    {
                        Console.Write("삭제할 캐릭터의 번호를 입력하세요 (1~" + characters.Count + "): ");
                        int choice;
                        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > characters.Count)
                        {
                            Console.WriteLine("올바른 숫자를 입력하세요.");
                        }

                        string characterToDelete = characters[choice - 1].Name;
                        DeleteCharacter(characterToDelete);
                        characters = LoadCharacterList(); // 목록을 새로 로드
                        continue; // 삭제 후 다시 선택 화면 표시
                    }
                    else if (input == "0")
                    {
                        return CreateNewCharacter(); // ✅ 새 캐릭터 생성 기능 정상 작동
                    }
                    else if (int.TryParse(input, out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= characters.Count)
                    {
                        return LoadExistingCharacter(characters[selectedIndex - 1].Name);
                    }
                    else
                    {
                        Console.WriteLine("올바른 입력이 아닙니다. 다시 입력하세요.");
                    }
                }
                else
                {
                    Console.WriteLine("저장된 캐릭터가 없습니다. 새로운 캐릭터를 생성합니다.");
                    return CreateNewCharacter();
                }
            }
        }


        // 캐릭터 리스트 파일
        private static readonly string CharacterListFile = "characters.json"; 

        //저장 캐릭터 리스트
        private void SaveCharacterList(string playerName, int jobType)
        {
            List<string> characterList = new List<string>();

            if (File.Exists(CharacterListFile))
            {
                string json = File.ReadAllText(CharacterListFile);
                characterList = JsonConvert.DeserializeObject<List<string>>(json);
            }

            if (!characterList.Contains($"{playerName},{jobType}"))
            {
                characterList.Add($"{playerName},{jobType}");
                File.WriteAllText(CharacterListFile, JsonConvert.SerializeObject(characterList, Formatting.Indented));
            }
        }

        //로드 캐릭터 리스트 불러오기
        public List<(string Name, int JobType, DateTime LastLogin)> LoadCharacterList()
        {
            List<(string Name, int JobType, DateTime LastLogin)> characters = new List<(string Name, int JobType, DateTime LastLogin)>();
            List<string> updatedCharacterList = new List<string>(); // 최신화된 리스트

            if (File.Exists(CharacterListFile))
            {
                string json = File.ReadAllText(CharacterListFile);
                List<string> characterList = JsonConvert.DeserializeObject<List<string>>(json);

                foreach (string entry in characterList)
                {
                    string[] parts = entry.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int jobType))
                    {
                        string filePath = $"{parts[0]}.json";

                        if (File.Exists(filePath))
                        {
                            DateTime lastLogin = JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(filePath)).LastLogin;
                            characters.Add((parts[0], jobType, lastLogin));
                            updatedCharacterList.Add(entry); // 유효한 캐릭터만 리스트에 추가
                        }
                    }
                }

                // 최신화된 캐릭터 목록을 다시 저장 (존재하지 않는 캐릭터 제거)
                File.WriteAllText(CharacterListFile, JsonConvert.SerializeObject(updatedCharacterList, Formatting.Indented));
            }

            return characters;
        }

        //기존 캐릭터 로드
        private Player LoadExistingCharacter(string playerName)
        {
            string filePath = $"{playerName}.json";

            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                PlayerData characterData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
                Console.WriteLine($"{characterData.Name} 캐릭터를 불러왔습니다.");

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
            else
            {
                Console.WriteLine("저장된 캐릭터 파일이 없습니다. 새로운 캐릭터를 생성합니다.");
                return CreateNewCharacter();
            }
        }

        //캐릭터 생성
        private Player CreateNewCharacter()
        {
            Console.Write("당신의 이름을 입력하세요: ");
            string name = Console.ReadLine();

            Console.Write("직업을 선택하세요 (0: 전사, 1: 마법사): ");
            int jobType;
            while (!int.TryParse(Console.ReadLine(), out jobType) || (jobType != 0 && jobType != 1))
            {
                Console.WriteLine("올바른 숫자를 입력하세요. (0: 전사, 1: 마법사)");
            }

            Player newPlayer = new Player(
                name,
                1,
                jobType == 0 ? 150 : 75,
                jobType == 0 ? 50 : 150,
                100,
                0,
                jobType == 0 ? 10 : 20,
                jobType == 0 ? 10.0f : 15.0f,
                jobType,
                jobType == 0 ? 10 : 5
            );

            SaveGame(newPlayer);
            return newPlayer;
        }

        // 마지막 접속시간 변환
        private string FormatTimeAgo(DateTime lastLogin)
        {
            if (lastLogin == DateTime.MinValue)
                return "(처음 접속)";

            TimeSpan timeDiff = DateTime.Now - lastLogin;
            if (timeDiff.TotalMinutes < 1)
                return "(방금 전)";
            if (timeDiff.TotalMinutes < 60)
                return $"({(int)timeDiff.TotalMinutes}분 전)";
            if (timeDiff.TotalHours < 24)
                return $"({(int)timeDiff.TotalHours}시간 {(int)(timeDiff.TotalMinutes % 60)}분 전)";
            return $"({(int)timeDiff.TotalDays}일 전)";
        }

        //캐릭터 삭제 기능
        public void DeleteCharacter(string playerName)
        {
            string filePath = $"{playerName}.json";

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"캐릭터 {playerName}의 저장 데이터를 삭제했습니다.");
            }
            else
            {
                Console.WriteLine($"{playerName}의 저장 데이터를 찾을 수 없습니다.");
            }

            // `characters.json`에서도 삭제
            if (File.Exists(CharacterListFile))
            {
                string json = File.ReadAllText(CharacterListFile);
                List<string> characterList = JsonConvert.DeserializeObject<List<string>>(json);

                characterList.RemoveAll(entry => entry.StartsWith(playerName + ",")); // 이름이 일치하는 캐릭터 제거

                File.WriteAllText(CharacterListFile, JsonConvert.SerializeObject(characterList, Formatting.Indented));
            }
        }


    }
}
