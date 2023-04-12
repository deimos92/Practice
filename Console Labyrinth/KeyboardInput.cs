namespace Console_Labyrinth
{
    internal class KeyboardInput : IInput
    {

        public Direction GetDirection()
        {
            if (Console.ReadKey(true).Key == ConsoleKey.W)
                return Direction.upDirection;
            if (Console.ReadKey(true).Key == ConsoleKey.S)
                return Direction.downDirection;
            if (Console.ReadKey(true).Key == ConsoleKey.A)
                return Direction.leftDirection;
            if (Console.ReadKey(true).Key == ConsoleKey.D)
                return Direction.rightDirection;

            return new Direction(0, 0);
        }       
    }
}
