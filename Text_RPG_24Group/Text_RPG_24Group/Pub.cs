using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_24Group
{
    class Pub
    {
        public void PubMainUI()
        {
        Console.Clear();
            Console.WriteLine("\n\r선술집에 오신 것을 환영합니다!(메인홀)\n\r");
            Console.WriteLine("원하시는 기능을 선택해주세요>");
            Console.WriteLine("1. 숙박");
            Console.WriteLine("2. 도박");
            Console.WriteLine("3. 술마시기");
            Console.WriteLine("4. 개인정비");
            Console.WriteLine("0. 뒤로가기");

            int input = Program.CheckInput(0, 4);
            switch (input)
            {
                case 0:
                    break;
                case 1:
                    PubGoToBed();
                    break;
                case 2:
                    Gambling();
                    break;
                case 3:
                    BlackJack();
                    break;
                case 4:
                    DrinkAlcohol();
                    break;
                default:
                    break;
            }          
        }
        void PubGoToBed()
        {
            Console.Clear();
            Console.WriteLine("\n\r숙박시설에 오신 것을 환영합니다!\n\r");
            Console.WriteLine($"\n현재 채력 : {Program.player.Hp}, 소지금 : {Program.player.Gold}\n");
            Console.WriteLine("원하시는 기능을 선택해주세요>");
            Console.WriteLine("1. 노숙    (20골드 HP 10회복)");
            Console.WriteLine("2. 일반 방 (50골드 HP 30회복)");
            Console.WriteLine("3. 고급 방 (60골드 HP 50회복)");
            Console.WriteLine("0. 뒤로가기");
            int input = Program.CheckInput(0, 3);
            if (input == 0) PubMainUI();
            else PubGoToBed(input);
            
        }
        void PubGoToBed(int num)
        {
                    Console.Clear();
            switch (num)
            {
                case 1:
                    if(Program.player.Gold < 20)
                    {
                        Console.WriteLine("소지금이 부족합니다!");
                    }
                    else
                    {
                        Console.WriteLine("20골드로 체력을 10 회복하였다. ");
                        Program.player.Gold -= 20;
                        Program.player.Hp += 10;
                        if (Program.player.Hp >Program.player.MaxHp) Program.player.Hp = Program.player.MaxHp;
                        Program.SoundEffectManager.PlaySoundEffect(Program.SoundEffectManager.SoundClear);
                    }
                    break;
                case 2:
                    if (Program.player.Gold < 50)
                    {
                        Console.WriteLine("소지금이 부족합니다!");
                    }
                    else
                    {
                        Console.WriteLine("50골드로 체력을 30 회복하였다. ");
                        Program.player.Gold -= 50;
                        Program.player.Hp += 30;
                        if (Program.player.Hp > Program.player.MaxHp) Program.player.Hp = Program.player.MaxHp;
                        Program.SoundEffectManager.PlaySoundEffect(Program.SoundEffectManager.SoundClear);

                    }
                    break;
                case 3:
                    if (Program.player.Gold < 60)
                    {
                        Console.WriteLine("소지금이 부족합니다!");
                    }
                    else
                    {
                        Console.WriteLine("60골드로 체력을 50 회복하였다. ");
                        Program.player.Gold -= 60;
                        Program.player.Hp += 50;
                        if (Program.player.Hp > Program.player.MaxHp) Program.player.Hp = Program.player.MaxHp;
                        Program.SoundEffectManager.PlaySoundEffect(Program.SoundEffectManager.SoundClear);
                    }
                    break;
                default:
                    break;
            }
            Console.WriteLine("0. 뒤로가기");
            int input = Program.CheckInput(0, 0);
            PubGoToBed();
        }        
        void Gambling()
        {
            Console.Clear();
            Console.WriteLine("\n\r도박장에 오신 것을 환영합니다!\n\r");
            Console.WriteLine($"\n소지금 : {Program.player.Gold}\n");
            Console.WriteLine("원하시는 기능을 선택해주세요(딜러비용 20G)>");
            Console.WriteLine("1. 코인플립");
            Console.WriteLine("2. 경마");
            Console.WriteLine("3. 미니블랙잭");
            Console.WriteLine("0. 뒤로가기");
            int input = Program.CheckInput(0, 3);
            if (Program.player.Gold <= 20 && input != 0)
            { 
            input = 0;
                Console.Clear();
                Console.WriteLine("골드가 없어서 돌아갑니다...!");
                Program.SoundEffectManager.PlaySoundEffect(Program.SoundEffectManager.SoundHurt);
                input = Program.CheckInput(0, 0);
                Console.WriteLine("0. 뒤로가기");
            } 
            else
            {
                Program.player.Gold -= 20;
            }
            switch (input)
            {
                case 0:
                    break;
                case 1:
                    CoinFlip();
                    break;
                case 2:
                    HorseRide();
                    break;
                case 3:
                    BlackJack();
                    break;
                default:
                    break;
            }
            PubMainUI();
        }
        
        void CoinFlip()
        {
            Console.Clear();
            Console.WriteLine("\n\r코인플립에 오신 것을 환영합니다!\n\r");
            Console.WriteLine("유저가 앞과 뒤를 선택하여 내기를 하는 게임입니다!");
            Console.WriteLine($"\n소지금 : {Program.player.Gold}\n");
            Console.WriteLine("판돈을 걸어주세요(최대 1000g)(소지금액 초과시 올인) : ");
            int bet = Program.CheckInput(1, 1000);
            if(Program.player.Gold <bet) bet = Program.player.Gold;
            Program.player.Gold -= bet;
            Console.WriteLine("숫자를 입력하세요\n1. 앞\n2.뒤");
            int input = Program.CheckInput(1,2);
            Random random = new Random();
            int computer = random.Next(1, 3);
            Console.WriteLine($"나 : {input}, 딜러 : {computer}");
            if (input == computer)
            {
                Program.player.Gold += bet;
                Console.WriteLine($"축하합니다. 승리하셨습니다!\n{bet}G 를 얻습니다, 현재골드 : {Program.player.Gold}G");
                Program.SoundEffectManager.PlaySoundEffect(Program.SoundEffectManager.SoundClear);
            }
            else
            {
                Console.WriteLine($"다음 기회에... 패배하셨습니다!\n{bet}G 를 잃습니다, 현재골드 : {Program.player.Gold}G");
                Program.SoundEffectManager.PlaySoundEffect(Program.SoundEffectManager.SoundDie);

            }
            Console.WriteLine("0. 돌아가기");
            input = Program.CheckInput(0, 0);
            Gambling();
        }
        void HorseRide()
        {
            Console.Clear();
            Random random = new Random();
            Console.WriteLine("\n\r경마게임에 오신 것을 환영합니다!\n\r");
            Console.WriteLine("\n\r유저가 1부터 6중 하나의 말에 돈을 걸어서 1등부터 3등까지가 보상을 얻습니다.\n\r");
            Console.WriteLine("1등 3배\t2등 2배\t 3등 본전");
            Console.WriteLine($"\n소지금 : {Program.player.Gold}\n");
            Console.WriteLine("판돈을 걸어주세요(최대 1000g)(소지금액 초과시 올인) : ");
            int bet = Program.CheckInput(1, 1000);
            if (Program.player.Gold < bet) bet = Program.player.Gold;
            Program.player.Gold -= bet;
            Console.WriteLine("1. ★\n2. ●\n3. ◆\n4. ♠\n5. ♥\n6. ♣\n");
            Console.WriteLine("몇번 말에 거시겠습니까?\n1~6입력 > ");
            int input = Program.CheckInput(1, 7);
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };// 1부터 6까지의 숫자를 리스트에 저장
            int[] arr = new int[3];
            for (int i = 0; i < 3; i++)
            {
                int index = random.Next(numbers.Count); // 리스트에서 랜덤한 인덱스 선택
                arr[i] = numbers[index];      // 선택된 숫자를 저장
                numbers.RemoveAt(index);      // 선택된 숫자는 리스트에서 제거 (중복 방지)
            }
            Console.WriteLine($"1등 : {arr[0]}번\n2등 : {arr[1]}번\n3등 : {arr[2]}번\n\n유저 선택 : {input}번");
            if (input == arr[0])
            {
                Program.player.Gold += bet * 3;
                Console.WriteLine($"1등에 당첨되었습니다! 상금은{bet*3}입니다! 현재 잔액 : {Program.player.Gold}");
                Program.SoundEffectManager.PlaySoundEffect(Program.SoundEffectManager.SoundClear);
            }
            else if (input == arr[1])
            {
                Program.player.Gold += bet * 2;
                Console.WriteLine($"2등에 당첨되었습니다! 상금은{bet * 2}입니다! 현재 잔액 : {Program.player.Gold}");
                Program.SoundEffectManager.PlaySoundEffect(Program.SoundEffectManager.SoundClear);
            }
            else if (input == arr[2])
            {
                Program.player.Gold += bet;
                Console.WriteLine($"3등에 당첨되었습니다! 상금은{bet}입니다! 현재 잔액 : {Program.player.Gold}");
                Program.SoundEffectManager.PlaySoundEffect(Program.SoundEffectManager.SoundDie);
            }
            else Console.WriteLine("아쉽지만 다음기회에!");
            Console.WriteLine("0. 돌아가기");
            input = Program.CheckInput(0, 0);
            Gambling();
        }
        void BlackJack()
        {
            Console.Clear();
            Console.WriteLine("\n\r블랙잭에 오신 것을 환영합니다!\n\r");
            Console.WriteLine("카드의 합이 21에 가깝게 만들되, 초과하지 않도록 하는 것이 목표입니다.\n21을 넘기면 즉시 패배합니다.(배당2배)");
            Console.WriteLine("사용할 카드 : 1~13까지의 카드가 한장씩 있음");
            Console.WriteLine($"\n소지금 : {Program.player.Gold}\n");
            Console.WriteLine("판돈을 걸어주세요(최대 1000g)(소지금액 초과시 올인) : ");
            int bet = Program.CheckInput(1, 1000);
            if (Program.player.Gold < bet) bet = Program.player.Gold;
            Program.player.Gold -= bet;
            Random random = new Random();
            int[] user = { 0, 0 };
            int[] computer = { 0, 0 };
            user[0] = random.Next(1,14);
            computer[0] = random.Next(1,14);
            Console.Clear();
            Console.WriteLine($"유저의 수{user[0]}\n상대방의 수{computer[0]}");
            Console.WriteLine($"카드를 더 드시겠습니까?\n(카드는 1부터 13까지)(21초과시 패배)\n1.예\n2.아니오");
            int input = Program.CheckInput(1, 2);
            if (input == 1)
            {
                while (user[1] != user[0])
                {
                user[1] = random.Next(1, 14);
                }
            }
            computer[1] = random.Next(1, 3);
            if(computer[1] ==1)
            {
                while (computer[1] != computer[0])
                {
                    computer[1] = random.Next(1, 14);
                }
            }
            Console.WriteLine($"유저수의 합 : {user[0]+ user[1]}\n상대방수의 합{computer[0]+ computer[1]}");
            if (user[0] + user[1] > 21 && computer[0] + computer[1] > 21)
            { 
                Program.player.Gold += bet;
                Console.WriteLine("서로 21을 초과하여 무승부 입니다. 배팅 골드를 돌려드립니다.");
            }
            else if(user[0] + user[1] > 21)
            {
                Console.WriteLine("패배 입니다.");
            }
            else if(computer[0] + computer[1] > 21)
            {
                Console.WriteLine($"승리 입니다. {bet}만큼 돈을 드립니다");
                Program.player.Gold += bet*2;
                Program.SoundEffectManager.PlaySoundEffect(Program.SoundEffectManager.SoundClear);
            }
            else
            {
                if(user[0] + user[1] > computer[0] + computer[1])
                {
                    Console.WriteLine($"승리 입니다. {bet}만큼 돈을 드립니다");
                    Program.player.Gold += bet * 2;
                    Program.SoundEffectManager.PlaySoundEffect(Program.SoundEffectManager.SoundClear);
                }
                else if(user[0] + user[1] < computer[0] + computer[1])
                {
                    Console.WriteLine("패배 입니다.");
                }
                else
                {
                    Program.player.Gold += bet;
                    Console.WriteLine($"서로 합이{user[0] + user[1]}로 같아 무승부 입니다. 배팅 골드를 돌려드립니다.");
                }
            }
                Console.WriteLine($"\n소지금 : {Program.player.Gold}\n");
            Console.WriteLine("0. 돌아가기");
            input = Program.CheckInput(0, 0);
            Gambling();
        }
        void DrinkAlcohol()
        {
            Console.Clear();
            Console.WriteLine("\n\r어떤 술을 드시겠습니까?\n\r");
            Console.WriteLine($"\n소지금 : {Program.player.Gold}\n");
            Console.WriteLine("원하시는 술을 선택해주세요>");
            Console.WriteLine("1. 맥주(50g)(50%확률로 마나)");
            Console.WriteLine("2. 과일주(100g)");
            Console.WriteLine("3. 와인(200g)");
            Console.WriteLine("4. 뒤로가기");
            int input = Program.CheckInput(0, 3);
            PubMainUI();

        }
    }
}
