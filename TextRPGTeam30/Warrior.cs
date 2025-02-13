namespace TextRPGTeam30
{
    public class Warrior : Job
    {
        public Warrior()
        {
            name = "전사";
            //skills.Add(new Slash());
            hp = 150;
            mp = 50;
            attack = 35;
            defense = 40;
        }

        public Warrior(List<Skill> savedSkills, int savedHp, int savedMp, float savedAttack, int savedDefense)
        {
            name = "전사";
            this.hp = savedHp;
            this.mp = savedMp;
            this.attack = savedAttack;
            this.defense = savedDefense;

            skills = savedSkills ?? new List<Skill> { }; // 저장된 스킬이 없으면 기본 스킬 추가
        }

    }
}
