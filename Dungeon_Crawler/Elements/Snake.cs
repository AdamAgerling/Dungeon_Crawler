﻿
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
        foreach (var element in levelElements)
        {
            if (element is Snake snake)
            {
                // NYI.
            }
        }
    }
}

