namespace TextRPGTeam30
{
    public class Program
    {
        static void Main(string[] args)
        {
            QuestManager questManager = new QuestManager();
            Player player = new Player();
            GameManager gameManager = new GameManager(player, questManager);

            while (true) gameManager.StartSelect();
        }
    }
}
