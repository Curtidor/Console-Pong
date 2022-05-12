using System;

namespace Pong
{
    class Program
    {
        private int[] Player1POS = new int[] { 0, 0 };
        private int[] Player2POS = new int[] { 0, 0 };
        private const int width = 80;
        private const int height = 30;
        private int[,] board = new int[height, width];
        static void Main(string[] args)
        {
            Program master = new();
            master.Start();
            
        }

 
        private void Start()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    board[y, x] = 32; // code for blank space
                }
            }
            Player1POS[0] = 1;
            Player1POS[1] = height / 2;
            Player2POS[0] = width-2;
            Player2POS[1] = height / 2;
            board[Player2POS[1], Player2POS[0]] = 124;
            board[Player1POS[1], Player1POS[0]] = 124;


            DrawFrame();
            GameLoop();
        }

        private void DrawFrame()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("{0}".PadRight(width+4,(char)35), (char)35);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == 0)
                    {
                        Console.SetCursorPosition(0, y);
                        Console.Write("{0}", (char)35);
                        Console.SetCursorPosition(width+1, y);
                        Console.Write("{0}", (char)35);
                    }
                    Console.SetCursorPosition(x+1, y+1);
                    Console.Write((char)board[y, x]);                 
                }
            }
            Console.SetCursorPosition(0, height);
            Console.Write("{0}".PadRight(width+4, (char)35), (char)35);
        }

    

        private void GameLogic(ConsoleKeyInfo key)
        {
           if (key.Key == ConsoleKey.W && Player1POS[1] < height) { Player1POS[1]++; }
           if (key.Key == ConsoleKey.S && Player1POS[1] >= 0) { Player1POS[1]--; }
           if (key.Key == ConsoleKey.UpArrow && Player2POS[1] < height) { Player2POS[1]++; }
           if (key.Key == ConsoleKey.DownArrow && Player2POS[1] >= 0) { Player2POS[1]--; }
           board[Player2POS[1], Player2POS[0]] = 124;
           board[Player1POS[1], Player1POS[0]] = 124;
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
