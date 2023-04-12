using System.Numerics;

namespace Console_Labyrinth
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameState state = GameState.IsPlaying;
            IInput currentInput = new KeyboardInput();
            Direction lastDirection;

            var player = new Player(0, 0);
            var currentField = GenerateField(10, 20, 18);
            PlacePlayer(currentField, player);

            DrawField(currentField, state);

            while (state == GameState.IsPlaying)
            {
                lastDirection = currentInput.GetDirection();                
                state = HandleLogic(currentField, player, lastDirection);
                DrawField(currentField, state);
            }

            switch (state)
            {
                case GameState.IsWin:
                    Console.WriteLine("U won!");
                    break;
                case GameState.GameOver:
                    Console.WriteLine("U lose :(");
                    break;
            }
        }

        private static GameState HandleLogic(char[,] field, Player player, Direction direction)
        {
            int newXPos = player.XPos + direction.dX;
            int newYPos = player.YPos + direction.dY;

            if (newXPos >= 0 && newYPos >= 0 && newXPos < field.GetLength(0) && newYPos < field.GetLength(1))
            {
                if(field[newXPos, newYPos] == (char)Entities.Wall)
                {
                    return GameState.IsPlaying;
                }
                else if (field[newXPos, newYPos] == (char)Entities.Finish)
                {
                    MovePlayer(field, player, newXPos, newYPos);
                    return GameState.IsWin;
                }
                else
                {
                    MovePlayer(field, player, newXPos, newYPos);
                    return GameState.IsPlaying;
                }                
            }

            return GameState.IsPlaying;
        }

        private static void MovePlayer(char[,] field, Player player, int newXPos, int newYPos)
        {
            field[player.XPos, player.YPos] = (char)Entities.Empty;
            player.Move(newXPos, newYPos);
            field[newXPos, newYPos] = (char)Entities.Player;
        }


        static char[,] GenerateField(int width, int height, int blockFreq)
        {
            var rand = new Random();

            char[,] field = new char[width, height];

            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    int randomNumber = rand.Next(0, 101);
                    if (randomNumber < blockFreq)
                        field[x, y] = (char)Entities.Wall;
                    else
                        field[x, y] = (char)Entities.Empty;
                }
            }

            int xF = rand.Next(0, field.GetLength(0));
            int yF = rand.Next(0, field.GetLength(1));

            field[xF, yF] = (char)Entities.Finish;

            return field;
        }

        static void PlacePlayer(char[,] field, Player player)
        {
            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    if (field[x, y] == ' ')
                    {
                        field[x, y] = (char)Entities.Player;
                        player.Move(x, y);
                        return;
                    }
                }
            }
            player.Move(0, 0);
        }

        static void DrawField(char[,] field, GameState gameState)
        {
            if (gameState == GameState.IsPlaying)
            {
                Console.Clear();
                for (int x = 0; x < field.GetLength(0); x++)
                {
                    for (int y = 0; y < field.GetLength(1); y++)
                    {
                        Console.Write(field[x, y]);
                    }
                    Console.Write("\n");
                }
            }
        }
    }

    public class Player
    {
        public int XPos => _xPos;
        public int YPos => _yPos;

        private int _xPos;
        private int _yPos;

        public Player(int xPos, int yPos)
        {
            _xPos = xPos;
            _yPos = yPos;
        }

        public void Move(int newX, int newY)
        {
            _xPos = newX;
            _yPos = newY;
        }
    }
}