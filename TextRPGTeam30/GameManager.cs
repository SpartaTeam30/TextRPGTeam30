namespace TextRPGTeam30
{
    internal class GameManager
    {
        public Player player;
        public DungeonManager dManager;
        public QuestManager qManager;

        public GameManager()
        {

        }

        public GameManager(Player player, DungeonManager dungeonManager, QuestManager questManager)
        {
            this.player = player;
            dManager = dungeonManager;
            qManager = questManager;
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
    }
}
