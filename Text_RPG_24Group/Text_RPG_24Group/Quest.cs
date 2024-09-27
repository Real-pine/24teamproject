using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_24Group
{
    public class Quest
    {
        public int Gold { set; get; }
        public int Goal1 { set; get; }
        public int Goal2 { set; get; }
        public bool QuestStart { set; get; }
        public bool QuestClear { set; get; }

        public Quest(int gold, int goal1, int goal2, bool start, bool clear)
        {
            Gold = gold;
            Goal1 = goal1;
            Goal2 = goal2;
            QuestStart = start;
            QuestClear = clear;
        }
        public static void QuestMain()
        {
            Console.Clear();
            Console.WriteLine("Quest!!\r\n\r\n0. 돌아가기\r\n1. 마을을 위협하는 미니언 처치\r\n2. 장비를 장착해보자\r\n3. 더욱 더 강해지기!\r\n\r\n\r\n원하시는 퀘스트를 선택해주세요.\r\n>>");
            int input = Program.CheckInput(0, 3);
            Console.Clear();
            switch (input)
            {
                case 0:
                    Program.DisplayMainUI();
                    break;
                case 1:
                    QuestDisplay(Program.questDb[0],0,1);// 퀘스트데이터베이스, 퀘스트타입번호, 보상무기인덱스 번호
                    break;
                case 2:
                    QuestDisplay(Program.questDb[1], 1, 1);
                    break;
                case 3:
                    QuestDisplay(Program.questDb[2],2,2);
                    break;
                default:
                    break;
            }
        }

        static void QuestDisplay(Quest quest,int num,int itemNum)// quest 퀘스트, num 몇번퀘스트(1~3),itemNum(보상을 줄 아이템 인덱스)
        {
            int input;
            GetQuestTxt(quest, num,itemNum);
            if (quest.QuestStart == false)
            {
                Console.WriteLine($"1. 수락\r\n2. 거절\r\n원하시는 행동을 입력해주세요.\r\n>>");
                input = Program.CheckInput(1, 2);
                if (input == 1) quest.QuestStart = true;
                QuestMain();
            }
            else if(quest.QuestStart == true)
            {
                if(quest.QuestClear == false)
                {
                    Console.WriteLine("2. 돌아가기\r\n\r\n원하시는 행동을 입력해주세요.\r\n>>");
                    input = Program.CheckInput(1, 2);
                    QuestMain();
                }
                else if(quest.Goal1>=5)//게임 목표 달성
                {
                    quest.QuestClear = true;
                    Console.WriteLine("1. 보상 받기\r\n2. 돌아가기\r\n\r\n원하시는 행동을 입력해주세요.\r\n>>");
                    input = Program.CheckInput(1, 2);
                    if(input==1)
                    {
                        QuestReward(quest, num, itemNum);//보상을 받는 로직 구현
                    }
                    QuestMain();
                }
            }            
        }      
        static void QuestReward(Quest quest,int num,int itemNum)
        {
            Console.Clear();
            quest.QuestStart=false;
            quest.QuestClear = false;
            quest.Goal1 = 0;
            quest.Goal2 = 0;
            Console.WriteLine("\r\n\r\n보상을 획득하였습니다!\r\n\r\n");
            Program.player.Gold += quest.Gold;
                Console.WriteLine($"획득한 골드 : {quest.Gold}G\r\n\r\n");
            Item targetItem = Program.itemDb[itemNum];
            if (Program.player.HasItem(targetItem))
            {
                Console.WriteLine($"이미 아이템이 존재하여 골드로 대체됩니다! : {targetItem.Price}G\r\n\r\n");
                Program.player.Gold += targetItem.Price;
            }
            else
            {
                Console.WriteLine($"아이템 획득 : {targetItem.Name}");
                Program.player.GetItem(targetItem);
            }
            Console.WriteLine("2. 돌아가기\r\n\r\n원하시는 행동을 입력해주세요.\r\n>>");
            int input = Program.CheckInput(1, 2);
        }
        static void GetQuestTxt(Quest quest, int indexnum,int itemNum)
        {
            Console.Clear();
            Item targetItem = Program.itemDb[itemNum];
            switch (indexnum)
            {
                case 0:// 몬스터 5마리 Goal1
                    if(quest.Goal1 >=5) quest.QuestClear = true;
                    Console.WriteLine("Quest!!\r\n\r\n마을을 위협하는 미니언 처치\r\n\r\n이봐! 마을 근처에 슬라임들이 너무 많아졌다고 생각하지 않나?\r\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n모험가인 자네가 좀 처치해주게!\r\n\r\n");
                    Console.WriteLine($"- 미니언 5마리 처치 ({quest.Goal1}/5)\r\n\r\n- 보상- \r\n\t{targetItem.Name} x 1\r\n\t{quest.Gold}G\r\n\r\n");
                    break;
                case 1:// 무기, 방어구 장착 Goal1, Goal2
                    if (quest.Goal1 >= 1 && quest.Goal2 >=1) quest.QuestClear = true;
                    Console.WriteLine("Quest!!\r\n\r\n장비를 장착해보자\r\n\r\n이봐! 좋은 모험가라면 좋은 장비를 써야하는 법...\r\n장비를 장착하면 던전을 조금 더 쉽게 공략하지!\r\n한번 장비를 껴봐!\r\n\r\n");
                    Console.WriteLine($"- 무기와 방어구 장착 ({quest.Goal1}/1)({quest.Goal2}/1)\r\n\r\n- 보상- \r\n\t{targetItem.Name} x 1\r\n\t{quest.Gold}\r\n\r\n");
                    break;
                case 2:// 몬스터 50마리 Goal1
                    if (quest.Goal1 >= 50) quest.QuestClear = true;
                    Console.WriteLine("Quest!!\r\n\r\n더욱 더 강해지기!\r\n\r\n이봐! 마을 근처에 슬라임들이 너무 많아졌다고 생각하지 않나?\r\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n모험가인 자네가 좀 처치해주게!\r\n\r\n");
                    Console.WriteLine($"- 미니언 50마리 처치 ({quest.Goal1}/50)\r\n\r\n- 보상- \r\n\t{targetItem.Name} x 1\r\n\t{quest.Gold}\r\n\r\n");
                    break;
                default:
                    break;

            }
        }
    }

}