using System.Xml.Linq;
using TextRPGTeam30;

namespace TextRPGTeam30
{
    public class Warrior : Job
    {
        public Warrior()
        {
            name = "전사";
            skills.Add(new Slash());
            hp = 100;
            attack = 10;
            defense = 10;

        }

        public Warrior(Skill skill, int savedHp, float savedAttack, int savedDefense) : base(skill, savedHp, savedAttack, savedDefense)
        {
            name = "야만전사"; // 직업명 유지
            this.hp = savedHp;
            this.attack = savedAttack;
            this.defense = savedDefense;
        }
    }
}
