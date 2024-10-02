using System;
using System.Threading;
using System.Numerics;
using static Text_RPG_24Group.CharacterCustom;
using Newtonsoft.Json;
using System.Threading;
using System.Diagnostics;



namespace Text_RPG_24Group
{
    public class Program
    {
        public static CharacterCustom player;
        public static Item[] itemDb;
        public static Monster[] monsterDb;
        public static Quest[] questDb;
        private static Herb[] herbDb;
        private static Poition potion;       
        private static string characterName;
        private static int selectedJob;
        public static DungeonMap[] mapDb;
        public static SoundManager BGMManager;
        public static SoundManager SoundEffectManager;
        private static Pub pub;
        static void Main(string[] args)
        {
            SetData();
            DungeonPlay.player = player; //DungeonPlay클래스의 player필드에 설정
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
            pub = new Pub();
            BGMManager = new SoundManager(@"C:\Users\BaekSeungWoo\Documents\GitHub\24teamproject\Text_RPG_24Group");
            SoundEffectManager = new SoundManager(@"C:\Users\BaekSeungWoo\Documents\GitHub\24teamproject\Text_RPG_24Group");
        herbDb = new Herb[]

            {
                new Herb("나무 뿌리", "10개 합치면 포션",0),
                new Herb("빨간 잎", "3개 합치면 포션",0)
            };

            potion = new Poition("빨간포션", 30, "HP를 30 회복합니다.", 3);
            //주소 예시//C:\Users\BaekSeungWoo\Documents\GitHub\24teamproject\Text_RPG_24Group//뒤에 @꼭 붙히세요
            itemDb = new Item[]//string name, int type, int value, string desc, int price
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
            questDb = new Quest[]//돈보상, 목표1,목표2,시작불값,클리어불값,0은퀘다시가능,1은2가되서불가
            {
                new Quest(50,0,0,false,false,0),
                new Quest(100,0,0,false,false,1),
                new Quest(200,0,0,false,false,0)
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
            BGMManager.PlayBGM(BGMManager, BGMManager.SoundVillage, 45);
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 던전");
            Console.WriteLine("4. 마을");
            Console.WriteLine("5. 허브산");
            Console.WriteLine("6. 저장하기");
            Console.WriteLine("7. 환경설정");
            Console.WriteLine("8. 게임종료");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(1, 8);
            BGMManager.PlayBGM(BGMManager, BGMManager.SoundVillage, 45);
            switch (result)
            {
                case 1:
                    DisplayStatUI();
                    break;
                case 2:
                    DisplayInventoryUI();
                    break;
                case 3://DisplayShopUI();
                    DisplayDungeonUI();
                    break;
                case 4://Program.questDb[0].QuestMain();
                    Pub.PubMainUI();
                    break;
                case 5://DiplayPotionUI();
                    HerbUI();
                    break;
                case 6:
                    SaveUI();
                    break;
                case 7:
                    UserSettings();
                    break;
                case 8:
                    ExitGame();
                    break;
                default:
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

        static void HerbUI()
        {
            Console.Clear();
            Console.WriteLine("약초 산");
            Console.WriteLine("약초를 캐서 포션을 만들자!" );
            Console.WriteLine("\n50골드가 차감됩니다" + "\t(현재골드" + player.Gold + "원)");

            Console.WriteLine("\n1.약초캐기");
            Console.WriteLine("2.약초가방");
            Console.WriteLine("\n0.돌아가기");

            int result = CheckInput(0, 2);

            switch (result)
            {
                case 0:
                    DisplayMainUI();
                    break;

                case 1:

                    GetHerbs();

                    break;

                case 2:

                    ShowHerbBag();

                    break;
            }


        }
        static void GetHerbs()
        {
            Random rand = new Random();
            int rootCount = rand.Next(0, 6); // 0 to 5
            int leafCount = rand.Next(0, 2); // 0 to 1

            herbDb[0].Count += rootCount;
            herbDb[1].Count += leafCount;
            player.Gold -= 50;

            Console.WriteLine("약초를 얻었습니다!");
            Console.WriteLine($"나무 뿌리: {rootCount}개");
            Console.WriteLine($"빨간 잎: {leafCount}개");
            Console.ReadLine();
            HerbUI();
        }

        static void ShowHerbBag()
        {
            Console.Clear();
            Console.WriteLine("약초 가방\n");
            for (int i = 0; i < herbDb.Length; i++)
            {
                Herb herb = herbDb[i];
                Console.WriteLine($"{herb.Name}: {herb.Count}개");
            }


            Console.WriteLine("\n1. 물약제작");
            Console.WriteLine("0. 돌아가기");
            int result = CheckInput(0, 1);

            switch (result)
            {
                case 0:
                    HerbUI();
                    break;

                case 1:
                    SelectHerbForPotion();
                    break;
            }
            Console.ReadLine();
            HerbUI();
        }
        static void SelectHerbForPotion()
        {
            Console.Clear();
            Console.WriteLine("어떤 약초로 포션을 만들겠습니까?");
            Console.WriteLine("\n나무뿌리 10개 = 빨간포션 1개");
            Console.WriteLine("빨간잎 3개 = 빨간포션 1개\n");

            for (int i = 0; i < herbDb.Length; i++)
            {
                Herb herb = herbDb[i];
                Console.WriteLine($"{i + 1}. {herb.Name} ({herb.Count}개)");
            }

            int herbChoice = CheckInput(1, herbDb.Length) - 1;

            MakePotion(herbChoice);
        }

        static void MakePotion(int herbIndex)
        {
            Herb herb = herbDb[herbIndex];

            if ((herbIndex == 0 && herb.Count >= 10) || (herbIndex == 1 && herb.Count >= 3))
            {
                if (herbIndex == 0)
                {
                    herb.Count -= 10;
                }
                else if (herbIndex == 1)
                {
                    herb.Count -= 3;
                }
                potion.Count++;
                Console.WriteLine("포션이 제작되었습니다!");
                SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundSelect);
            }
            else
            {
                Console.WriteLine("재료가 부족합니다.");
                SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundHurt);
            }

            Console.ReadLine();
            ShowHerbBag();
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
                    Item targetItem = player.Inventory[itemIdx];
                    player.EquipItem(targetItem);

                    DisplayEquipUI();
                    break;
            }
        }

