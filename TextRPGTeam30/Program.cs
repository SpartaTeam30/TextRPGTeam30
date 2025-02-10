namespace TextRPGTeam30
{
    internal class Program
    {
        static void Main(string[] args)
        {
            QuestManager questManager = new QuestManager();
            Player player = new Player();
            GameManager gameManager = new GameManager(player, questManager);

            gameManager.PrintStartScene();

            while (true)
            {
                gameManager.StartSelect();
            }
        }
    }
}
