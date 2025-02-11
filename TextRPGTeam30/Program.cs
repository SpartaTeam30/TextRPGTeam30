namespace TextRPGTeam30
{
    public class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();

            while (gameManager.player.Hp > 0) gameManager.StartSelect();
        }
    }
}
