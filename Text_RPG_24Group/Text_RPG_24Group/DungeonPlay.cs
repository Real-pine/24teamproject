using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace Text_RPG_24Group
{
    class DungeonPlay
    {
        public static CharacterCustom player;
        public static Monster monster;
        public static Battle battle;
        public void InDungeonUI()
        {

            Console.Clear();
            Console.WriteLine("던전에 입장했습니다.");
            Console.WriteLine($"<현재 {player.Name}의 상태>");
            player.DisplayCharacterInfo();
            Console.WriteLine("\n\n원하시는 행동을 입력해 주세요.");
            Console.WriteLine("1. 던전진행 (");
            Console.WriteLine("0. 나가기");

            int result = Program.CheckInput(0, 1);

            switch (result)
            {
                case 0:
                    Program.DisplayMainUI();
                    break;
                case 1:
                    EncounterUI();
                    break;
            }
        }

        public void EncounterUI()
        {
            Console.Clear();
            Console.WriteLine("적을 마주했습니다!");
            Console.WriteLine("\n\nBattle !!");
            //몬스터 출현 메서드
            //몬스터 목록
            Console.WriteLine("\n\n{내정보}");
            Console.WriteLine($"LV.{player.Level} {player.Name}");
            Console.WriteLine($"HP : {player.Hp}/{player.MaxHp}");
            //Console.WriteLine($"MP : {player.MP}/{player.MaxMP}"); <<마나

            Console.WriteLine("\n\n원하시는 행동을 입력해주세요");
            Console.WriteLine("1. 전투시작");
            Console.WriteLine("0. 도망간다(Hp 50감소)");
            int result = Program.CheckInput(0, 1);

            switch (result)
            {
                case 0:
                    Program.DisplayMainUI();
                    player.Hp -= 50;
                    break;
                case 1:
                    MyBattlePhase();
                    break;
            }
        }
        public void MyBattlePhase()
        {
            Console.WriteLine("\n\nBattle !!");
            Console.WriteLine($"{player.Name}의 페이즈");
            
            //몬스터 출현 메서드
            //몬스터 목록

            Console.WriteLine("\n\n{내정보}");
            Console.WriteLine($"LV.{player.Level} {player.Name}");
            Console.WriteLine($"HP : {player.Hp}/{player.MaxHp}");
            //Console.WriteLine($"MP : {player.MP}/{player.MaxMP}"); <<마나

            Console.WriteLine("\n\n원하시는 행동을 입력해주세요");
            Console.WriteLine("1. 공격하기");
            Console.WriteLine("2. 스킬사용");
            Console.WriteLine("0. 도망가기(HP 50감소)");

            int result = Program.CheckInput(0, 2);

            switch (result)
            {
                case 0:
                    Program.DisplayMainUI();
                    player.Hp -= 50;
                    break;
                case 1:
                    //기본공격메서드, 공격시 데미지출력 메서드
                    break;
                case 2:
                    //스킬공격메서드, 공격시 데미지출력 메서드
                    break;
            }
        }
        //기본공격시 페이즈
        public void Player1Attack()
        {
            Console.WriteLine("\n\nBattle !!");
            Console.WriteLine($"{player.Name}의 페이즈");

            //출현 몬스터중에 랜덤선택메서드 or 선택 메서드
            Console.WriteLine($"\n\n{player.Name}의 기본공격!");
            battle.PlayerAttacknfoText();

            //Console.WriteLine("해당몬스터");
            //Console.WriteLine($"Hp {xx} -> {xx - battle.Damage}");

            Console.WriteLine("\n\n0. 다음");
            int result = Program.CheckInput(0, 2);

            switch (result)
            {
                case 0:
                    Program.DisplayMainUI();
                    break;
            }
        }
        //스킬공격시페이즈
        public void Player2SkillAttack()
        {
            Console.WriteLine("\n\nBattle !!");
            Console.WriteLine($"{player.Name}의 페이즈");

            //몬스터 출현 메서드
            //몬스터 목록

            Console.WriteLine("\n\n{내정보}");
            Console.WriteLine($"LV.{player.Level} {player.Name}");
            Console.WriteLine($"HP : {player.Hp}/{player.MaxHp}");
            //Console.WriteLine($"MP : {player.MP}/{player.MaxMP}"); <<마나

            Console.WriteLine("\n\n원하시는 행동을 입력해주세요");
            
        }
    }
}
