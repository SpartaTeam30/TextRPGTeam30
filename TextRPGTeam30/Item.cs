namespace TextRPGTeam30
{
    internal interface IItem
    {
        int ID { get; set; }
        string Name { get; set; }
        // public int Price { get; set; }
        string IDescription { get; set; } // 변수 설명용          
    }
}