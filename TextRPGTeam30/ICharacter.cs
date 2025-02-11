namespace TextRPGTeam30
{
    internal interface ICharacter
    {
        int Level { get; set; }
        string Name { get; set; }
        float Attack { get; set; }
        int Hp { get; set; }
        int Defense { get; set; }
        int CritRate { get; set; }       
        int Evasion { get; set; }
        void TakeDamage(int damage);

        void Dead();    
        
        void ApplydStat(UtilitySkill s);     
    }
}

