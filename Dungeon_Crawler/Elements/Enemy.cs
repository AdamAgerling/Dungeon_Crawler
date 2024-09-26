
abstract class Enemy : LevelElement
    {
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defence { get; set; }

    public abstract void EnemyMovementPattern();

    }

