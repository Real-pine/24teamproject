using System;
namespace Text_RPG_24Group
{
    class Program
    {
        private static CharacterCustom player;
        private static Item[] itemDb;
        private static Monster[] monsterDb;
        private static Battle[] playerBattleDb;
        private static Battle[] monsterBattleDb;
        private static Poition[] poitionDb;

        static void Main(string[] args)
        {
            SetData();
            StartDisplay();
            DisplayMainUI();
        }

        static void SetData()
        {
            //캐릭터생성
            string characterName = "";
            int selectedJob = 0;
            CharacterCustom player = new CharacterCustom(characterName, selectedJob);

            itemDb = new Item[]
            {
            new Item("수련자의 갑옷", 1, 5,"수련에 도움을 주는 갑옷입니다. ",1000),
            new Item("무쇠갑옷", 1, 9,"무쇠로 만들어져 튼튼한 갑옷입니다. ",2000),
            new Item("스파르타의 갑옷", 1, 15,"스파르타의 전사들이 사용했다는 전설의 갑옷입니다. ",3500),
            new Item("낣은 검", 0, 2,"쉽게 볼 수 있는 낡은 검 입니다. ",600),
            new Item("청동 도끼", 0, 5,"어디선가 사용됐던거 같은 도끼입니다. ",1500),
            new Item("스파르타의 창", 0, 7,"스파르타의 전사들이 사용했다는 전설의 창입니다. ",2500)
            };
            monsterDb = new Monster[]//이름, 이야기, 레벨, 공격, 방어, 체력, 돈
          {
            new Monster("슬라임","몬스터 중에서도 가장 약한 몹이다.",1,5,0,5,50),//레벨, 공격, 방어, 체력, 돈
            new Monster("고블린","욕심이 많고 주로 약탈을 일삼는다.",2,10,0,10,100),
            new Monster("스켈레톤","뼈로 이루어진 고대 사람이였던 것",3,10,0,15,150),
            new Monster("골램","자연적인 현상에의해 돌에 생명력이 들어갔다.",5,5,10,20,200),
            new Monster("악마","고대부터 존재했던 것",10,20,5,50,1000),

          };
            playerBattleDb = new Battle[] // 직업, 이름, 설명, 스킬, 스킬 데미지, 타겟 수, 마력
            {
                new Battle(player.Job, "기본공격", "하나의 적을 공격합니다.", 1, player.Atk, 1, 0),
                new Battle(player.Job, "강공격", "하나의 적을 강하게 공격합니다.", 2, player.Atk, 1, 10),
                new Battle(player.Job, "연속공격", "2명의 적을 랜덤으로 공격합니다.", 3, player.Atk, 2, 15),
            };
            monsterBattleDb = new Battle[] // 이름, 설명, 데미지
            {
                new Battle("점액 뿌리기", "산성 점액에 의해 화상을 약간 입었다.", 5),
                new Battle("돌맹이 던지기", "단단한 돌에 맞았다.", 10),
                new Battle("끌어안기", "뼈조각에 찔렸다.", 10),
                new Battle("휘두르기", "느리지만 강력한 공격에 맞았다.", 5),
                new Battle("점액 뿌리기", "지독한 저주에 걸렸다.", 20),
            };
            poitionDb = new Poition[]
           {
            new Poition("레드포션", 1, 5,"hp를 30 회복합니다. "),

           };

        }

        static void StartDisplay()
        {

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("\n원하시는 이름을 설정해주세요.");

            string characterName = Console.ReadLine();//이름 입력

            Console.WriteLine("직업을 선택해주세요.");
            Console.WriteLine("\n\n1. 전사");
            Console.WriteLine("2. 마법사");
            Console.WriteLine("3. 도적");

            int selectedJob = int.Parse(Console.ReadLine());//직업선택

        }

        static void DisplayMainUI()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");


            int result = CheckInput(1, 3);

            switch (result)
            {
                case 1:
                    DisplayStatUI();
                    break;

                case 2:
                    DisplayInventoryUI();
                    break;

                case 3:
                    DisplayShopUI();
                    break;
            }
        }

        static void DisplayStatUI()
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");

            player.DisplayCharacterInfo();

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine();

            int result = CheckInput(0, 0);

