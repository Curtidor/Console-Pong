using System;


namespace Console_Game_Engine
{
    class Inputs
    {
        private readonly ConsoleKey Right = ConsoleKey.D;
        private readonly ConsoleKey Left = ConsoleKey.A;
        public void CameraInput(ConsoleKeyInfo key, int camX, int camY, int viewSizeX, int viewSizeY, int worldWidth, int worldHeight, Camera camera)
        {
            int x = 0;
            if (key.Key == Right) { x = 1; }
            else if (key.Key == Left) { x = -1; }

            if (camX + x + viewSizeX < worldWidth && camX + x > -1)
            {
                if (key.Key == Right) { camera.X++; }
                else if (key.Key == Left) { camera.X--; }
            }
            //if (camY + viewSizeY < worldHeight && camY > -1) { camera.Y++; }


        }

        public static bool IsValidMovement(int x, int y, int worldWidth, int worldHeight, int objectHeight = 1 ,int objectWidth = 1)
        {
            if (x + objectWidth < worldWidth && y + objectHeight < worldHeight && x >= 0 && y >= 0) { return true; }
            return false;
        }
    }
}
