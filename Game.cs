using System;


namespace Console_Game_Engine
{
    class Game
    {
        int[] player = new int[] { 10, 1, 19, 2, 64 };

        bool running = true;
        Inputs inputHandle = new();
        World mainWorld = new World(22, 22);
        Camera mainCamera;

        public void Start()
        {
            Console.CursorVisible = false;
            mainCamera = new Camera(20, 20, mainWorld.WorldSize);
            for (int y = 0; y < mainWorld.Height; y++)
            {
                for (int x = 0; x < mainWorld.Width; x++)
                {
                    mainWorld.PixelMap[y, x] = 32; //ASCII code for a blank
                }
            }
            mainWorld.GenWorld();
            GameLoop();
        }

        private void GameLogic()
        {
            var key = Console.ReadKey(true);
            //inputHandle.CameraInput(key, mainCamera.X, mainCamera.Y, mainCamera.dimensions[0], mainCamera.dimensions[1], mainWorld.Width, mainWorld.Height, mainCamera);
            mainWorld.UpdateWorld();
            if (key.Key == ConsoleKey.Backspace) { running = false; }
        }


        private void GameLoop()
        {
            while (running)
            {
                if (Console.KeyAvailable)
                {
                    GameLogic();
                    mainCamera.Draw(mainWorld.PixelMap);
                  
                    Console.WriteLine();
                    Console.WriteLine(mainCamera.X + mainCamera.dimensions[0] + " " + mainCamera.Y);
                }
            }
        }
        
    }
}
