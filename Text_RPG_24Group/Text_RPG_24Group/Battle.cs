using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Text_RPG_24Group.CharacterCustom;

namespace Text_RPG_24Group
{
    public class Battle
    {
        public string PlayerName { get; private set; }

        public int Type { get; private set; } // 직업
        public string Name { get; private set; } // 스킬 이름
        public double Value { get; private set; } // 스킬 벨류
        public int TargetCount { get; private set; } // 타겟 수
        public int Power { get; set; } // 공격력
        public int Spell { get; set; } // 마력
        public string Desc { get; private set; } // 스킬 설명

        public int damage { get; set; }
        public int MonsterAtk { get; private set; }
        public bool isSkill = false;

        static int abilityDamage = 0;
        static int abilityCriticalChance = 0;
        static double abilityCriticalDamage = 0.0f;

        public Battle(JobType job, int InName, string InDesc, double InValue, int InTargetCount, int InAttack, int InSpell, bool InSkill)
        {
            string[] name1 = { "", "기본공격", "깊게 베기", "연속공격" };
            string[] name2 = { "", "기본공격", "폭발", "전기 사슬" };
            string[] name3 = { "", "기본공격", "암습", "수리검 던지기" };

            Type = (int)job;
            Desc = InDesc;
            Value = InValue;
            TargetCount = InTargetCount;
            Spell = InSpell;
            isSkill = InSkill;
            Power = (int)Math.Round(InAttack * InValue);

            switch (InName)
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

        public string PlayerSkillInfoText() //스킬설명메서드
        {
            string TargetCountText = TargetCount == 2 ? $" * {TargetCount}명의" : "한명의";

            return $"{Name}!\n- 공격력 * {Value} 로 {TargetCountText} {Desc}\n";
            // 기본 공격
            //  공격력 * 1로 한명의 적을 공격합니다.
            // or 공격력 * 1.5로 2명의 적을 랜덤으로 공격합니다.
            // or 공격력 * 2로 한명의 적을 공격합니다.
        }

        public string PlayerAttackInfoText(int InAtk) //데미지표시메서드
        {
            Power = (int)Math.Round(InAtk * Value);
            // 플레이어의 능력치 업데이트

            Random random = new Random();

            int avoid = random.Next(0, 100);

            string attacktext;

            if (isSkill)
            {
                attacktext = $"을(를) 맞췄습니다.{damageText}";
                // 을(를) 맞췄습니다. [데미지 : 10]
            }
            else
            {
                attacktext = avoid <= 10 ? $" 을(를) 맞췄습니다. {damageText}"
                : "을(를) 공격했지만 아무일도 일어나지 않았습니다.";
                damage = avoid <= 10 ? damage : 0;
                // 을(를) 맞췄습니다. [데미지 : 10]
                // or 을(를) 맞췄습니다. [데미지 : 16] - 치명타 공격!!
                // or 회피시 -> 을(를) 공격했지만 아무일도 일어나지 않았습니다. (damage = 0;)
            }

            return attacktext;
        }

        public Battle(string name, string tell, int atk)
        {
            Name = name;
            Desc = tell;
            Power = atk;
        }

        public string MonsterSkillInfoText() //몬스터가 플레이어공격시 출력메서드
        {
            Random random = new Random();

            int avoid = random.Next(0, 100);

            return avoid <= 10 ? $"{Name}\n{Desc} - {damageText}" 
                : $"{Name}\n을(를) 공격했지만 아무일도 일어나지 않았습니다.";
        }

        public void AbilityStat()
        { // 능력치 포인트로 올린 스탯
            abilityDamage = Type == 1 ? Program.player.stat[0] * 2 : Program.player.stat[0] * 1;
            abilityCriticalChance = Type == 2 ? Program.player.stat[1] * 2 : Program.player.stat[1] * 1;
            abilityCriticalDamage = Type == 3 ? Program.player.stat[2] * 0.2f : Program.player.stat[2] * 0.1f;
            // 현재 모두 0이라 능력치엔 변화 없음
        }

        private string damageText
        {
            get
            {
                Random random = new Random();

                int err = random.Next(-1, 2);
                int errDamange = (Power / 10) * err;
                // 데미지 10 퍼센트 오차

                int critical = random.Next(0, 100);
                if (critical <= 15 + abilityCriticalChance)
                    damage = (int)Math.Round((Power + abilityDamage + errDamange) * 1.6f + abilityCriticalDamage);
                else
                    damage = Power + errDamange + abilityDamage;
                // 크리티컬 15 퍼센트

                return critical <= 15 ? $" [데미지] : {damage} - 치명타 공격!!"
                    : $" [데미지 : {damage}]";
            }
        }
    }
}
// 캐릭터별로 힘 민첩 지능
// 힘 = 전사 == true ?  공격력 + 2 : 공격력 + 1
// 민첩 = 도적 == true ?  치명타 확률 + 2 : 치명타 확률 + 1 
// 지능 = 마법사 == true ?  치명타 공격력 + 0.2f : 치명타 공격력 + 0.1f
// 레벨 업마다 찍을 수 있게