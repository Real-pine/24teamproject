using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_24Group
{
    internal class Battle
    {
        public string Name { get; }
        public int Type { get; }
        public int Value1 { get; }
        public int Value2 { get; }
        public int Value3 { get; }
        public string Desc { get; }

        public static int[] skillJobType = { 0, 1, 2, 3 }; // 1: 전사, 2: 마법사, 3. 도적
        public static int[] akkackValue = { 0, 3, 1, 2 }; // 기본 데미지 값
        public static int[] skillValue = { 0, 1, 3, 2 }; // 스킬 데미지 값
        public static int[] targetCount = { 0, 1, 2 }; // 타겟 수

        public Battle(CharacterCustom.JobType job, string name, string tell, int skillNumber, int value1, int value2, int value3)
        {
            Name = name; // 이름
            Desc = tell; // 설명

            switch (job)
            {
                case (CharacterCustom.JobType)1:
                    Type = value1 * akkackValue[skillNumber]; // 직업별 공격력
                    Value1 = Type * skillValue[skillNumber]; // 직업별 스킬 공격력
                    Value2 = targetCount[value2]; // 타겟 수
                    Value3 = value3; // 마력
                    break;
                case (CharacterCustom.JobType)2:
                    Type = value1 * akkackValue[skillNumber]; // 직업별 공격력
                    Value1 = Type * skillValue[skillNumber]; // 직업별 스킬 공격력
                    Value2 = targetCount[value2]; // 타겟 수
                    Value3 = value3; // 마력
                    break;
                case (CharacterCustom.JobType)3:
                    Type = value1 * akkackValue[skillNumber]; // 직업별 공격력
                    Value1 = Type * skillValue[skillNumber]; // 직업별 스킬 공격력
                    Value2 = targetCount[value2]; // 타겟 수
                    Value3 = value3; // 마력
                    break;
            }
        }

        private List<Battle> playerSkillList = new List<Battle>();

        public int MonsterAtk { get; private set; }

        public Battle(string name, string tell, int atk)
        {
            Name = name;
            Desc = tell;
            Value1 = atk;
        }

        private List<Battle> MonsterSkillList = new List<Battle>();
    }
}
