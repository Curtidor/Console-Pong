using System;

namespace Pong
{
    class Paddle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int height = 2;

        public Paddle(int setX, int setY)
        {
            X = setX;
            Y = setY;
        }
    }

    class Game
    {
        private readonly int width = 101;
        private readonly int height = 31;
        private int[,] staticPixelMap;
        private int[,] pixelMapCopy;

        private Paddle paddle1;
        private Paddle paddle2;
        public void Start()
        {
            paddle1 = new Paddle(2, height / 2);
            paddle2 = new Paddle(width-3, height / 2);
            staticPixelMap = new int[height, width];
            for (int y = 0; y < height; y++)
            {
                if (y % 2 == 0) { staticPixelMap[y, width / 2] = 124; }
                if (y == height/2) { staticPixelMap[y, width / 2] = 42; }
                for (int x = 0; x < width; x++)
                {
                    if (x == 0) 
                    { 
                        staticPixelMap[y, x] = 35; 
                        staticPixelMap[y, width-1] = 35;
                    }
                    if (y == 0 || y == height-1) { staticPixelMap[y, x] = 35;} // 35 is used to add a boarder to the pixelmap
                    if (staticPixelMap[y, x] == 0) { staticPixelMap[y, x] = 32; }
                }
            }
            DrawFrame();
            GameLoop();
        }

        private void DrawFrame()
        {
            Console.CursorVisible = false;
            pixelMapCopy = staticPixelMap.Clone() as int[,];
            for (int y = 0; y < height; y++)
            {
                if (InRange(paddle1.Y, paddle1.height + paddle1.Y, y)) { pixelMapCopy[y, paddle1.X] = 124; }
                if (InRange(paddle2.Y, paddle2.height + paddle2.Y, y)) { pixelMapCopy[y, paddle2.X] = 124; }
                for (int x = 0; x < width; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write((char)pixelMapCopy[y, x]);
                }
            }
        }


        private void GameLogic(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.W && paddle1.Y + paddle1.height > paddle1.height+1) { paddle1.Y--; }
            if (key.Key == ConsoleKey.S && paddle1.Y + paddle1.height < height - 2) { paddle1.Y++; }
            if (key.Key == ConsoleKey.UpArrow && paddle2.Y + paddle2.height > paddle2.height+1) { paddle2.Y--; }
            if (key.Key == ConsoleKey.DownArrow && paddle2.Y + paddle2.height < height - 2 ) { paddle2.Y++; }
        }

        public static bool InRange(int low, int high, int num)
        {
            if(low <= num && num <= high) { return true; }
            return false;
        }

        private void GameLoop()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    GameLogic(key);
                    DrawFrame();
                    if (key.Key == ConsoleKey.Backspace) { break; }
                }
            }
        }
    }
}
