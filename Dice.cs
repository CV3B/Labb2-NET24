namespace Labb2;

public class Dice
{

    private int NumberOfDice { get; set; }
    private int SidePerDice { get; set; }
    private int Modifier { get; set; }
    
    Random rnd = new Random();
    
    public Dice(int numberOfDice, int sidePerDice, int modifier)
    {
        NumberOfDice = numberOfDice;
        SidePerDice = sidePerDice;
        Modifier = modifier;
    }

    public int Throw()
    {
        int points = 0;
        
        for (int i = 0; i < NumberOfDice; i++)
        {
            points += rnd.Next(1, SidePerDice+1);
        }

        return points + Modifier;
    }

    public override string ToString()
    {
        return $"{NumberOfDice}d{SidePerDice}+{Modifier}";
    }
}