        public static void DisplayShopUI()
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
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, 2);

            switch (result)
            {
                case 0:
                    Pub.PubMainUI();
                    break;

                case 1:
                    DisplayBuyUI();
                    break;
                case 2:
                    DisplaySellUI();
                    break;
            }
        }
        static void DisplaySellUI()
        {
            Console.Clear();
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.(80%가격으로 판매)");
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

                    if(player.IsEquipped(targetItem))
                    {
                        Console.WriteLine("장착된 아이템입니다. 판매불가!");
                        Console.WriteLine("Enter 를 눌러주세요.");
                        Console.ReadLine();
                        SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundHurt);
                    }
                    else if (player.HasItem(targetItem))// 이미 구매한 아이템이라면?
                    {
                        Console.WriteLine("아이템을 판매합니다.");
                        player.Gold += (int)(targetItem.Price*0.8f);
                        player.RemoveItem(targetItem);
                        Console.WriteLine("Enter 를 눌러주세요.");
                        Console.ReadLine();
                        SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundSelect);
                    }
                    else // 아이템이 없을 때
                    {
                        Console.WriteLine("없는 아이템입니다.");
                        Console.WriteLine("Enter 를 눌러주세요.");
                        Console.ReadLine();
                        SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundHurt);
                    }

                    DisplaySellUI();
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
                            SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundSelect);
                        }
                        else
                        {
                            Console.WriteLine("골드가 부족합니다.");
                            Console.WriteLine("Enter 를 눌러주세요.");
                            Console.ReadLine();
                            SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundHurt);
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
            Console.WriteLine("\n200골드를 지불하면 HP를 회복할수있습니다."+"(남은골드 :" +player.Gold+")");
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("1. 회복하기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, 1);

            switch (result)
            {
                case 0:
                    Pub.PubMainUI();
                    break;

                case 1:

                    if (player.Gold >= 200)
                    {
                        player.AgeUP();
                        player.Gold -= 200;
                        player.Hp = player.MaxHp;
                        Console.WriteLine("\nHP가 최대로 회복되었습니다.");
                        Console.ReadLine();
                        SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundSelect);
                        DiplayPotionUI();
                    }
                    else
                    {
                    Console.WriteLine("골드가 부족합니다");
                        SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundHurt);
                    }
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
                SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundButton);
                bool isNumber = int.TryParse(input, out result);
                if (isNumber)
                {
                    if (result >= min && result <= max)
                        return result;
                }
                Console.WriteLine("잘못된 입력입니다!!!!");
            }
        }

        static void SaveUI()
        {
            Console.Clear();
            Console.WriteLine("1.저장하기");
            Console.WriteLine("2.불러오기");
            Console.WriteLine("3.저장된 파일");
            Console.WriteLine("4.저장된 파일제거");
            Console.WriteLine("\n0.나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");


            int result = CheckInput(0, 4);

            switch (result)
            {
                case 0:
                    DisplayMainUI();
                    break;

                case 1:

                    Save();

                    break;

                case 2:

                    Load();

                    break;

                case 3:

                    ListSaves();

                    break;


                case 4:

                    DeleteSave();

                    break;
            }


        }        
       
        static void ListSaves()
        {
            SaveLoadSystem.ListSaves();
            Console.ReadLine();
            SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundSelect);
            SaveUI();

        }

        static void Save()
        {
            Console.Write("저장할 파일 이름을 입력하세요: ");
            string saveName = Console.ReadLine();
            SaveLoadSystem.SaveCharacter(player, saveName);
            Console.ReadLine();
            SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundSelect);
            SaveUI();
        }

        static void Load()
        {
            Console.Write("불러올 파일 이름을 입력하세요: ");
            string saveName = Console.ReadLine();
            player = SaveLoadSystem.LoadCharacter(saveName);       
            Console.ReadLine();
            SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundSelect);
            SaveUI();
        }

        static void DeleteSave()
        {
            Console.Write("삭제할 파일 이름을 입력하세요: ");
            string saveName = Console.ReadLine();
            SaveLoadSystem.DeleteSaveFile(saveName);
            Console.ReadLine();
            SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundSelect);
            SaveUI();
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
                        SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundSelect);
                        Console.WriteLine("\n배경음이 OFF 되었습니다. 일정 시간이 지나면 꺼집니다.");
                        Program.BGMManager.SoundBGMUserSet = false;
                    }
                    else if (Program.BGMManager.SoundBGMUserSet == false)
                    {
                        SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundSelect);
                        Console.WriteLine("\n배경음이 ON 되었습니다. 일정 시간이 지나면 켜집니다.");
                        Program.BGMManager.SoundBGMUserSet = true;
                    }
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

        public static void GameEnd(int age)
        {
            if (age != 40)
                return;

            Console.Clear();
            Console.WriteLine("...");
            Console.WriteLine("0. 은퇴");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int result = CheckInput(0, 0);
            // 게임 종료

            if (result == 0)
                Environment.Exit(0);
        }
        public static void ExitGame()
        {
            Console.Clear();
            Console.WriteLine("...");
            Console.WriteLine("게임을 종료하시겠습니까?");
            Console.WriteLine("0. Yes\n1. No");
            int result = CheckInput(0, 1);
            if (result == 0)return;
            else if (result == 1) DisplayMainUI();
        }
    }
}