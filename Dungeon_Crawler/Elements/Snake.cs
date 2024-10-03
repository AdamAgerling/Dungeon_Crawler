
class Snake : Enemy
{
    public Snake(Position position)
    {
        Position = position;
        ColorPicker = ConsoleColor.Green;
        Health = 20;
        MapElement = 's';
        Name = "Snakeboy";
        AttackDice = new Dice(3, 6, 3);
        DefenceDice = new Dice(1, 8, 5);
    }

    public override void UpdateEnemies(List<LevelElement> levelElements, Player player)
    {
        var random = new Random();

        foreach (var element in levelElements)
        {
            if (element is Snake snake && snake.Health > 0)
            {
                var playerDistanceX = Math.Abs(player.Position.X - snake.Position.X);
                var playerDistanceY = Math.Abs(player.Position.Y - snake.Position.Y);

                if (playerDistanceX >= 2 || playerDistanceY >= 2)
                {
                    continue;
                }

                Position oldSnakePosition = snake.Position;
                Console.SetCursorPosition(oldSnakePosition.X, oldSnakePosition.Y + 4);
                Console.Write(' ');

                Position updatedSnakePosition = oldSnakePosition;

                if (playerDistanceX >= playerDistanceY)
                {
                    if (player.Position.X < snake.Position.X)
                    {
                        updatedSnakePosition.X += 1;
                    }
                    else
                    {
                        updatedSnakePosition.X -= 1;
                    }
                }
                else
                {
                    if (player.Position.Y < snake.Position.Y)
                    {
                        updatedSnakePosition.Y += 1;
                    }
                    else
                    {
                        updatedSnakePosition.Y -= 1;
                    }
                }
                var collisionElement = levelElements
              .FirstOrDefault(e => e.Position.X == updatedSnakePosition.X && e.Position.Y == updatedSnakePosition.Y);

                if (collisionElement == null || collisionElement is Player)
                {
                    if (updatedSnakePosition.X == player.Position.X && updatedSnakePosition.Y == player.Position.Y)
                    {
                        return;
                    }
                    snake.Position = updatedSnakePosition;
                }
            }
        }
    }
}