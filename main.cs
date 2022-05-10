using System;

namespace Console_Game_Maker
{
    class Level
    {
        public int[,] AddBoarder(int[,] pixelMap) 
        {
            int height = pixelMap.GetUpperBound(0) + 1;
            int width = pixelMap.GetUpperBound(1) + 1;

            for (int row = 0; row < height; row++)
            {
                pixelMap[row, width - 1] = 35;
                pixelMap[row, 0] = 35;
            }
            
            for (int col = 0; col < width; col++)
            {
                pixelMap[0, col] = 35;
                pixelMap[height-1, col] = 35;
            }

            return pixelMap;
        }

        public void ShowMap(int[,] pixelMap) 
        {
            int pixelCount = 0;
            int width = pixelMap.GetUpperBound(1) + 1;
            foreach (int pixel in pixelMap)
            {
                if (pixelCount == width)
                {
                    Console.WriteLine();
                    pixelCount = 0;
                }
                Console.Write(pixel);
                pixelCount++;

            }
        }

        public void ShowCharMap(int[,] pixelMap)
        {
            int pixelCount = 0;
            int width = pixelMap.GetUpperBound(1) + 1;
            foreach (int pixel in pixelMap)
            {
                if (pixelCount == width)
                {
                    Console.WriteLine();
                    pixelCount = 0;
                }

                if (pixel == 0) { Console.Write(" "); }
                else { Console.Write((char)pixel); }
                pixelCount++;
            }
        }

        public int[,] AddBox(int[,] pixelMap, int x, int width, int y, int height, int pixelData=22)
        {
            for (int j = 0; j < width; j++)
            {
                pixelMap[y, x + j] = pixelData;
                pixelMap[y+height, x + j] = pixelData;
            }

            for (int k = 0; k < height; k++)
            {
                pixelMap[y + k, x] = pixelData;
                pixelMap[y + k, x+width-1] = pixelData;
            }

            return pixelMap;
        }


        public int[,] InitLevel(int width, int height, Level level)
        {
            int[,] pixelMap = new int[height, width];

            pixelMap = level.AddBoarder(pixelMap);
            pixelMap = level.AddBox(pixelMap, 60, 10, 5, 29, 15);
            pixelMap = level.AddBox(pixelMap, 10, 5, 29, 5);
            pixelMap = level.AddBox(pixelMap, 1, 78, 35, 2);

            return pixelMap;
        }

        static void Main()
        {
            Level mainLevel = new Level();
            int [,] mainLevelData = mainLevel.InitLevel(80, 40, mainLevel);
            mainLevel.ShowCharMap(mainLevelData);
        
           
        }
    }
}
