using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        public CharacterCustom player;

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

        string[] skillName1 = { "깊게 베기", "폭발", "암습" };
        string[] skillName2 = { "휩쓸기", "전기 사슬", "수리검 던지기" };

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
                Power = player.Atk;

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

        public void Skill(JobType Jop)
        {
            Console.WriteLine("[내정보]");
            switch ((int)Jop)
            {
                case 1:
                    Console.WriteLine($"Lv. {player.Level} {player.Name} (전사)");
                    break;
                case 2:
                    Console.WriteLine($"Lv. {player.Level} {player.Name} (마법사)");
                    break;
                case 3:
                    Console.WriteLine($"Lv. {player.Level} {player.Name} (도적)");
                    break;
            }
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}");
            Console.WriteLine("MP 50/50");
            // 마나 아직 없음
            Console.WriteLine("\n");
            Console.WriteLine($"1. {skillName1[(int)player.Job]} - MP 10");
            Console.WriteLine("공격력 * 2 로 하나의 적을 공격합니다.");
            Console.WriteLine($"2. {skillName2[(int)player.Job]} - MP 15");
            Console.WriteLine("공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.");
        }
        // ↑ 지우게 될 수 도 있는거

        public void SkillAttack(Monster monster, int skiilNumber) // 스킬 공격 메서드
        { // 행동 선택시 스킬 발동 마찬가지로 타겟이 된 몬스터 정보 받아오기
            switch (skiilNumber)
            {
                case 1:
                    Spell = 10;
                    Power = player.Atk * 2;
                    break;
                case 2:
                    Spell = 15;
                    Power = (int)Math.Round(player.Atk * 1.5f);
                    // 2번 공격 : 2명의 적을 랜덤으로 공격합니다. -> 한명의 적을 두번 공격합니다.
                    // 이게 좋으려나?
                    break;
            }

            if (isCritical)
                Console.WriteLine($"Lv.{monster.MonsterLev} {monster.MonsterName} 을(를) 맞췄습니다. [데미지 :{damage}] - 치명적 공격!!");
            else
                Console.WriteLine($"Lv.{monster.MonsterLev} {monster.MonsterName} 을(를) 맞췄습니다. [데미지 :{damage}]");

            Console.WriteLine("\n");
            Console.WriteLine($"Lv.{monster.MonsterLev} {monster.MonsterName}");
            Console.Write($"HP {monster.curHp} -> ");
            monster.curHp =- damage;
            Console.WriteLine(monster.curHp > 0 ? monster.curHp : "Dead");
        }

        public void MonsterAttack(Monster monster) // 몬스터 공격 메서드
        { // 타겟이 되었던 몬스터의 정보 받아오기
            Console.WriteLine($"{monster.MonsterName} 의 공격!");

            // 공격 회피 10%
            int avoid = random.Next(0, 100);
            isAvoid = avoid <= 10 ? false : true;

            if (isAvoid)
            { // 회피시 회피 출력 후 종료
                Console.WriteLine($"Lv.{player.Level} {player.Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
            }
            else
            {
                Power = monster.MonsterAtk;

                if (isCritical)
                    Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 :{damage}] - 치명적 공격!!");
                else
                    Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 :{damage}]");

                Console.WriteLine("\n");
                Console.Write($"HP {player.Hp} -> ");
                player.Hp = - damage;
                if (player.Hp < 0)
                    player.Hp = 0;
                Console.WriteLine(player.Hp);
            }
        }

<<<<<<< HEAD
        public void ResultWin(int monsterCount)
=======
        public string PlayerSkillInfoText() //스킬상세설명메서드
>>>>>>> (New)Dungeon30
        {
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine("\n");
            Console.WriteLine("Victory");
            Console.WriteLine("\n");
            Console.WriteLine($"던전에서 몬스터 {monsterCount}마리를 잡았습니다.");
            Console.WriteLine("\n");
            Console.WriteLine($"Lv. {player.Level} {player.Name}");
            Console.WriteLine($"HP {player.Hp} -> {player.Hp}");
            // 던전 입장 전 HP? 마지막 전투 HP?
        }

<<<<<<< HEAD
        public void ResultLose()
=======
        public string PlayerAttackInfoText() //스킬일떄만?기본공격둘다? 데미지출력메서드
>>>>>>> (New)Dungeon30
        {
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine("\n");
            Console.WriteLine("You Lose");
            Console.WriteLine("\n");
            Console.WriteLine($"Lv. {player.Level} {player.Name}");
            Console.WriteLine($"HP {player.Hp} -> {player.Hp}");
            // 던전 입장 전 HP? 마지막 전투 HP?
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