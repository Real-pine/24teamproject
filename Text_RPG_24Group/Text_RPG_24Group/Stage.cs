using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_24Group
{
    class Stage
    {
        int PlayerX = 1;
        int PlayerY = 1;

        // 플레이어 이동 메서드
        public static void PlayerMove(int playerX, int playerY, DungeonMap map)
        {
            while (true)
            {
                // 맵 출력
                Console.Clear();
                for (int y = 0; y < map.GetHeight(); y++) // GetHeight()로 높이를 가져옴
                {
                    for (int x = 0; x < map.GetWidth(); x++) // GetWidth()로 너비를 가져옴
                    {
                        if (x == playerX && y == playerY)
                        {
                            Console.Write("□"); // 플레이어 표시
                        }
                        else if (map.GetTile(y, x) == 0)
                        {
                            Console.Write("  "); // 빈 공간은 공백2개로 표시
                        }
                        else if (map.GetTile(y, x) == 1)
                        {
                            Console.Write("■"); // 벽 표시
                        }
                        else if (map.GetTile(y, x) == 2)
                        {
                            Console.Write("◎"); // 적 표시
                        }
                        else if (map.GetTile(y, x) == 3)
                        {
                            Console.Write("♨"); // 마을 표시
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("■ : 벽\t□ : 플레이어\t◎ : 적\t");
                // 플레이어 이동 처리
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                int newPlayerX = playerX;
                int newPlayerY = playerY;

                switch (keyInfo.Key)
                {
                    case ConsoleKey.W: // 위로 이동
                        newPlayerY--;
                        break;
                    case ConsoleKey.S: // 아래로 이동
                        newPlayerY++;
                        break;
                    case ConsoleKey.A: // 왼쪽으로 이동
                        newPlayerX--;
                        break;
                    case ConsoleKey.D: // 오른쪽으로 이동
                        newPlayerX++;
                        break;
                }

                
                if (map.GetTile(newPlayerY, newPlayerX) == 0)//빈공간 이동
                {
                    playerX = newPlayerX;
                    playerY = newPlayerY;
                }
                else if (map.GetTile(newPlayerY, newPlayerX) == 2) //적과 컨택 > 전투시작
                {
                    map.SetTile(newPlayerY, newPlayerX, 0);
                    playerX = newPlayerX;
                    playerY = newPlayerY;
                    //전투 씬
                }
                else if (map.GetTile(newPlayerY, newPlayerX) == 3)//마을로 이동
                {
                    Console.Clear();
                    break;
                }

            }
        }
    }

    public class DungeonMap
    {
        public int[,] MapData { get; set; }

        public DungeonMap(int[,] mapData)
        {
            this.MapData = mapData;
        }

        public int GetTile(int y, int x)
        {
            return MapData[y, x];
        }

        public int GetWidth()
        {
            return MapData.GetLength(1);
        }

        public int GetHeight()
        {
            return MapData.GetLength(0);
        }
        public void SetTile(int posX, int posY, int name)
        {
            this.MapData[posX, posY] = name;
        }
    }
}