using System;
namespace Text_RPG_24Group
{
     public class Program
    {
        public static CharacterCustom player;
        public static Item[] itemDb;
        private static Monster[] monsterDb;
        private static Battle[] playerBattleDb;
        private static Battle[] monsterBattleDb;
        private static Poition[] poitionDb;
        public static Quest[] questDb;
        private static Poition potion;
        private static string characterName;
        private static int selectedJob;


        static void Main(string[] args)
        {
            StartDisplay();
            SetData();
            DisplayMainUI();
        }
        public static void StartDisplay()
        {

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("\n원하시는 이름을 설정해주세요.");

            characterName = Console.ReadLine();//이름 입력

            Console.WriteLine("직업을 선택해주세요.");
            Console.WriteLine("\n\n1. 전사");
            Console.WriteLine("2. 마법사");
            Console.WriteLine("3. 도적");

            selectedJob = int.Parse(Console.ReadLine());//직업선택
            if (string.IsNullOrEmpty(characterName))
            {
                Console.WriteLine("이름을 다시 입력해주세요.");
                characterName = Console.ReadLine();
            }
        }

        protected static void SetData()
        {
            //캐릭터생성

            player = new CharacterCustom(characterName, selectedJob);

            potion = new Poition("빨간포션", 30, "HP를 30 회복합니다.", 3);

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

            playerBattleDb = new Battle[] // 직업, 이름, 설명, 스킬 벨류, 타겟 수, 스킬 구분
            {

                //new Battle(player.Job, "기본공격", "하나의 적을 공격합니다.", 1, player.Atk, 1, 0),
                //new Battle(player.Job, "강공격", "하나의 적을 강하게 공격합니다.", 2, player.Atk, 1, 10),
                //new Battle(player.Job, "연속공격", "2명의 적을 랜덤으로 공격합니다.", 3, player.Atk, 2, 15),

                //new battle(1, "기본공격", "적을 공격합니다.", 1, 1, 10, false),
                //new battle(1, "깊게 베기", "적을 강하게 공격합니다.", 2, 1, 10, true),
                //new battle(1, "연속공격", "적을 랜덤으로 공격합니다.",  1.5f, 2, 10, true),
                //new battle(2, "기본공격", "적을 공격합니다.", 1, 1, 15, false),
                //new battle(2, "폭발", "적을 강하게 공격합니다.", 2, 1, 15, true),
                //new battle(2, "전기 사슬", "적을 랜덤으로 공격합니다.",  1.5f, 2, 15, true),
                //new battle(3, "기본공격", "적을 공격합니다.", 1, 1, 12, false),
                //new battle(3, "암습", "적을 강하게 공격합니다.", 2, 1, 12, true),
                //new battle(3, "수리검 던지기", "적을 랜덤으로 공격합니다.",  1.5f, 2, 12, true),

            };
          

            monsterBattleDb = new Battle[] // 이름, 설명, 데미지
            {
                new Battle("점액 뿌리기", "산성 점액에 의해 화상을 약간 입었다.", 5),
                new Battle("돌맹이 던지기", "단단한 돌에 맞았다.", 10),
                new Battle("끌어안기", "뼈조각에 찔렸다.", 10),
                new Battle("휘두르기", "느리지만 강력한 공격에 맞았다.", 5),
                new Battle("저주", "지독한 저주에 걸렸다.", 20),
            };

            questDb = new Quest[]//돈보상, 목표1,목표2,시작불값,클리어불값
            {
                new Quest(50,0,0,false,false),
                new Quest(100,0,0,false,false),
                new Quest(200,0,0,false,false)

            };

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
            Console.WriteLine("4. 퀘스트");
            Console.WriteLine("5. 회복의 방");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int result = CheckInput(1, 5);
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
                case 4:
                    Quest.QuestMain();
                    break;
                case 5:
                    DiplayPotionUI();
                    break;
            }
        }

        static void DisplayStatUI()
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");

            if (player == null)
            {
                Console.WriteLine("플레이어 객체가 null입니다. 먼저 캐릭터를 생성하세요.");
                DisplayMainUI(); // 메인 메뉴로 돌아가게 함
                return;
            }

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

        static void DiplayPotionUI()
        {
            Console.Clear();
            Console.WriteLine("회복의 방.");
            Console.WriteLine("\n포션을 사용하면 체력을 30 회복 할 수 있습니다."+"(남은포션 :" +potion.Count+")");
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("1. 회복하기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, 1);

            switch (result)
            {
                case 0:
                    DisplayMainUI();
                    break;

                case 1:

                    if (potion.Count > 0)
                    {
                        potion.Count--;
                        Program.player.Hp += 30;
                        Console.WriteLine("\nHP가 30 회복되었습니다.");
                        DiplayPotionUI();
                    }

                    else

                    Console.WriteLine("포션의 수가 부족합니다.");
                    DiplayPotionUI();

                    break;

            }



        }



        static void DisplayDungeonUI()
        {

        }

        public static int CheckInput(int min, int max)
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