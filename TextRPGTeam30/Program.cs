namespace TextRPGTeam30
{
    public class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();

            while (true) gameManager.StartSelect();
        }
    }
}
