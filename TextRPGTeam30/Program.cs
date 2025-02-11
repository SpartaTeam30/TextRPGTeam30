namespace TextRPGTeam30
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            string characterName =player.Name ;
            QuestManager questManager = new QuestManager(characterName);  
            GameManager gameManager = new GameManager(player, questManager);

            gameManager.PrintStartScene();

            while (true)
            {
                gameManager.StartSelect();
            }
        }
    }
}
