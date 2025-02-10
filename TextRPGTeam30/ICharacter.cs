namespace TextRPGTeam30
{
    internal interface ICharacter
    {
        int Level { get; set; }
        string Name { get; set; }
        float Attack { get; set; }
        int Hp { get; set; }
        int CritRate { get; set; }       
        int Evasion { get; set; }

        void TakeDamage(float attack, int crit, bool isSkill);

        void Dead();    
    }
}

