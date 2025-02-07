namespace TextRPGTeam30
{
    internal class Player : ICharacter
    {
        public int mp;
        public int gold;
        public int exp;

        //public lJob job;
        //public List<Equipable> equipment;
        //public List<Consumable> consumables;
        //public Weapon? equipWeapon;
        //public Armor? equipArmor;
        public int Level { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public int CritRate { get; set; }
        public float Attack { get; set; }
        public int CritDamage { get; set; }
        public int Evasosion { get; set; }

        public Player()
        {

        }

        public void TakeDamage(int damage)
        { 
        
        }

        public void Dead()
        {

        }
    }
}
