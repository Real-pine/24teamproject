using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public CharacterCustom player = Program.player;

        public int Atk = 0; // 공격력
        public int Def = 0; // 방어력

        private Random random = new Random();

        private bool isCritical = false;
        private bool isAvoid = false;

        private int damage = 0; // 데미지
        private int playerHp = 0;

        public int checkNumber { get; set; }

        private string[] skillName1 = { "", "깊게 베기", "폭발", "암습" };
        private string[] skillName2 = { "", "휩쓸기", "전기 사슬", "수리검 던지기" };

        public void BasicAttack(Monster monster) // 기본 공격 메서드
        { // 타겟이 된 몬스터 정보 받아오기
            if(monster.curHp <= 0)
            {
                Console.WriteLine("몬스터가 이미 죽어있습니다.");
                return;
            }

            Console.WriteLine($"{player.Name} 의 공격!");

            // 공격 회피 10% ← 스킬은 회피 X
            int avoid = random.Next(0, 100);
            isAvoid = avoid <= 10 ? true : false;

            Console.WriteLine(avoid);

            if (isAvoid)
            { // 회피시 회피 출력 후 종료
                Console.WriteLine($"Lv.{monster.MonsterLev} {monster.MonsterName} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
            }
            else
            {
                Atk = player.Atk;
                Def = monster.MonsterDef;

                int critical = random.Next(0, 100);
                if (critical <= 15)
                    isCritical = true;
                else
                    isCritical = false;

                damage = damageCalculation;

                if (isCritical)
                    Console.WriteLine($"Lv.{monster.MonsterLev} {monster.MonsterName} 을(를) 맞췄습니다. [데미지 :{damage}] - 치명적 공격!!");
                else
                    Console.WriteLine($"Lv.{monster.MonsterLev} {monster.MonsterName} 을(를) 맞췄습니다. [데미지 :{damage}]");

                // 몬스터 클래스에서  curHp 변수를 만들고
                // curHp = MonsterHP;

                Console.WriteLine("\n");
                Console.WriteLine($"Lv.{monster.MonsterLev} {monster.MonsterName}");
                Console.Write($"HP {monster.curHp} -> ");
                monster.curHp -= damage;
                Console.WriteLine(monster.curHp > 0 ? monster.curHp : "Dead");
                Console.WriteLine("\n");
            }
        }

        public void Skill(JobType Jop) // 스킬 설명 출력 메서드
        {
            checkNumber = 0;

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
            Console.WriteLine($"MP {player.Mp}/{player.MaxMp}");
            Console.WriteLine("\n");
            if (player.Mp >= 10)
            {
                checkNumber++;
                Console.WriteLine($"1. {skillName1[(int)player.Job]} - MP 10");
                Console.WriteLine("공격력 * 2 로 하나의 적을 공격합니다.");
            }
            if (player.Mp >= 15)
            {
                checkNumber++;
                Console.WriteLine($"2. {skillName2[(int)player.Job]} - MP 15");
                Console.WriteLine("공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.");
            }
            Console.WriteLine("0. 취소");
            Console.WriteLine("\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
        }

        public void SkillAttack(Monster monster, int skiilNumber) // 스킬 공격 메서드
        { // 행동 선택시 스킬 발동 마찬가지로 타겟이 된 몬스터 정보 받아오기
            if (monster.curHp <= 0)
            {
                Console.WriteLine("몬스터가 이미 죽어있습니다.");
                return;
            }

            Atk = player.Atk;

            switch (skiilNumber)
            {
                case 1:
                    player.Mp -= 10;
                    Atk = player.Atk * 2;
                    break;
                case 2:
                    player.Mp -= 15;
                    Atk = (int)Math.Round(player.Atk * 1.5f);
                    break;
            }
            Def = monster.MonsterDef; ;

            Console.WriteLine($"{player.Name} 의 공격! - {skillName1[(int)player.Job]}");

            int critical = random.Next(0, 100);
            if (critical <= 15)
                isCritical = true;
            else
                isCritical = false;

            damage = damageCalculation;

            if (isCritical)
                Console.WriteLine($"Lv.{monster.MonsterLev} {monster.MonsterName} 을(를) 맞췄습니다. [데미지 :{damage}] - 치명적 공격!!");
            else
                Console.WriteLine($"Lv.{monster.MonsterLev} {monster.MonsterName} 을(를) 맞췄습니다. [데미지 :{damage}]");
            Console.WriteLine("\n");

            Console.WriteLine($"Lv.{monster.MonsterLev} {monster.MonsterName}");
            Console.Write($"HP {monster.curHp} -> ");
            monster.curHp -= damage;
            Console.WriteLine(monster.curHp > 0 ? monster.curHp : "Dead");
            Console.WriteLine("\n");
        }

        public void MonsterAttack(Monster monster) // 몬스터 공격 메서드
        { // 타겟이 되었던 몬스터의 정보 받아오기
            Console.WriteLine($"{monster.MonsterName} 의 공격!");

            // 공격 회피 10%
            int avoid = random.Next(0, 100);
            isAvoid = avoid <= 10 ? true : false;

            if (isAvoid)
            { // 회피시 회피 출력 후 종료
                Console.WriteLine($"Lv.{player.Level} {player.Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
            }
            else
            {
                Atk = monster.MonsterAtk;
                Def = player.Def;

                int critical = random.Next(0, 100);
                if (critical <= 15)
                    isCritical = true;
                else
                    isCritical = false;

                damage = damageCalculation;

                if (isCritical)
                    Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 :{damage}] - 치명적 공격!!");
                else
                    Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 :{damage}]");

                Console.WriteLine("\n");
                Console.Write($"HP {player.Hp} -> ");
                playerHp = player.Hp;
                // 전투 종료시 출력 될 이전 HP
                player.Hp -= damage;
                if (player.Hp < 0)
                    player.Hp = 0;
                Console.WriteLine(player.Hp);
                Console.WriteLine("\n");
            }
        }

        public void ResultWin(int monsterCount) // 결과 - 승리 출력 메서드
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!! - Result");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n");
            Console.WriteLine("Victory");
            Console.WriteLine("\n");
            Console.WriteLine($"던전에서 몬스터 {monsterCount}마리를 잡았습니다.");
            Console.WriteLine("\n");
            Console.WriteLine($"Lv. {player.Level} {player.Name}");
            Console.WriteLine($"HP {playerHp} -> {player.Hp}");
            Console.WriteLine("\n");
        }

        public void ResultLose() // 결과 - 패배 출력 메서드
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!! - Result");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n");
            Console.WriteLine("You Lose");
            Console.WriteLine("\n");
            Console.WriteLine($"Lv. {player.Level} {player.Name}");
            Console.WriteLine($"HP {playerHp} -> {player.Hp}");
            Console.WriteLine("\n");
        }

        public int damageCalculation // 데미지 계산 변수
        {
            get
            {
                int outDamage = 0;

                // 데미지 오차 10%
                int err = random.Next(-1, 2);
                int errDamage = (Atk / 10) * err;

                if (isCritical)
                    outDamage = (int)Math.Round((Atk + errDamage) * 1.6f) - Def;
                else
                    outDamage = Atk + errDamage - Def;

                return outDamage;
            }
        }
    }
}