using System;
using System.Threading;
using System.Numerics;
using static Text_RPG_24Group.CharacterCustom;
using Newtonsoft.Json;
using System.Threading;



namespace Text_RPG_24Group
{
    public class Program
    {
        public static CharacterCustom player;
        public static Item[] itemDb;
        public static Monster[] monsterDb;
        public static Quest[] questDb;
        private static Poition potion;       
        private static string characterName;
        private static int selectedJob;
        public static DungeonMap[] mapDb;
        private static SoundManager BGMManager;
        private static SoundManager SoundEffectManager;
    
        static void Main(string[] args)
        {
            SetData();
            DungeonPlay.player = player; //DungeonPlay클래스의 player필드에 설정
            //Program.BGMManager.PlayBGM(Program.BGMManager.SoundVillage, BGMManager, 45); //사운드(BGM)//(string형 사운드파일,int형 플레이 초)
            DisplayMainUI();
        }

        protected static void SetData()
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("\n원하시는 이름을 설정해주세요.\n");

            characterName = Console.ReadLine();//이름 입력

            Console.Clear();
            Console.WriteLine("직업을 선택해주세요.");
            Console.WriteLine("\n\n1. 전사");
            Console.WriteLine("2. 마법사");
            Console.WriteLine("3. 도적\n");

            selectedJob = int.Parse(Console.ReadLine());//직업선택
            if (string.IsNullOrEmpty(characterName))
            {
                Console.WriteLine("이름을 다시 입력해주세요.");
                characterName = Console.ReadLine();
            }
            player = new CharacterCustom(characterName, selectedJob);

            potion = new Poition("빨간포션", 30, "HP를 30 회복합니다.", 3);

            BGMManager = new SoundManager(@"C:\Users\BaekSeungWoo\Documents\GitHub\24teamproject\Text_RPG_24Group");
            SoundEffectManager = new SoundManager(@"C:\Users\BaekSeungWoo\Documents\GitHub\24teamproject\Text_RPG_24Group");
            //주소 예시//C:\Users\BaekSeungWoo\Documents\GitHub\24teamproject\Text_RPG_24Group//뒤에 @꼭 붙히세요
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
            questDb = new Quest[]//돈보상, 목표1,목표2,시작불값,클리어불값
            {
                new Quest(50,5,0,false,false),
                new Quest(100,0,0,false,false),
                new Quest(200,0,0,false,false)
            };
            mapDb = new DungeonMap[]
   {
        new DungeonMap(new int[,]
        {
            { 1, 3, 1, 1, 1, 1 }, //1벽, 2적,3마을
            { 1, 0, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 0, 1 },
            { 1, 2, 0, 1, 0, 1 },
            { 1, 0, 0, 2, 0, 1 },
            { 1, 1, 1, 1, 1, 1 }
        }),
        new DungeonMap(new int[,]
        {
            { 1, 3, 1, 1, 1, 1 },
            { 1, 0, 0, 1, 0, 1 },
            { 1, 1, 2, 1, 0, 1 },
            { 1, 0, 0, 1, 0, 1 },
            { 1, 0, 0, 2, 0, 1 },
            { 1, 1, 1, 1, 1, 1 }
        }),
        new DungeonMap(new int[,]
        {
            { 1, 3, 1, 1, 1, 1 },
            { 1, 0, 1, 0, 2, 1 },
            { 1, 0, 1, 0, 0, 1 },
            { 1, 0, 1, 2, 0, 1 },
            { 1, 0, 2, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 1 }
        })
   };
        }
        public static void DisplayMainUI()

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
            Console.WriteLine("6. 저장된 파일");
            //Console.WriteLine("7. 저장된 파일제거");
            Console.WriteLine("7. 던전입장");
            Console.WriteLine("8. 저장하기");
            Console.WriteLine("9. 불러오기");
            Console.WriteLine("10. 환경설정");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(1, 10);
            Program.SoundEffectManager.PlaySoundEffect(Program.SoundEffectManager.SoundButton, SoundEffectManager);//사운드(버튼)
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
                    Program.questDb[0].QuestMain();
                    break;
                case 5:
                    DiplayPotionUI();
                    break;
                case 6:
                    ListSaves();
                    break;
                case 7:
                    DisplayDungeonUI();
                    break;
                //case 7:
                //    DeleteSave();
                //    break;
                case 8:
                    Save();
                    break;
                case 9:
                    Load();
                    break;
                case 10:
                    UserSettings();
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
                            player.Gold -= targetItem.Price;
                            player.GetItem(targetItem);
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

