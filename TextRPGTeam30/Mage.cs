namespace TextRPGTeam30
{
    public class Mage : Job
    {
        // 기본 생성자 (초기값을 강제 설정하지 않음)
        public Mage()
        {
            name = "마법사";
            skills.Add(new Fireball());
            hp = 100;
            attack = 10;
            defense = 10;
        }

        // 저장된 값으로 초기화하는 생성자 추가
        public Mage(List<Skill> savedSkills, int savedHp, float savedAttack, int savedDefense)
        {
            name = "마법사";
            this.hp = savedHp;
            this.attack = savedAttack;
            this.defense = savedDefense;

            skills = savedSkills ?? new List<Skill> { new Fireball() }; // 저장된 스킬이 없으면 기본 스킬 추가
        }

    }
}
