using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Text_RPG_24Group.CharacterCustom;

namespace Text_RPG_24Group
{
    internal class Battle
    {
        public string Name { get; private set; } // 스킬 이름
        public int Value1 { get; private set; } // 스킬 벨류
        public int Value2 { get; private set; } // 타겟 수
        public int Value3 { get; private set; } // 공격력
        public string Desc { get; private set; } // 스킬 설명

        private static bool isSkill = false;

        private static int damage;

        public Battle(JobType job, string name, string tell, int targetCount, int atk)
        {
            Name = name;
            Desc = tell;

            switch ((int)job)
            {
                case 1:
                    Value1 = 1;
                    Value2 = targetCount;
                    Value3 = atk * Value1;
                    isSkill = false;
                    break;
                case 2:
                    Value1 = 3;
                    Value2 = targetCount;
                    Value3 = atk * Value1;
                    isSkill = true;
                    break;
                case 3:
                    Value1 = 2;
                    Value2 = targetCount;
                    Value3 = atk * Value1;
                    isSkill = true;
                    break;
            }
        }

        public string DisplayTypeText
        {
            get
            {
                return Value2 == 2 ? $" * {Value2}명의" : "한명의";
            }
        }

        public string PlayerSkillInfoText()
        {
            return $"{Name}\n\t공격력 * {Value1}로 {DisplayTypeText}{Desc}";
            // 기본 공격
            //  공격력 * 1로 한명의 적을 공격합니다.
        }

        public string PlayerAttacknfoText()
        {
            Random random = new Random();

            int avoid = random.Next(0, 100);
            // ↓ 스킬은 회피할 수 없습니다.
            string attacktext;

            if (isSkill)
                attacktext = $"을(를) 맞췄습니다. [데미지 : {Damage}]";
            // 을(를) 맞췄습니다. [데미지 : 16] - 치명타 공격!!
            else
                attacktext = avoid <= 10 ? $"을(를) 맞췄습니다. [데미지 : {Damage}]"
                : "을(를) 공격했지만 아무일도 일어나지 않았습니다.";
            // 회피시 : 하지만 아무일도 일어나지 않았습니다.

            return attacktext;
        }

        public int MonsterAtk { get; private set; }

        public Battle(string name, string tell, int atk)
        {
            Name = name;
            Desc = tell;
            Value3 = atk;
        }

        public string MonsterSkillInfoText()
        {
            Random random = new Random();

            int avoid = random.Next(0, 100);

            return avoid <= 10 ? $"{Name}\n{Desc} - {Damage}" 
                : $"{Name}\n을(를) 공격했지만 아무일도 일어나지 않았습니다.";
        }

        private string Damage
        {
            get
            {
                Random random = new Random();

                int critical = random.Next(0, 100);

                if (critical <= 15)
                    damage = (int)Math.Round(Value3 * 1.6f);
                else
                    damage = Value3;

                return critical <= 15 ? $"[데미지] : {damage} - 치명타 공격!!"
                    : $"[데미지 : {Value3}]";
            }
        }
    }
}