        public static void DiplayPotionUI()
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
                        Console.ReadLine();
                        DiplayPotionUI();
                    }

                    else

                    Console.WriteLine("포션의 수가 부족합니다.");
                    Console.ReadLine();
                    DiplayPotionUI();

                    break;

            }

        }

        static void DisplayDungeonUI()
        {
            DungeonPlay.InDungeonUI();
        }

        public static int CheckInput(int min, int max)
        {
            int result;
            while (true)
            {
                string input = Console.ReadLine();
                Program.SoundEffectManager.PlaySoundEffect(Program.SoundEffectManager.SoundButton,SoundEffectManager);
                bool isNumber = int.TryParse(input, out result);
                if (isNumber)
                {
                    if (result >= min && result <= max)
                        return result;
                }
                Console.WriteLine("잘못된 입력입니다!!!!");
            }
        }

        static void ListSaves()
        {
            SaveLoadSystem.ListSaves();
            Console.ReadLine();
            DisplayMainUI();

        }
        static void Save()
        {
            Console.Write("저장할 파일 이름을 입력하세요: ");
            string saveName = Console.ReadLine();
            SaveLoadSystem.SaveCharacter(player, saveName);
            Console.ReadLine();
            DisplayMainUI();
        }

        static void Load()
        {
            Console.Write("불러올 파일 이름을 입력하세요: ");
            string saveName = Console.ReadLine();
            player = SaveLoadSystem.LoadCharacter(saveName);
         
            Console.ReadLine();
            DisplayMainUI();
        }

        static void DeleteSave()
        {
            Console.Write("삭제할 파일 이름을 입력하세요: ");
            string saveName = Console.ReadLine();
            SaveLoadSystem.DeleteSaveFile(saveName);
            Console.ReadLine();
            DisplayMainUI();
        }
        static void UserSettings()
        {
            Console.Clear();
            Console.WriteLine("\n\r환경설정\n");
            Console.WriteLine($"1. 배경음ON/OFF(현재{Program.BGMManager.SoundBGMUserSet})");
            Console.WriteLine($"2. 효과음ON/OFF(현재{Program.SoundEffectManager.SoundEffectUserSet})");
            Console.WriteLine("0. 돌아가기\n\r");
            Console.WriteLine("원하는 숫자 입력 : ");
            int result = CheckInput(0, 2);
            Console.Clear();
            switch (result)
            {
                case 1:
                    if (Program.BGMManager.SoundBGMUserSet == true)
                    {
                        Console.WriteLine("\n배경음이 OFF 되었습니다. 일정 시간이 지나면 꺼집니다.");
                       Program.BGMManager.SoundSize = 0f;
                    }
                    else if (Program.BGMManager.SoundBGMUserSet == false)
                    {
                        Console.WriteLine("\n배경음이 ON 되었습니다. 일정 시간이 지나면 켜집니다.");
                        Program.BGMManager.SoundSize = 1f;
                    }
                    Program.BGMManager.SoundBGMUserSet = !Program.BGMManager.SoundBGMUserSet;
                    Console.WriteLine("0. 돌아가기\n\r");
                    int result1 = CheckInput(0, 0);
                    break;
                case 2:
                    if (Program.SoundEffectManager.SoundEffectUserSet == true) Console.WriteLine("\n효과음이 OFF 되었습니다.");
                    else if (Program.SoundEffectManager.SoundEffectUserSet == false) Console.WriteLine("\n효과음이 ON 되었습니다.");
                    Program.SoundEffectManager.SoundEffectUserSet = !Program.SoundEffectManager.SoundEffectUserSet;
                    Console.WriteLine("0. 돌아가기\n\r");
                    int result2 = CheckInput(0, 0);
                    break;
                default:
                    break;
            }
            DisplayMainUI();
        }
    }
}