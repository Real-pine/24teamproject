using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Text_RPG_24Group.CharacterCustom;

namespace Text_RPG_24Group
{
    internal class Battle
    {
        public int Type { get; private set; } // 직업
        // int input = CheckInput(1, 3);
        // playerBattleDb[player.Job * 3 - input].PlayerSkillInfoText();
        // 이런식으로 스킬을 쓰면 되려나
        public string Name { get; private set; } // 스킬 이름
        public double Value { get; private set; } // 스킬 벨류
        public int TargetCount { get; private set; } // 타겟 수
        public int Power { get; private set; } // 공격력
        public string Desc { get; private set; } // 스킬 설명

        public bool isSkill = false;

        public static int damage;

        public static string[] name1 = { "가만히 있기", "기본공격", "깊게 베기", "연속공격" };
        public static string[] name2 = { "가만히 있기", "기본공격", "폭발", "전기 사슬" };
        public static string[] name3 = { "가만히 있기", "기본공격", "암습", "수리검 던지기" };

        public Battle(JobType job, int InName, string InDesc, double InValue, int InTargetCount, int InAtk, bool InSkill)
        {
            Desc = InDesc;
            Value = InValue;
            TargetCount = InTargetCount;
            Power = InAtk;
            isSkill = InSkill;

            switch ((int)job)
            {
                case 1:
                    Name = name1[InName];
                    break;
                case 2:
                    Name = name2[InName];
                    break;
                case 3:
                    Name = name3[InName];
                    break;
            }
        }

        public string DisplayTypeText
        {
            get
            {
                return TargetCount == 2 ? $" * {TargetCount}명의" : "한명의";
            }
        }

        public string PlayerSkillInfoText()
        {
            return $" {Name}\n - 공격력 * {Value}로 {DisplayTypeText}{Desc}";
            // 기본 공격
            //  공격력 * 1로 한명의 적을 공격합니다.
        }

        public string PlayerAttacknfoText()
        {
            Random random = new Random();

            int avoid = random.Next(0, 100);

            string attacktext;

            if (isSkill)
                attacktext = $" 을(를) 맞췄습니다.{Damage}";
            // 을(를) 맞췄습니다. [데미지 : 16] - 치명타 공격!!
            else
                attacktext = avoid <= 10 ? $" 을(를) 맞췄습니다. {Damage}"
                : " 을(를) 공격했지만 아무일도 일어나지 않았습니다.";
            // 회피시 : 하지만 아무일도 일어나지 않았습니다.

            return attacktext;
        }

        public int MonsterAtk { get; private set; }

        public Battle(string name, string tell, int atk)
        {
            Name = name;
            Desc = tell;
            Power = atk;
        }

        public string MonsterSkillInfoText()
        {
            Random random = new Random();

            int avoid = random.Next(0, 100);

            return avoid <= 10 ? $"{Name}\n{Desc} - {Damage}" 
                : $" {Name}\n을(를) 공격했지만 아무일도 일어나지 않았습니다.";
        }

        private string Damage
        {
            get
            {
                Random random = new Random();

                int err = random.Next(-1, 2);
                int errDamange = (Power / 10) * err;

                int critical = random.Next(0, 100);
                if (critical <= 15)
                    damage = (int)Math.Round((Power + errDamange) * 1.6f);
                else
                    damage = Power + errDamange;

                return critical <= 15 ? $" [데미지] : {damage} - 치명타 공격!!"
                    : $" [데미지 : {Power}]";
            }
        }
    }
}
// 캐릭터별로 힘 민첩 지능
// 힘 = 공격력
// 민첩 = 치명타 확률
// 지능 = 치명타
// 레벨 업마다 찍을 수 있고
// CharacterCustom을 건드려야 되나?
// 곱 처리로 데미지 증가
// 임시로 만들어서 해봐야지