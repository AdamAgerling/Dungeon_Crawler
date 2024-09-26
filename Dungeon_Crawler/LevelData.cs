


//Vidare har LevelData en metod, Load(string filename), som läser in data från filen man anger vid anrop.
//Load läser igenom textfilen tecken för tecken, och för varje tecken den hittar som är någon av #, r, eller s,
//så skapar den en ny instans av den klass som motsvarar tecknet och lägger till en referens till instansen på “elements”-listan.
//Tänk på att när instansen skapas så måste den även få en (X/Y) position; d.v.s Load behöver alltså hålla reda på vilken rad och kolumn i filen som tecknet hittades på.
//Den behöver även spara undan startpositionen för spelaren när den stöter på @.

class LevelData
    {
    private List<LevelElement> elements = new List<LevelElement>();

    // elements should be readOnly.


    public static void Load(string filename)
        {
        // Should keep track of position X,Y. And stuff.
        }
    }

