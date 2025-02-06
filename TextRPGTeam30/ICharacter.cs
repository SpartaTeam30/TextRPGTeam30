using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGTeam30
{
    internal interface ICharacter
    {
        public int Level { get; set; }
        public string Name { get; set; }

        public int Hp { get; set; }

        public int CritRate { get; set; }

        public float Attack { get; set; }

        public int CritDamage { get; set; }
        public int Evasosion { get; set; }

        public void TakeDamage(int damage)

        {

        }

        public void Dead()
        {



        }

    }
        

       
















}

