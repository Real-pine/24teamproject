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
        public static Monster[] monster;
        public static Stage[] stages;
        public static Battle battle;
        private static Difficulty currentDifficulty; //현재난이도저장
        public static Dictionary<Difficulty, Monster[]> difficultyMonsters;

        //난이도 넘버링 세팅
        public enum Difficulty
        {
            Easy = 1, 
            Normal = 2, 
            Hard = 3 
        }
        static DungeonPlay()
        {
            SetMonsterGroups();
        }
        //그룹화
        private static void SetMonsterGroups()
        {
            difficultyMonsters = new Dictionary<Difficulty, Monster[]>
            {
                {Difficulty.Easy, new[] { Program.monsterDb[0], Program.monsterDb[1] } },
                {Difficulty.Normal, new[] { Program.monsterDb[0], Program.monsterDb[1], Program.monsterDb[2] } },
                {Difficulty.Hard, new[] { Program.monsterDb[2], Program.monsterDb[3], Program.monsterDb[4] } },
            };
        }

        public static Monster[] GetMonsterByDifficulty(Difficulty difficulty)
        {
            if (difficultyMonsters.TryGetValue(difficulty, out Monster[] monsters))
                { return monsters; }

            return new Monster[0];
        }

        //던전첫메뉴
        public static void InDungeonUI()
        {
            Console.Clear();
            Console.WriteLine("던전에 입장했습니다.");
            Console.WriteLine($"<현재 {player.Name}의 상태>");

            player.DisplayCharacterInfo();

            Console.WriteLine("\n\n원하시는 행동을 입력해 주세요.");
            Console.WriteLine("1. 던전진행");
            Console.WriteLine("\n0. 마을로 돌아가기");

            int result = Program.CheckInput(0, 1);

            switch (result)
            {
                case 0:
                    Program.DisplayMainUI();
                    break;
                case 1:
                    ChooseStage();
                    break;
            }
        }

        public static void ChooseStage()
        {
            Console.WriteLine("\n\n원하시는 스테이지(난이도)를 입력해 주세요.");
            Console.WriteLine("\n\n1. 쉬움");
            Console.WriteLine("2. 보통");
            Console.WriteLine("3. 어려움");
            Console.WriteLine("\n0. 마을로 돌아가기");

            int result = Program.CheckInput(0, 3);

            switch (result)
            {
                case 0:
                    Program.DisplayMainUI();
                    break;
                case 1:
                    StartStage(Difficulty.Easy);//쉬움 스테이지 맵db연결, 쉬움몬스터구성
                    break;
                case 2:
                    StartStage(Difficulty.Normal);//보통 스테이지 맵db연결, 보통몬스터구성
                    break;
                case 3:
                    StartStage(Difficulty.Hard);//어려움 스테이지 맵db연결, 어려움몬스터구성
                    break;
            }
        }

        private static void StartStage(Difficulty difficulty)
        {
            currentDifficulty = difficulty; //현재난이도설정
            monster = GetMonsterByDifficulty(difficulty);
            DungeonMap selectedMap = Program.mapDb[(int)difficulty - 1];
            Stage.PlayerMove(1, 1, selectedMap);
        }

        public void EncounterUI()
        {
            Console.Clear();
            Console.WriteLine("적과 마주쳤습니다!");
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
                    player.Hp -= 50;
                    Stage.PlayerMove(1, 1, Program.mapDb[(int)currentDifficulty - 1]);
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
            Console.WriteLine("1. 기본공격하기{공격설명메서드}");
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

            //출현몬스터 표시

            Console.WriteLine("\n\n{내정보}");
            Console.WriteLine($"LV.{player.Level} {player.Name}");
            Console.WriteLine($"HP : {player.Hp}/{player.MaxHp}");
            //Console.WriteLine($"MP : {player.MP}/{player.MaxMP}"); <<마나

            Console.WriteLine("\n\n0. 다음");
            int result = Program.CheckInput(0, 0);

            switch (result)
            {
                case 0:
                    //배틀의 기본공격 메서드();
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

            Console.WriteLine("\n\n원하시는 스킬을 입력해주세요");
            Console.WriteLine("1. 스킬1");
            Console.WriteLine("2. 스킬2");
            Console.WriteLine("0. 돌아가기");

            int result = Program.CheckInput(0, 2);

            switch (result)
            {
                case 0:
                    MyBattlePhase();
                    break;
                case 1:
                    //스킬1메서드
                    break;
                case 2:
                    //스킬2메서드
                    break;
                    //혹은 배틀의 스킬 메서드
            }

        }
        //공격결과창
        public void ResultAttackDamageInfo()
        {
            Console.WriteLine("\n\nBattle !!");
            Console.WriteLine($"{player.Name}의 페이즈");
            //배틀클래스의 기본공격or스킬 메서드
            //Console.WriteLine($"\n\n{player.Name}의 공격!");
            //Console.WriteLine($"\nLv. {몬스터레벨} 을(를) 공격했습니다.[데미지 : {가한데미지}");

            //Console.WriteLine($"\n\nnLv. {몬스터레벨}");
            //Console.WriteLine($"HP {몬스터HP}");

            Console.WriteLine($"\n0. 다음");
            int result = Program.CheckInput(0, 0);

            switch (result)
            {
                case 0:
                    MonsterPhase();
                    break;
            }
        }

        //몬스터공격시 페이즈
        public void MonsterPhase()
        {
            Console.WriteLine("\n\nBattle !!");
            Console.WriteLine("ENEMY 페이즈");

            //배틀의 몬스터1 공격 메서드()
            //배틀의 몬스터2 공격 메서드()
            //배틀의 몬스터3 공격 메서드()

            Console.WriteLine($"\n\nLV.{player.Level} {player.Name}");
            Console.WriteLine($"HP : {player.Hp} -> {player.Hp-몬스터데미지}");
            //Console.WriteLine($"MP : {player.MP}/{player.MaxMP}"); <<마나

            Console.WriteLine("\n\n0. 다음");
            int result = Program.CheckInput(0, 0);

            switch (result)
            {
                case 0:
                    if (player.Hp <= 0) FailResult();
                    else { MyBattlePhase(); }
                    break;
            }
        }
        //승리결과창
        public void SuccessResult()
        {
            Console.WriteLine($"\n\n몬스터 격퇴 성공!");
            Console.WriteLine($"보상");
            CharacterCustom.GainExperience(몬스터1경험치+몬스터2경험치+몬스터3경험치);
            Console.WriteLine($"{player.Gold}->{player.Gold+몬스터1,2,3골드}");

            Console.WriteLine($"0. 다음");
            int result = Program.CheckInput(0, 0);

            switch (result)
            {
                case 0:
                    if (맵db에몬스터가없다면)Program.DisplayMainUI();
                    else { 기존맵db; }
                    break;
            }
        }
        //공략실패결과창
        public void FailResult()
        {
            Console.WriteLine("\n\n공략 실패..");
            Console.WriteLine("\n\n0.회복의 방으로 간다.");
            int result = Program.CheckInput(0, 0);

            switch (result)
            {
                case 0:
                    Program.DiplayPotionUI();
                    break;
            }
        }
    }

}
