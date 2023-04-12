namespace Console_Labyrinth
{
    internal struct Direction
    {
        public int dX;
        public int dY;

        public Direction(int dX, int dY)
        {
            this.dX = dX;
            this.dY = dY;
        }

        public static readonly Direction leftDirection = new Direction(0, -1);
        public static readonly Direction rightDirection = new Direction(0, 1);
        public static readonly Direction upDirection = new Direction(-1, 0);
        public static readonly Direction downDirection = new Direction(1, 0);   


    }

    
}