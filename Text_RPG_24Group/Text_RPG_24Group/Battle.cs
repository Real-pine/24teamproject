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
        public string Name { get; } // 스킬 이름
        public int Type { get; } // 스킬 설명
        public int Value1 { get; } // 스킬 벨류
        public int Value2 { get; } // 타겟 수
                                   //public int Value3 { get; }
        public string Desc { get; }

        public static int[] skillValue = { 0, 1, 3, 2 }; // 스킬 데미지 값

        public Battle(JobType job, string name, string tell, int value1, int value2)
        {
            Name = name; // 이름
            Desc = tell; // 설명
            Type = value1;

            switch ((int)job)
            {
                case 1:
                    Value1 = skillValue[Value1]; // 직업별 스킬 벨류
                    Value2 = value2; // 타겟 수
                                     //Value3 = value3; // 마력
                    break;
                case 2:
                    Value1 = Type * skillValue[(int)job]; // 직업별 스킬 공격력
                    Value2 = value2; // 타겟 수
                                     //Value3 = value3; // 마력
                    break;
                case 3:
                    Value1 = Type * skillValue[(int)job]; // 직업별 스킬 공격력
                    Value2 = value2; // 타겟 수
                                     //Value3 = value3; // 마력
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
            return $"{Name}\n\t공격력 * {skillValue[Value1]}로 {DisplayTypeText}{Desc}";
            // 기본 공격
            //  공격력 * 1로 한명의 적을 공격합니다.
        }

        public int MonsterAtk { get; private set; }

        public Battle(string name, string tell, int atk)
        {
            Name = name;
            Desc = tell;
            Value1 = atk;
        }

        public string MonsterSkillInfoText()
        {
            return $"{Name}\n{Desc} - [데미지 : {Value1}]";
            // 점액 뿌리기
            // 산성 점액에 의해 화상을 약간 입었다. - [데미지 : 5]
            // 치명타와 회피는 따로 하기
        }
    }
}