            switch (result)
            {
                case 0:
                    DisplayMainUI();
                    break;
            }
        }

        static void DisplayInventoryUI()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            player.DisplayInventory(false);

            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, 1);

            switch (result)
            {
                case 0:
                    DisplayMainUI();
                    break;

                case 1:
                    DisplayEquipUI();
                    break;
            }
        }

        static void DisplayEquipUI()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            player.DisplayInventory(true);

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, player.InventoryCount);

            switch (result)
            {
                case 0:
                    DisplayInventoryUI();
                    break;

                default:

                    int itemIdx = result - 1;
                    Item targetItem = itemDb[itemIdx];
                    player.EquipItem(targetItem);

                    DisplayEquipUI();
                    break;
            }
        }

        static void DisplayShopUI()
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < itemDb.Length; i++)
            {
                Item curItem = itemDb[i];

                string displayPrice = (player.HasItem(curItem) ? "구매완료" : $"{curItem.Price} G");
                Console.WriteLine($"- {curItem.ItemInfoText()}  |  {displayPrice}");
            }

            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, 1);

            switch (result)
            {
                case 0:
                    DisplayMainUI();
                    break;

                case 1:
                    DisplayBuyUI();
                    break;
            }
        }

        static void DisplayBuyUI()
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < itemDb.Length; i++)
            {
                Item curItem = itemDb[i];

                string displayPrice = (player.HasItem(curItem) ? "구매완료" : $"{curItem.Price} G");
                Console.WriteLine($"- {i + 1} {curItem.ItemInfoText()}  |  {displayPrice}");
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, itemDb.Length);

            switch (result)
            {
                case 0:
                    DisplayShopUI();
                    break;

                default:
                    int itemIdx = result - 1;
                    Item targetItem = itemDb[itemIdx];

                    // 이미 구매한 아이템이라면?
                    if (player.HasItem(targetItem))
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                        Console.WriteLine("Enter 를 눌러주세요.");
                        Console.ReadLine();
                    }
                    else // 구매가 가능할떄
                    {
                        //   소지금이 충분하다
                        if (player.Gold >= targetItem.Price)
                        {
                            Console.WriteLine("구매를 완료했습니다.");
                            player.BuyItem(targetItem);
                        }
                        else
                        {
                            Console.WriteLine("골드가 부족합니다.");
                            Console.WriteLine("Enter 를 눌러주세요.");
                            Console.ReadLine();
                        }

                        //   소지금이 부족핟
                    }

                    DisplayBuyUI();
                    break;
            }
        }

        static void DisplayDungeonUI()
        {

        }

        static void DisplayQuestUI()
        {
            Console.WriteLine("Quest!!\r\n\r\n1. 마을을 위협하는 미니언 처치\r\n2. 장비를 장착해보자\r\n3. 더욱 더 강해지기!\r\n\r\n\r\n원하시는 퀘스트를 선택해주세요.\r\n>>");
            int input = CheckInput(1, 3);
            Console.Clear();
            switch (input)
            {
                case 1:
                    DisplayQuestUI1(1);//수정 필요
                    break;
                case 2:
                    DisplayQuestUI2();
                    break;
                case 3:
                    DisplayQuestUI3();
                    break;
                default:
                    break;
            }
        }
        static void DisplayQuestUI1(int killNum)// 미니언 5마리 처지 저장공간//보상아이템을 멀 줄지 
        {
             Console.WriteLine("Quest!!\r\n\r\n마을을 위협하는 미니언 처치\r\n\r\n이봐! 마을 근처에 슬라임들이 너무 많아졌다고 생각하지 않나?\r\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n모험가인 자네가 좀 처치해주게!\r\n\r\n");
            Console.WriteLine($"- 미니언 5마리 처치 (0/5)\r\n\r\n- 보상- \r\n\t쓸만한 방패 x 1\r\n\t5G\r\n\r\n");
             Console.WriteLine($"1. 수락\r\n2. 거절\r\n원하시는 행동을 입력해주세요.\r\n>>");
            int input = CheckInput(1, 2);
            if (input == 1) { Console.WriteLine("퀘스트 수락 로직 구현"); }
                 else { DisplayQuestUI(); }
        }
        static void DisplayQuestUI2()// 무기방어구 장착 저장공간//보상아이템을 멀 줄지 
        {
            Console.WriteLine("Quest!!\r\n\r\n장비를 장착해보자\r\n\r\n이봐! 좋은 모험가라면 좋은 장비를 써야하는 법...\r\n장비를 장착하면 던전을 조금 더 쉽게 공략하지!\r\n한번 장비를 껴봐!\r\n\r\n");
            Console.WriteLine($"- 무기와 방어구 장착 (0/1)(0/1)\r\n\r\n- 보상- \r\n\t쓸만한 방패 x 1\r\n\t50G\r\n\r\n1. 수락\r\n2. 거절\r\n원하시는 행동을 입력해주세요.\r\n>>");
            int input = CheckInput(1, 2);
            if (input == 1) { Console.WriteLine("퀘스트 수락 로직 구현"); }
            else { DisplayQuestUI(); }
        }
        static void DisplayQuestUI3()// 스켈레톤 5마리 처지 저장공간//보상아이템을 멀 줄지 
        {
            Console.WriteLine("Quest!!\r\n\r\n더욱 더 강해지기\r\n\r\n이봐! 진정한 모험가는 수련을 해야된다네...\r\n스켈레톤 처치를 부탁하네!\r\n\r\n");
            Console.WriteLine($"- 스켈레톤 3마리 처치 (0/3)\r\n\r\n- 보상- \r\n\t쓸만한 방패 x 1\r\n\t50G\r\n\r\n1. 수락\r\n2. 거절\r\n원하시는 행동을 입력해주세요.\r\n>>");
            int input = CheckInput(1, 2);
            if (input == 1) { Console.WriteLine("퀘스트 수락 로직 구현"); }
            else { DisplayQuestUI(); }
        }
        static int CheckInput(int min, int max)
        {
            int result;
            while (true)
            {
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out result);
                if (isNumber)
                {
                    if (result >= min && result <= max)
                        return result;
                }
                Console.WriteLine("잘못된 입력입니다!!!!");
            }
        }
    }
}