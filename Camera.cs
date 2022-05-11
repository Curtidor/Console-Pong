using System;
using System.Windows.Input;


namespace Console_Game_Engine
{
    class Camera
    {
        public int[] dimensions = new int[] { 0, 0 };
        public int X { get; set; }
        public int Y { get; set; }

        public Camera(int width, int height, int worldSize)
        {
           
            if (width*height > worldSize) { throw new ArgumentException("!!Camera view size is bigger than the world!!"); }
            if (width <= 0 || height <= 0) { throw new ArgumentException("!!Camera view size is to small!!"); }
            dimensions[0] = width;
            dimensions[1] = height;
        }

        public void Draw(int[,] pixelMap, bool hasBoarder = true, int boarderValue = 35)
        {
            Console.SetCursorPosition(0, 0);
            if (hasBoarder) { Console.WriteLine("{0}".PadRight(dimensions[1], (char)boarderValue), (char)boarderValue); }
            for (int y = 0; y < dimensions[0]; y++)
            {
                for (int x = 0; x < dimensions[1]; x++)
                {
                    if (hasBoarder && x == 0)
                    {
                        Console.SetCursorPosition(0, y);
                        Console.Write("{0}",(char)boarderValue);
                        Console.SetCursorPosition(dimensions[1]-2, y);
                        Console.Write("{0}", (char)boarderValue);
                    }
                    Console.SetCursorPosition(x+1, y+1);
                    Console.Write((char)pixelMap[y + Y, X + x]);
                }
            }
            Console.Write("{0}".PadRight(dimensions[1]), (char)boarderValue);
            Console.SetCursorPosition(0,dimensions[0]);
            if (hasBoarder) { Console.WriteLine("{0}".PadRight(dimensions[1], (char)boarderValue), (char)boarderValue); }

        }
    }
}
