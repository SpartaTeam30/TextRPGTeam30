namespace TextRPGTeam30
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            GameManager.PrintColored("Hello, World!");
            GameManager.PrintColoredLine("Hello, World!", ConsoleColor.Yellow);
            GameManager.PrintColored("Hello, World!", background: ConsoleColor.Magenta);
        }
    }
}
