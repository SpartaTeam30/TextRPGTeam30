namespace TextRPGTeam30
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string characterName = "Player1";
            QuestManager questManager = new QuestManager(characterName);
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
