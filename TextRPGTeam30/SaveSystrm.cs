using System.Numerics;
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
        public int MaxHP { get; set; }
        public int Mp { get; set; }
        public int MaxMP { get; set; }
        public int CritRate { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int Evasion { get; set; }
        public int Stage { get; set; } = 1;
        public List<string> Skills { get; set; }
        public List<string> Inventory { get; set; } = new List<string>();


        public DateTime LastLogin { get; set; } //시간 관련 변수
    }
    public class GameSaveManager
    {
        //저장 시스템 
        public void SaveGame(Player player)
        {
            string saveFilePath = $"{player.Name}.json";

            string jobName = player.JobType == 0 ? "전사" : "마법사";

            PlayerData playerData = new PlayerData
            {
                Name = player.Name,
                JobType = player.JobType,
                Level = player.Level,
                Hp = player.Hp,       //  현재 체력 유지
                MaxHP = player.MaxHP, //  최대 체력 유지 (레벨업할 때만 변경)
                Mp = player.mp,       //  현재 마나 유지
                MaxMP = player.maxMp, // 최대 마나 유지 (레벨업할 때만 변경)
                Attack = (float)Math.Round(player.Attack, 1),
                Defense = player.Defense,
                CritRate = player.CritRate,
                Evasion = player.Evasion,
                Gold = player.gold,
                Exp = player.exp,
                LastLogin = DateTime.Now,
                Stage = player.Stage,
                Skills = player.job.skills.Select(skill => skill.name).ToList(),
                Inventory = player.inventory.Select(item =>
                    item is Consumable consumable ? $"{item.itName},{consumable.itemCount}" : $"{item.itName},1"
        ).ToList()
            };

            string jsonData = JsonConvert.SerializeObject(playerData, Formatting.Indented);
            File.WriteAllText(saveFilePath, jsonData);
        }


        //로드캐릭터 불러오기
        public Player LoadCharacter()
        {
            List<(string Name, int JobType, DateTime LastLogin, int Stage)> characters = LoadCharacterList();

            if (characters.Count > 0)
            {
                Console.WriteLine("기존 캐릭터 목록:");
                for (int i = 0; i < characters.Count; i++)
                {
                    string jobName = characters[i].JobType == 0 ? "전사" : "마법사";
                    string timeAgo = FormatTimeAgo(characters[i].LastLogin);
                    Console.WriteLine($"[{i + 1}] {characters[i].Name} ({jobName}) - 스테이지 {characters[i].Stage} {timeAgo}");
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
                    return LoadCharacter(); // 다시 선택 화면 표시
                }
                else if (input == "0")
                {
                    return CreateNewCharacter();
                }
                else if (int.TryParse(input, out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= characters.Count)
                {
                    return LoadExistingCharacter(characters[selectedIndex - 1].Name);
                }
                else
                {
                    Console.WriteLine("올바른 입력이 아닙니다. 다시 입력하세요.");
                    return LoadCharacter();
                }
            }
            else
            {
                Console.WriteLine("저장된 캐릭터가 없습니다. 새로운 캐릭터를 생성합니다.");
                return CreateNewCharacter();
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
        public List<(string Name, int JobType, DateTime LastLogin, int Stage)> LoadCharacterList()
        {
            List<(string Name, int JobType, DateTime LastLogin, int Stage)> characters = new List<(string Name, int JobType, DateTime LastLogin, int Stage)>();

            if (File.Exists(CharacterListFile))
            {
                string json = File.ReadAllText(CharacterListFile);
                List<string> characterList = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();

                foreach (string entry in characterList)
                {
                    string[] parts = entry.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int jobType))
                    {
                        string filePath = $"{parts[0]}.json";

                        if (File.Exists(filePath))
                        {
                            PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(filePath));
                            characters.Add((parts[0], jobType, playerData.LastLogin, playerData.Stage));
                        }
                    }
                }
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

                string jobName = characterData.JobType == 0 ? "전사" : "마법사";

                // ✅ 기존 HP & MP 유지 (던전 진행 후 깎인 상태 반영)
                Player player = new Player(
                    characterData.Name,
                    characterData.Level,
                    characterData.Hp,    
                    characterData.MaxHP, 
                    characterData.Mp,    
                    characterData.MaxMP, 
                    characterData.Gold,
                    characterData.Exp,
                    characterData.CritRate,
                    (float)characterData.Attack,
                    characterData.JobType,
                    characterData.Defense,
                    characterData.Stage
                );

                player.inventory.Clear();
                foreach (var itemData in characterData.Inventory)
                {
                    string[] itemInfo = itemData.Split(',');
                    if (itemInfo.Length == 2)
                    {
                        string itemName = itemInfo[0];
                        int itemCount = int.TryParse(itemInfo[1], out int count) ? count : 1;

                        Item item = CreateItemFromName(itemName);
                        if (item != null)
                        {
                            if (item is Consumable consumable)
                            {
                                consumable.itemCount = itemCount; // 포션 개수 설정
                            }

                            player.inventory.Add(item);
                        }
                    }
                }

                // ✅ 기존 스킬 초기화 후 로드
                player.job.skills.Clear();
                foreach (var skillName in characterData.Skills)
                {
                    Skill skill = CreateSkillFromName(skillName);
                    if (skill != null) player.job.skills.Add(skill);
                }

                return player;
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

            int baseHp = jobType == 0 ? 150 : 75;
            int baseMp = jobType == 0 ? 50 : 150;
            int baseAttack = jobType == 0 ? 10 : 20;
            int baseDefense = jobType == 0 ? 10 : 5;

            Player newPlayer = new Player(
                name,
                1,             // Level
                baseHp,        // Hp
                baseHp,        // MaxHP  최대 체력 추가
                baseMp,        // Mp
                baseMp,        // MaxMP  최대 마나 추가
                100,           // Gold
                0,             // Exp
                10,            // CritRate
                baseAttack,    // Attack
                baseDefense,   // Defense
                jobType,       // JobType
                1              // Stage  기본 스테이지 추가
            );

            SaveGame(newPlayer); // ✅ 새로운 캐릭터 저장
            SaveCharacterList(name, jobType); // ✅ 캐릭터 슬롯 추가

            return newPlayer;
        }


        //스킬생성
        private Skill CreateSkillFromName(string skillName)
        {
            switch (skillName)
            {
                case "베기":
                    return new Slash();
                case "화염구":
                    return new Fireball();
                default:
                    Console.WriteLine($"알 수 없는 스킬: {skillName}");
                    return null;
            }
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
            string playerFilePath = $"{playerName}.json";
            string questFilePath = $"{playerName}_Quest.json"; // ✅ 퀘스트 데이터 파일 경로

            // ✅ 캐릭터 저장 데이터 삭제
            if (File.Exists(playerFilePath))
            {
                File.Delete(playerFilePath);
                Console.WriteLine($"캐릭터 {playerName}의 저장 데이터를 삭제했습니다.");
            }
            else
            {
                Console.WriteLine($"{playerName}의 저장 데이터를 찾을 수 없습니다.");
            }

            // ✅ 퀘스트 데이터 삭제
            if (File.Exists(questFilePath))
            {
                File.Delete(questFilePath);
                Console.WriteLine($"{playerName}의 퀘스트 데이터를 삭제했습니다.");
            }
            else
            {
                Console.WriteLine($"{playerName}의 퀘스트 데이터를 찾을 수 없습니다.");
            }

            // ✅ 캐릭터 목록에서도 삭제
            if (File.Exists(CharacterListFile))
            {
                string json = File.ReadAllText(CharacterListFile);
                List<string> characterList = JsonConvert.DeserializeObject<List<string>>(json);

                characterList.RemoveAll(entry => entry.StartsWith(playerName + ",")); // 이름이 일치하는 캐릭터 제거

                File.WriteAllText(CharacterListFile, JsonConvert.SerializeObject(characterList, Formatting.Indented));
                Console.WriteLine($"{playerName}이(가) 캐릭터 목록에서 삭제되었습니다.");
            }
            LoadCharacterList();
        }

        //던전 저장
        public void SaveDungeonClearData(Player player)
        {
            player.Stage++; //  스테이지 증가

            string saveFilePath = $"{player.Name}.json";

            if (File.Exists(saveFilePath))
            {
                string jsonData = File.ReadAllText(saveFilePath);
                PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);

                playerData.Hp = player.Hp; // 현재 체력 저장
                playerData.Mp = player.mp; // 현재 마나 저장
                playerData.Stage = player.Stage; // 스테이지 증가 반영

                jsonData = JsonConvert.SerializeObject(playerData, Formatting.Indented);
                File.WriteAllText(saveFilePath, jsonData);

                Console.WriteLine($"✅ {player.Name}의 진행 정보가 저장되었습니다. (스테이지: {player.Stage}, 체력: {player.Hp}, 마나: {player.mp})");
            }
        }

        //아이템
        private Item CreateItemFromName(string itemName)
        {
            switch (itemName)
            {
                case "본 헬름":
                    return new Armor("본 헬름", 30, "방어력", "동물의 뼈를 이용하여 악마의 머리 모양으로 깎아놓은 투구.", 10, 100);
                case "아론다이트":
                    return new Weapon("아론다이트", 40, "공격력", "원탁의 기사단 단장 란슬롯이 사용했다는 중세 시대의 검.", 10, 100);
                case "브리간딘 갑옷":
                    return new Armor("브리간딘 갑옷", 35, "방어력", "부드러운 가죽이나 천 안쪽에 작은 쇠판을 리벳으로 고정시킨 형태의 갑옷.", 15, 100);
                case "건틀렛":
                    return new Armor("건틀렛", 25, "방어력", "철로 만들어진 전투용 장갑.", 5, 100);
                case "이더 부츠":
                    return new Armor("이더 부츠", 10, "방어력", "가죽으로 만든 목이 긴 부츠.", 7, 100);
                case "녹색 망토":
                    return new Armor("녹색 망토", 20, "방어력", "숲에서 몸을 숨기고 기습하는 데에 최적인 녹색 망토.", 20, 100);
                case "체력 물약":
                    return new HealingPotion("체력 물약", 30, "체력 회복", "마시면 체력이 회복된다.", 100, 2);
                case "마나 물약":
                    return new ManaPotion("마나 물약", 30, "마나 회복", "마시면 마나가 회복된다.", 100, 1);

                default:
                    Console.WriteLine($"⚠ 알 수 없는 아이템: {itemName}");

                    return null;
            }
        }
        //최대 체력마나 저장
        public void SaveMaxHPMP(Player player)
        {
            string saveFilePath = $"{player.Name}.json";

            if (File.Exists(saveFilePath))
            {
                string jsonData = File.ReadAllText(saveFilePath);
                PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);

                playerData.MaxHP = player.MaxHP; // 레벨업한 최대 체력 저장
                playerData.MaxMP = player.maxMp; // 레벨업한 최대 마나 저장

                jsonData = JsonConvert.SerializeObject(playerData, Formatting.Indented);
                File.WriteAllText(saveFilePath, jsonData);
            }
        }


    }
}
