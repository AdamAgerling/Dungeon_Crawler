﻿

//Spelet använder sig av simulerade tärningskast för att avgöra hur mycket skada spelaren och fienderna gör på varandra.

//Skapa en klass “Dice” med en konstruktor som tar 3 parametrar: numberOfDice, sidesPerDice och Modifier.
//Genom att skapa nya instans av denna kommer man kunna skapa olika uppsättningar av tärningar t.ex “3d6+2”, d.v.s slag med 3 stycken 6-sidiga tärningar,
//där man tar resultatet och sedan plussar på 2, för att få en total poäng.

//Dice-objekt ska ha en publik Throw() metod som returnerar ett heltal med den poäng man får när man slår med tärningarna enligt objektets konfiguration.
//Varje anrop motsvarar alltså ett nytt kast med tärningarna.

//Gör även en override av Dice.ToString(), så att man när man skriver ut ett Dice-objekt får en sträng som beskriver objektets konfiguration. t.ex: “3d6 + 2”.
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

