﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public double Value1 { get; private set; } // 스킬 벨류
        public int Value2 { get; private set; } // 타겟 수
        public int Value3 { get; private set; } // 공격력
        public string Desc { get; private set; } // 스킬 설명

        public bool isSkill = false;


        public Battle(CharacterCustom.JobType job, string name, string desc, double value,int targetCount, int atk, bool skillType )
        {
            Name = name;
            Desc = desc;
            Value1 = value;
            Value2 = targetCount;
            Value3 = atk;
            isSkill = skillType;

            //Name = name; // 이름
            //Desc = desc; // 설명

            //switch (job)
            //{
            //    case (CharacterCustom.JobType)1:
            //        Type = value1 * akkackValue[skillNumber]; // 직업별 공격력
            //        Value1 = Type * skillValue[skillNumber]; // 직업별 스킬 공격력
            //        Value2 = targetCount[value2]; // 타겟 수
            //        Value3 = value3; // 마력
            //        break;
            //    case (CharacterCustom.JobType)2:
            //        Type = value1 * akkackValue[skillNumber]; // 직업별 공격력
            //        Value1 = Type * skillValue[skillNumber]; // 직업별 스킬 공격력
            //        Value2 = targetCount[value2]; // 타겟 수
            //        Value3 = value3; // 마력
            //        break;
            //    case (CharacterCustom.JobType)3:
            //        Type = value1 * akkackValue[skillNumber]; // 직업별 공격력
            //        Value1 = Type * skillValue[skillNumber]; // 직업별 스킬 공격력
            //        Value2 = targetCount[value2]; // 타겟 수
            //        Value3 = value3; // 마력
            //        break;
            //}
        }

        public string DisplayTypeText
        {
            get
            {

                return Value2 == 2 ? $" * {Value2}명의" : "한명의";

            }
        }

        public string PlayerSkillInfoText() //스킬설명메서드
        {
            return $"{Name}\n\t공격력 * {Value1}로 {DisplayTypeText}{Desc}";
            // 기본 공격(모든 데미지)
            //  공격력 * 1로 한명의 적을 공격합니다.
        }

        public string PlayerAttacknfoText() //데미지표시메서드
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

        public string MonsterSkillInfoText() //몬스터가 플레이어공격시 출력메서드
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
                int damage = 1;
                Random random = new Random();

                int err = random.Next(-1, 2);
                int errDamange = (Value3 / 10) * err;

                int critical = random.Next(0, 100);
                if (critical <= 15)
                    damage = (int)Math.Round((Value3 + errDamange) * 1.6f);
                else
                    damage = Value3 + errDamange;

                return critical <= 15 ? $"[데미지] : {damage} - 치명타 공격!!"
                    : $"[데미지 : {Value3}]";
            }
        }
    }
}
