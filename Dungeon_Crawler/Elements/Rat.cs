class Rat : Enemy
{

    public Rat(Position position)
    {
        Position = position;
        ColorPicker = ConsoleColor.Red;
        Health = 15;
        MapElement = 'r';
        Name = "Ratman";
        AttackDice = new Dice(1, 6, 3);
        DefenceDice = new Dice(1, 6, 1);
    }

    public override void UpdateEnemies(List<LevelElement> levelElements, Player player)
    {
        var random = new Random();

        foreach (var element in levelElements)
        {
            if (element is Rat rat)
            {
                var ratMoveDirection = random.Next(1, 5);


                Position? oldRatPosition = rat.Position;
                Console.SetCursorPosition(oldRatPosition.Value.X, oldRatPosition.Value.Y + 4);
                Console.Write(' ');

                Position updatedRatPosition = oldRatPosition.Value;
                switch (ratMoveDirection)
                {
                    case 1:
                        updatedRatPosition.Y += 1;
                        break;
                    case 2:
                        updatedRatPosition.Y -= 1;
                        break;
                    case 3:
                        updatedRatPosition.X += 1;
                        break;
                    case 4:
                        updatedRatPosition.X -= 1;
                        break;
                }
                var collisionElement = levelElements
               .FirstOrDefault(e => e.Position.X == updatedRatPosition.X && e.Position.Y == updatedRatPosition.Y);

                if (collisionElement == null || collisionElement is Player)
                {
                    if (updatedRatPosition.X == player.Position.X && updatedRatPosition.Y == player.Position.Y)
                    {
                        return;
                    }
                    rat.Position = updatedRatPosition;
                }
            }
        }
    }

}
