
class Dice
{
    private int _modifier;
    private int _numberOfDice;
    private int _sidesPerDice;
    public Dice(int numberOfDice, int sidesPerDice, int modifier)
    {
        _numberOfDice = numberOfDice;
        _sidesPerDice = sidesPerDice;
        _modifier = modifier;
    }

    public int Throw()
    {
        var random = new Random();
        var value = 0;
        for (int i = 0; i < _numberOfDice; i++)
        {
            value += random.Next(0, _sidesPerDice + 1);
        }
        return value + _modifier;
    }

    public override string ToString()
    {
        return $"{_numberOfDice}d{_sidesPerDice} + {_modifier}";
    }
}