namespace TextRPGTeam30
{
    public class Weapon : Equipable
    {
        public Weapon(string _ItName, int _ItAbility, string _ItType, string _ItInfo, float _attack) : base(_ItName, _ItAbility, _ItType, _ItInfo)
        {
            attack = _attack;
        }

        public float attack;

        // public Weapon()
        
        public void Toggle()
        {
            isEquip = !isEquip;
        }
    }
}
