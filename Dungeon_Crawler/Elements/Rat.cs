
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
                Position newRatPosition = rat.Position;
                var ratMoveDirection = random.Next(1, 5);

                //THE RATS ARE MULTIPLYING

                switch (ratMoveDirection)
                {
                    case 1:
                        newRatPosition.Y += 1;
                        break;
                    case 2:
                        newRatPosition.Y -= 1;
                        break;
                    case 3:
                        newRatPosition.X += 1;
                        break;
                    case 4:
                        newRatPosition.X -= 1;
                        break;
                }
                var collisionElement = levelElements
               .FirstOrDefault(e => e.Position.X == newRatPosition.X && e.Position.Y == newRatPosition.Y);

                if (collisionElement == null || collisionElement is Player || collisionElement is Rat)
                {
                    Console.SetCursorPosition(newRatPosition.X, newRatPosition.Y);
                    Console.Write(' ');
                    rat.Position = newRatPosition;
                }
            }
        }
    }

}
