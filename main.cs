using System;
using System.Threading;
using System.Diagnostics;

namespace Pong
{
    class Paddle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int OldY { get; set; }
        public int height = 3;

        public Paddle(int setX, int setY)
        {
            X = setX;
            Y = setY;
        }
    }

    class Game
    {
        private readonly int width = 100;
        private readonly int height = 31;
        private bool running = true;
        private int ballX, ballY, player1Score, player2Score, boardOffset = 2;
        private int[] ballMotion;

        private Paddle paddle1, paddle2;
        public void Start()
        {
            ballX = width / 2;
            ballY = height / 2;
            ballMotion = new int[] { -1, 1 };
            paddle1 = new Paddle(5, height / 2);
            paddle2 = new Paddle(width-3, (height+boardOffset) / 2);
            DrawFrame();
            Thread thread1 = new Thread(GameLoop);
            Thread thread2 = new Thread(Ball);
            thread1.Start();
            thread2.Start();
        }

        private void ShowScore()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("Player 1 {0}", player1Score);
            Console.SetCursorPosition(width - 7, 0);
            Console.Write("Player 2 {0}", player2Score);
        }

        private void DrawCenter()
        {
            for (int y = 0; y < height; y++)
            {
                if (y % 2 == 0)
                {
                    Console.SetCursorPosition(width / 2, y + boardOffset);
                    Console.Write("|");
                }
            }
        }

        private void CheckScore(int x)
        {
            if (x == 1)
            {
                player2Score++;
                ballX = width / 2;
                ballY = height / 2;
                ballMotion[0] = 1;
            }

            if (x == width + 1)
            {
                player1Score++; 
                ballX = width / 2; 
                ballY = height / 2;
                ballMotion[0] = -1;
            }
        }

        private void Ball()
        {
            while (running)
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(ballX, ballY);
                Console.Write((char)32);
                ballX += ballMotion[0];
                ballY += ballMotion[1];
                if (ballY - boardOffset == 1) { ballMotion[1] = 1; }
                if (ballY == height + 1) { ballMotion[1] = -1; }
                if (ballX == paddle1.X + 1 && InRange(paddle1.Y, paddle1.Y + paddle1.height, ballY - 1)) { ballMotion[0] = 1; } // bounce off paddle x
                if (ballX == paddle2.X - 1 && InRange(paddle2.Y, paddle2.Y + paddle2.height, ballY - 1)) { ballMotion[0] = -1; } // bounce off paddle x
                CheckScore(ballX);
                Console.SetCursorPosition(ballX, ballY);
                Console.Write("*");
                Thread.Sleep(70);
            }
            
        }
        
        private void DrawFrame()
        {
            
            Console.CursorVisible = false;
            ShowScore();
            DrawCenter();
            for (int y = 0; y < height; y++)
            {
                Console.SetCursorPosition(0, y + boardOffset);
                Console.Write('#');
                Console.SetCursorPosition(width + 2, y + boardOffset);
                Console.Write('#');
            }
            Console.SetCursorPosition(0, boardOffset);
            Console.Write("#".PadRight(width + 2, '#'));
            Console.SetCursorPosition(0, height + boardOffset);
            Console.Write("#".PadRight(width + 3, '#'));
            Console.SetCursorPosition(ballX, ballY + boardOffset);
            Console.Write("*");
            UpdateFrame();
        }


        private void UpdateFrame()
        {
            Console.CursorVisible = false;
            ShowScore();
            if (reDrawCenter)
            {
                DrawCenter();
            }
                
            if (paddle1.OldY >= 0) 
            { 
                Console.SetCursorPosition(paddle1.X, paddle1.OldY + boardOffset); 
                Console.Write((char)32);
                Console.SetCursorPosition(paddle1.X, paddle1.OldY + paddle1.height-1 + boardOffset);
                Console.Write((char)32);
            }
            if (paddle2.OldY >= 0)
            {
                Console.SetCursorPosition(paddle2.X, paddle2.OldY + boardOffset);
                Console.Write((char)32);
                Console.SetCursorPosition(paddle2.X, paddle2.OldY + paddle2.height - 1 + boardOffset);
                Console.Write((char)32);
            }
            for (int y = 0; y < paddle1.height; y++)
            {
                Console.SetCursorPosition(paddle1.X, (y + paddle1.Y) + boardOffset) ;
                Console.Write((char)124);
                Console.SetCursorPosition(paddle2.X, (y + paddle2.Y) + boardOffset);
                Console.Write((char)124);
            }
        }

        private void GameLogic(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.W && paddle1.Y + paddle1.height > paddle1.height+1) {paddle1.OldY = paddle1.Y; paddle1.Y--; }
            if (key.Key == ConsoleKey.S && paddle1.Y + paddle1.height < height) { paddle1.OldY = paddle1.Y; paddle1.Y++; }
            if (key.Key == ConsoleKey.UpArrow && paddle2.Y + paddle2.height > paddle2.height+1) { paddle2.OldY = paddle2.Y; paddle2.Y--; }
            if (key.Key == ConsoleKey.DownArrow && paddle2.Y + paddle2.height < height) { paddle2.OldY = paddle2.Y; paddle2.Y++; }
        }

        public static bool InRange(int low, int high, int num)
        {
            if(low <= num && num <= high) { return true; }
            return false;
        }

        private void GameLoop()
        {
            Stopwatch sw = new Stopwatch();
            while (running)
            {
                var key = Console.ReadKey(true);
                sw.Start();
                GameLogic(key);
                UpdateFrame();
                if (key.Key == ConsoleKey.Backspace) { running = false; }
                sw.Stop();
                Console.SetCursorPosition(0, height + 5);
                Console.WriteLine("FPS: " + (1/((float)sw.ElapsedMilliseconds/1000)));
                Console.WriteLine(ballX + " " + ballY);
                Console.WriteLine("Paddle 1 (y): {0}\nPaddle 1 (y+height): {1}",paddle1.Y,paddle1.height+paddle1.Y);
                Console.WriteLine("Paddle 2 (y): {0}\nPaddle 2 (y+height): {1}",paddle2.Y,paddle1.height+paddle2.Y);
                sw.Reset();
            }
        }
    }
}
