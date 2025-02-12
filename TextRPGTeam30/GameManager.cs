namespace TextRPGTeam30
{
    public class GameManager
    {
        public Player player;
        public DungeonManager dManager;
        public Shop shop;
        public static GameManager Instance { get; private set; }

        public GameManager()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            PrintStartScene();
        }

        public static void CheckWrongInput(out int select, int minN, int maxN)//입력 예외처리
        {
            while (true)
            {
                Console.Write("\n원하시는 행동을 입력해주세요.(숫자로 입력): ");
                bool rightInput = int.TryParse(Console.ReadLine(), out select);

                if (!rightInput)
                {
                    Console.WriteLine("입력이 잘못되었습니다. 다시 입력해주세요.");
                    continue;
                }
                if (select < minN || select > maxN)
                {
                    Console.WriteLine($"{minN}~{maxN}의 숫자를 입력해주세요.");
                    continue;
                }
                return;
            }
        }

        public static void PrintColored(string message, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            ConsoleColor originalForeground = Console.ForegroundColor;
            ConsoleColor originalBackground = Console.BackgroundColor;
            try
            {
                Console.ForegroundColor = foreground;
                Console.BackgroundColor = background;

                Console.Write(message);
            }
            finally
            {
                Console.ForegroundColor = originalForeground;
                Console.BackgroundColor = originalBackground;
            }
        }

        public static void PrintColoredLine(string message, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            PrintColored(message, foreground, background);
            Console.WriteLine();
        }

        public void PrintStartScene()
        {
            Console.Clear();

            GameSaveManager saveManager = new GameSaveManager();
            player = saveManager.LoadCharacter();
            dManager = new DungeonManager(player);
            QuestManager.Instance.Initialize(player.Name);
            shop = new Shop(player);
            QuestManager.Instance.player = player;

            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Thread.Sleep(500);
        }

        public void StartSelect()
        {
            Console.Clear();
            PrintColoredLine("마을", ConsoleColor.Green);
            Console.WriteLine("이곳에서는 다양한 활동을 할 수 있습니다.\n");
            GameSaveManager saveManager = new GameSaveManager();
            saveManager.SaveGame(player, player.JobType);

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리 보기");
            Console.WriteLine("3. 전투 시작");
            Console.WriteLine("4. 퀘스트");
            Console.WriteLine("5. 상점");
            Console.WriteLine("0. 종료하기");
            CheckWrongInput(out int select, 0, 5);


            switch (select)
            {
                case 1:
                    player.DisplayStatus();
                    break;
                case 2:
                    player.DisplayInventory();
                    break;
                case 3:
                    dManager.DungeonStart();
                    break;
                case 4:
                    QuestManager.Instance.Questscreen();
                    break;
                case 5:
                    shop.PrintShop();
                    break;
                case 0:
                    Console.WriteLine("게임을 저장하고 종료합니다...");
                    saveManager.SaveGame(player, player.JobType);
                    Environment.Exit(0);
                    break;
            }
        }

        private static List<String> startStory = new List<String>()//게임 시작 스토리
        {
            "아주 오래전, 마왕이 인간 세계를 침략했습니다."
            ,"그리고 많은 시간이 흐른 뒤,"
            ,"아무도 손에 넣지 못했던 전설의 성검을 당신이 뽑아냈습니다."
            ,"용사가 된 당신은 긴 여정을 거쳐 마침내 마왕성에 도착했습니다."
            ,"웅장한 성을 올려다보며, 당신은 반드시 마왕을 쓰러뜨리겠다고 굳게 다짐합니다."
            ,"\n"
            ,"> 모험을 시작하려면 Enter를 입력하세요."
        };

        private static List<String> rememberStory = new List<String>()//던전 10층 스토리(20층이 보스)
        {
            "마왕성 내부는 어둠 속에서 기묘한 아름다움을 뽐내고 있습니다."
            ,"계단을 오르던 중, 화려하게 빛나는 촛불이 당신의 시선을 이끌었습니다."
            ,"화려한 세공이 들어간 촛대를 보자, 당신은 알 수 없는 기억이 떠오릅니다."
            ,"어째서 이 곳에 와 봤던 것 같은 기분이 드는 걸까요?"
            ,"당신은 알 수 없는 감정을 느끼며 검을 고쳐 잡고 다시 발걸음을 옮깁니다."
            ,"\n"
            ,"> 모험을 계속하려면 Enter를 입력하세요."
        };

        private static List<String> meetStory = new List<String>()//던전 보스 입장 스토리
        {
            "마침내 마왕성의 가장 높은 곳에 도착한 당신,"
            ,"그 곳에는 마왕이 당신을 기다리고 있었습니다."
            ,"당신은 순간 이상한 기분을 느꼈지만 그럼에도 손에 쥔 검을 흔들림 없이 마왕을 향해 겨눕니다."
            ,"\n"
            ,"> 모험을 계속하려면 Enter를 입력하세요."
        };

        private static List<String> endStory = new List<String>()//던전 보스 클리어 스토리
        {
            "용사의 차갑고 날카로운 칼날이 마왕의 심장을 관통했습니다."
            ,"\"용사여.\""
            ,"마왕이 무언가 할 말이 있는 듯 합니다."
            ,"마왕은 정신을 차린 지금, 단 한 번이라도 좋으니 진실을 전하고 싶었습니다."
            ,"\"나는-\""
            ,"그러나 용사는 마왕의 말을 들을 새도 없이 칼을 비틀었습니다."
            ,"용사는 마왕을 기억하지 못하니까요."
            ,"마왕의 말은 그저 눈물이 되어 흐를 뿐이었습니다."
            ,"마지막 순간, 마왕의 손이 용사의 뺨을 스쳤습니다."
            ,"용사는 그 순간 무언가가 떠오르는 듯한 기분을 느꼈습니다."
            ,"하지만 그것이 무엇인지 알기도 전에, 마왕은 점차 사라지기 시작했습니다."
            ,"\n"
            ,"당신은 검을 늘어트리고 거칠게 숨을 몰아쉬었습니다."
            ,"드디어 마왕을 처치하셨군요. 당신은 세상을 구했습니다."
            ,"하지만 왠지 가슴 한 구석이 쓰라립니다. 격한 전투 중에 상처라도 입은 걸까요?."
            ,"당신은 일단 마을로 돌아가기로 합니다."
            ,"\n"
            ,"> 모험을 끝마치려면 Enter를 입력하세요."
        };

        private static bool startFlag = false;
        private static bool rememberFlag = false;
        private static bool meetFlag = false;
        private static bool endFlag = false;

        public static void PrintStartStory()
        {
            if (startFlag) return;

            Console.Clear();
            foreach (string s in startStory)
            {
                Console.WriteLine(new string(' ', (int)(Console.WindowWidth - s.Length * 1.5f) / 2) + s);
                Thread.Sleep(1000);
            }
            startFlag = true;
            Console.ReadLine();
            Console.Clear();
        }

        public static void PrintRememberStory()
        {
            if (rememberFlag) return;

            Console.Clear();
            foreach (string s in rememberStory)
            {
                Console.WriteLine(new string(' ', (int)(Console.WindowWidth - s.Length * 1.5f) / 2) + s);
                Thread.Sleep(1000);
            }
            rememberFlag = true;
            Console.ReadLine();
            Console.Clear();
        }

        public static void PrintMeetStory()
        {
            if (meetFlag) return;

            Console.Clear();
            foreach (string s in meetStory)
            {
                Console.WriteLine(new string(' ', (int)(Console.WindowWidth - s.Length * 1.5f) / 2) + s);
                Thread.Sleep(1000);
            }
            meetFlag = true;
            Console.ReadLine();
            Console.Clear();
        }

        public static void PrintEndStory()
        {
            if (endFlag) return;

            Console.Clear();
            foreach (string s in endStory)
            {
                Console.WriteLine(new string(' ', (int)(Console.WindowWidth - s.Length * 1.5f) / 2) + s);
                Thread.Sleep(1200);
            }
            endFlag = true;
            Console.ReadLine();
            Console.Clear();
        }
    }
}