namespace TextRPGTeam30
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            player.DisplayInventory();
                
            
            GameManager gameManager = new GameManager();

            gameManager.PrintStartScene();

            while(true)
            {
                gameManager.StartSelect();
            }
        }
    }
}
