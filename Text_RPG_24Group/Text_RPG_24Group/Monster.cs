using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_24Group
{
    public class Monster
    {
        public string MonsterName { get; private set; }
        public string Monstertell { get; private set; }
        public int MonsterLev { get; private set; }
        public int MonsterAtk { get; private set; }
        public int MonsterDef { get; private set; }
        public int MonsterHp { get; private set; }
        public int MonsterGold { get; private set; }

        public Monster(string name, string tell, int level, int atk, int def, int hp, int Gold)
        {
            MonsterName = name;
            Monstertell = tell;
            MonsterLev = level;
            MonsterAtk = atk;
            MonsterDef = def;
            MonsterHp = hp;
            MonsterGold = Gold;
        }
        //private List<Monster> MonsterList = new List<Monster>();
    }
}
