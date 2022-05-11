using System;


namespace Console_Game_Engine
{
    class World
    {
        public int[,] PixelMap { get; set; }
        public int Height { get; }
        public int Width { get; } 
        public int WorldSize { get; }


        public World(int width, int height)
        {
            PixelMap = new int[height, width];
            Height = PixelMap.GetLength(0);
            Width = PixelMap.GetLength(1);
            WorldSize = Height * Width;
        }

        public void DrawBox(int x, int width, int y, int height, int pixelData = 22)
        {
            for (int length = 0; length < width; length++)
            {
                PixelMap[y, x + length] = pixelData;
                PixelMap[y+height, x + length] = pixelData;
            }

            for (int row = 0; row < height; row++)
            {
                PixelMap[row+y, x] = pixelData;
                PixelMap[row+y, x+width-1] = pixelData;
            }
        }

        public void GenWorld()
        {
            DrawBox(10, 4, 3, 3);
            DrawBox(10, 1, 17, 2, 64);
        }

        public void UpdateWorld()
        {

        }
    }
}
