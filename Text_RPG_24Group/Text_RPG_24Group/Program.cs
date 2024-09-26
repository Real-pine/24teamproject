using System;
namespace Text_RPG_24Group
{
    class Program
    {
        private static CharacterCustom player;
        private static Item[] itemDb;

        static void Main(string[] args)
        {
            SetData();
            StartDisplay();
            DisplayMainUI();
        }

        static void SetData()
        {
            itemDb = new Item[]
            {
            new Item("수련자의 갑옷", 1, 5,"수련에 도움을 주는 갑옷입니다. ",1000),
            new Item("무쇠갑옷", 1, 9,"무쇠로 만들어져 튼튼한 갑옷입니다. ",2000),
            new Item("스파르타의 갑옷", 1, 15,"스파르타의 전사들이 사용했다는 전설의 갑옷입니다. ",3500),
            new Item("낣은 검", 0, 2,"쉽게 볼 수 있는 낡은 검 입니다. ",600),
            new Item("청동 도끼", 0, 5,"어디선가 사용됐던거 같은 도끼입니다. ",1500),
            new Item("스파르타의 창", 0, 7,"스파르타의 전사들이 사용했다는 전설의 창입니다. ",2500)
            };
        }

        static void StartDisplay()
        {

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("\n원하시는 이름을 설정해주세요.");

            player.Name = Console.ReadLine();

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