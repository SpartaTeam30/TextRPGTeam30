namespace TextRPGTeam30
{
    internal class GameManager
    {
        public Player player;
        public DungeonManager dManager;
        public QuestManager questManager;

        public GameManager()
        {

        }

        public GameManager(Player player, QuestManager questManager)
        {
            this.player = player;
            dManager = new DungeonManager(this.player);
            this.questManager = questManager;
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
            QuestManager questManager = new QuestManager();
            dManager = new DungeonManager(player);

            Console.WriteLine("이제 전투를 시작할 수 있습니다.");

            StartSelect();
        }

        public void StartSelect()
        {
            Console.Clear();
            PrintColoredLine("마을", ConsoleColor.Green);
            Console.WriteLine("이곳에서는 다양한 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine("3. 퀘스트");
            CheckWrongInput(out int select, 1, 3);

            switch (select)
            {
                case 1:
                    player.DisplayStatus();
                    break;
                case 2:
                    dManager.DungeonStart();
                    break;
                case 3:
                    questManager.Questscreen();
                    break;
                default:
                    break;
            }
        }
    }
}
