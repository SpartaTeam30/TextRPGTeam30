namespace TextRPGTeam30
{
    internal interface ICharacter
    {
        int Level { get; set; }
        string Name { get; set; }
        float Attack { get; set; }
        int Hp { get; set; }
<<<<<<< Updated upstream
=======
        int Defense { get; set; }
>>>>>>> Stashed changes
        int CritRate { get; set; }       
        int Evasion { get; set; }
        int Health { get; set; }
        public int MaxHp { get; set; }
        public int Potions { get; set; }

        void TakeDamage(int damage);

<<<<<<< Updated upstream
        void Dead();    
=======
        }

        void ApplydStat(UtilitySkill s);

        void Dead();
        void RestoreMana(int restoreAmount);
        void RestoreHealth(int restoreAmount);
>>>>>>> Stashed changes
    }
}

