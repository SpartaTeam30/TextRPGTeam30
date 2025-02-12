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
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");

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
            saveManager.SaveGame(player);

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리 보기");
            Console.WriteLine("3. 전투 시작");
            Console.WriteLine("4. 퀘스트");
            Console.WriteLine("5. 상점");
            Console.WriteLine("0. 종료하기");

            // 🔥 장비 착용 상태 표시
            Console.WriteLine("\n[장비 상태]");
            Console.Write("무기: ");
            if (player.equipWeapon != null)
                GameManager.PrintColoredLine($"{player.equipWeapon.itName}", ConsoleColor.Cyan);
            else
                Console.WriteLine("없음");

            Console.Write("방어구: ");
            if (player.equipArmor != null)
                GameManager.PrintColoredLine($"{player.equipArmor.itName}", ConsoleColor.Cyan);
            else
                Console.WriteLine("없음");

            Console.Write("\n>> ");
            GameManager.CheckWrongInput(out int select, 0, 5);

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
                    saveManager.SaveGame(player);
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
