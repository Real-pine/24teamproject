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
        public static Monster[] currentStageMonster;
        public static Stage[] stages;
        public static Battle battle;
        private static Difficulty currentDifficulty; //현재난이도저장
        private static List<Monster> activeMonster; //현재 전투중인 몬스터
        public static Dictionary<Difficulty, Monster[]> difficultyMonsters;
        private static Random random = new Random();

        //난이도 세팅
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
        //난이도별 몬스터 출현 그룹 설정
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
        //난이도에 맞는 몬스터 개수 생성기
        private static void GenerateActiveMonsters()
        {
            activeMonster = new List<Monster>();
            int monsterCount = currentDifficulty == Difficulty.Easy ? 2 : 3;

            for ( int i = 0; i < monsterCount; i++ )
            {
                Monster newMonster = SelectRandomMonster();
                activeMonster.Add(new Monster(newMonster.MonsterName, newMonster.Monstertell,
                newMonster.MonsterLev, newMonster.MonsterAtk, newMonster.MonsterDef,
                newMonster.MonsterHp, newMonster.MonsterGold));
            }
        }

        //몬스터 목록 표시 메서드
        private static void DisplayMonsters()
        {
            Console.WriteLine("\n출현한 몬스터:");
            for(int i = 0; i<activeMonster.Count; i++)
            {
                Monster monster = activeMonster[i];
                string hpDisplay = monster.curHp > 0 ? $"{monster.curHp}/{monster.MonsterHp}" : "Dead";
                Console.WriteLine($"{i + 1}. Lv. {monster.MonsterLev} {monster.MonsterName} (HP : {hpDisplay})");
            }
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
            currentDifficulty = difficulty; //난이도설정
            currentStageMonster = GetMonsterByDifficulty(difficulty);
            DungeonMap selectedMap = Program.mapDb[(int)difficulty - 1];
            battle = new Battle { player = player }; // Battle인스턴스 생성
            Stage.PlayerMove(1, 1, selectedMap);
        }

        private static Monster SelectRandomMonster()
        {
            int index = random.Next(currentStageMonster.Length);
            return currentStageMonster[index];
        }

        public static void EncounterUI()
        {
            Console.Clear();
            Console.WriteLine("적과 마주쳤습니다!");
            Console.WriteLine("\n\nBattle !!");
            
            GenerateActiveMonsters();
            DisplayMonsters();

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
                    Console.WriteLine($"{player.Name}의 HP가 50 감소했습니다. (현재 HP: {player.Hp}/{player.MaxHp})");
                    Console.WriteLine("아무 키나 누르면 계속합니다...");
                    Console.ReadKey();
                    Stage.PlayerMove(Stage.PlayerX, Stage.PlayerY, Program.mapDb[(int)currentDifficulty - 1]); 
                    break;
                case 1:
                    MyBattlePhase();
                    break;
            }
        }

        public static void MyBattlePhase()
        {
            Console.Clear();
            Console.WriteLine("\n\nBattle !!");
            Console.WriteLine($"{player.Name}의 페이즈");

            DisplayMonsters();

            Console.WriteLine("\n\n{내정보}");
            Console.WriteLine($"LV.{player.Level} {player.Name}");
            Console.WriteLine($"HP : {player.Hp}/{player.MaxHp}");
            //Console.WriteLine($"MP : {player.MP}/{player.MaxMP}"); <<마나

            Console.WriteLine("\n\n원하시는 행동을 입력해주세요");
            Console.WriteLine("1. 기본공격하기");
            Console.WriteLine("2. 스킬사용");
            Console.WriteLine("0. 도망가기(HP 50감소)");

            int result = Program.CheckInput(0, 2);

            switch (result)
            {
                case 0:
                    player.Hp -= 50;
                    Console.WriteLine($"{player.Name}의 HP가 50 감소했습니다. (현재 HP: {player.Hp}/{player.MaxHp})");
                    Console.WriteLine("아무 키나 누르면 계속합니다...");
                    Console.ReadKey();
                    Stage.PlayerMove(Stage.PlayerX, Stage.PlayerY, Program.mapDb[(int)currentDifficulty - 1]);
                    break;
                case 1:
                    PlayerBasicAttack();
                    break;
                case 2:
                    PlayerSkillAttack();
                    break;
            }
        }

        //기본공격시 페이즈
        public static void PlayerBasicAttack()
        {
            Console.WriteLine("\n\nBattle !!");
            Console.WriteLine($"{player.Name}의 페이즈");

            DisplayMonsters();

            Console.WriteLine("\n\n{내정보}");
            Console.WriteLine($"LV.{player.Level} {player.Name}");
            Console.WriteLine($"HP : {player.Hp}/{player.MaxHp}");
            //Console.WriteLine($"MP : {player.MP}/{player.MaxMP}"); <<마나
            Console.WriteLine("공격할 몬스터를 선택하세요.");
            int target = Program.CheckInput(1, activeMonster.Count) - 1;
            Monster targetMonster = activeMonster[target];
            battle.BasicAttack(targetMonster);
            CheckMonsterDefeated(targetMonster);

            if (activeMonster.Count == 0)
            {
                Console.WriteLine($"\n0. 다음");
                int result = Program.CheckInput(0, 0);

                switch (result)
                {
                    case 0:
                        SuccessResult();
                        break;
                }
            }
            else
            {
                Console.WriteLine($"\n0. 다음");
                int result = Program.CheckInput(0, 0);

                switch (result)
                {
                    case 0:
                        MonsterPhase();
                        break;
                }
            }
        }


        //스킬공격시페이즈
        public static void PlayerSkillAttack()
        {
            Console.WriteLine("\n\nBattle !!");
            Console.WriteLine($"{player.Name}의 페이즈");

            battle.Skill(player.Job);
            int skillChoice = Program.CheckInput(1, 2);

            Console.WriteLine("대상 몬스터를 선택하세요:");
            DisplayMonsters();
            int target = Program.CheckInput(1, activeMonster.Count) -1;
            Monster targetMonster = activeMonster[target];

            battle.SkillAttack(targetMonster, skillChoice);
            CheckMonsterDefeated(targetMonster);

            if (activeMonster.Count == 0)
            {
                Console.WriteLine($"\n0. 다음");
                int result = Program.CheckInput(0, 0);

                switch (result)
                {
                    case 0:
                        SuccessResult();
                        break;
                }
            }
            else
            {
                Console.WriteLine($"\n0. 다음");
                int result = Program.CheckInput(0, 0);

                switch (result)
                {
                    case 0:
                        MonsterPhase();
                        break;
                }
            }
        }

        //몬스터 사망확인 메서드
        private static void CheckMonsterDefeated(Monster monster)
        {
            if (monster.curHp <= 0)
            {
                Console.WriteLine($"{monster.MonsterName}이(가) 쓰러졌습니다!");
                activeMonster.Remove(monster);
            }
        }
        

        //몬스터공격시 페이즈
        public static void MonsterPhase()
        {
            Console.WriteLine("\n\nBattle !!");
            Console.WriteLine("ENEMY 페이즈");

            foreach (var monster in activeMonster)
            {
                battle.MonsterAttack(monster);
                if (player.Hp <= 0) break;
            }

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
        public static void SuccessResult()
        {
            Console.WriteLine($"\n\n몬스터 격퇴 성공!");
            Console.WriteLine($"보상");
            //경험치획득 로직
            int totalEXP = activeMonster.Sum(m => m.MonsterLev);
            player.GainExperience(totalEXP);
            Console.WriteLine($"획득 경험치: {totalEXP}");
            //골드획득 로직
            int totlaGold = activeMonster.Sum(m => m.MonsterGold);
            int oldGold = player.Gold;
            player.Gold += totlaGold;
            Console.WriteLine($"Gold: {oldGold} -> {player.Gold} (+{totlaGold}");
            //랜덤 상점 아이템 드롭(15프로 이하)
            if (random.Next(100) < 15)
            {
                Item droppedItem = GetRandomItem();
                player.GetItem(droppedItem);
                Console.WriteLine($"아이템 획득: {droppedItem.Name}");
            }

            Console.WriteLine($"0. 다음");
            int result = Program.CheckInput(0, 0);

            switch (result)
            {
                case 0:
                    //현재 위치의 몬스터 제거
                    RemoveMonsterFromMap(Stage.PlayerX, Stage.PlayerY);

                    if (AreAllMonsterDefeated())
                    {
                        Console.WriteLine("던전의 모든 몬스터를 물리쳤습니다!");
                        Console.WriteLine("0. 마을로 돌아가기");
                        Program.CheckInput(0, 0);
                        Program.DisplayMainUI();
                    }
                    else
                    {
                        Stage.PlayerMove(Stage.PlayerX, Stage.PlayerY, Program.mapDb[(int)currentDifficulty - 1]);
                    }
                    break;
            }
        }

        //아이템 랜덤 셋
        static private Item GetRandomItem()
        {
            int index = random.Next(Program.itemDb.Length);
            return Program.itemDb[index];
        }

        //공략실패결과창
        public static void FailResult()
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

        //전투승리시 기존맵의 2(적)지우기
        private static void RemoveMonsterFromMap(int x, int y)
        {
            Program.mapDb[(int)currentDifficulty - 1].SetTile(y, x, 0);
        }
        
        //맵에 적(2)이 남아있나 확인하는 로직
        private static bool AreAllMonsterDefeated()
        {
            DungeonMap currentMap = Program.mapDb[(int)currentDifficulty - 1];
            for ( int y = 0; y < currentMap.GetHeight(); y++)
            {
                for(int x = 0; x < currentMap.GetWidth(); x++)
                {
                    if (currentMap.GetTile(y, x) == 2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

}
