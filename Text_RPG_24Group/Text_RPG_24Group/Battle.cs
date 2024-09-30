using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static Text_RPG_24Group.CharacterCustom;

namespace Text_RPG_24Group
{
    public class Battle
    {
        public static CharacterCustom player;

        public string Name { get; private set; } // 스킬 이름
        public string Desc { get; private set; } // 스킬 설명
        public int Power { get; set; } // 공격력
        public int Spell { get; set; } // 마력

        Random random = new Random();

        bool isCritical = false;
        bool isAvoid = false;

        //public Battle(string name, string desc)
        //{
        //    Power = player.Atk;
        //    Name = name;
        //    Desc = desc;
        //}

        //public Battle(string name, string tell, int atk)
        //{
        //    Name = name;
        //    Desc = tell;
        //    Power = atk;
        //}

        // 직업 별 기본 스탯은 CharacterCustomdp 있고
        // 스킬만 다르게 하면 될 거 같고
        // 몬스터 별 기본 스탯은 이미 있으니까 받아오면 되지 않아도 되려나?

        public void BasicAttack(Monster monster) // 기본 공격 메서드
        { // 타겟이 된 몬스터 정보 받아오기
            Console.WriteLine($"{player.Name} 의 공격!");

            // 공격 회피 10% ← 스킬은 회피 X
            int avoid = random.Next(0, 100);
            isAvoid = avoid <= 10 ? false : true;

            if (isAvoid)
            { // 회피시 회피 출력 후 종료
                Console.WriteLine($"Lv.{monster.MonsterLev} {monster.MonsterName} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
            }
            else
            {
                if (isCritical)
                    Console.WriteLine($"Lv.{monster.MonsterLev} {monster.MonsterName} 을(를) 맞췄습니다. [데미지 :{damage}] - 치명적 공격!!");
                else
                    Console.WriteLine($"Lv.{monster.MonsterLev} {monster.MonsterName} 을(를) 맞췄습니다. [데미지 :{damage}]");

                // 몬스터 클래스에서  curHp 변수를 만들고
                // curHp = MonsterHP;

                Console.WriteLine("\n");
                Console.WriteLine($"Lv.{monster.MonsterLev} {monster.MonsterName}");
                Console.Write($"HP {monster.curHp} -> ");
                monster.curHp =- damage;
                Console.WriteLine(monster.curHp > 0 ? monster.curHp : "Dead");
            }
        }

        public void SkillAttack(int skiilNumber)
        { // 행동 선택시 스킬 발동

            switch (skiilNumber)
            {
                case 1:
                    break;
                case 2:
                    break;
            }
        }

        public void MonsterAttack(Monster monster) // 몬스터 공격 메서드
        { // 타겟이 되었던 몬스터의 정보 받아오기
            Console.WriteLine($"{monster.MonsterName} 의 공격!");

        }
        public int damage
        {
            get
            {
                int outDamage = 0;

                // 데미지 오차 10%
                int err = random.Next(-1, 2);
                int errDamage = (Power / 10) * err;

                // 크리티컬 15%
                int critical = random.Next(0, 100);
                if (critical <= 15)
                {
                    outDamage = (int)Math.Round((Power + errDamage) * 1.6f);
                    isCritical = true;
                }
                else
                {
                    outDamage = Power + errDamage;
                    isCritical = false;
                }

                return outDamage;
            }
        }
    }
}