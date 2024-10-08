namespace Labb2;

public abstract class LevelElement
{
    public int PostionX { get; set; }
    public int PostionY { get; set; }
    
    public char Character { get; set; }
    
    public ConsoleColor CharacterColor { get; set; }

    public void Draw()
    {
        
        Console.ForegroundColor = CharacterColor;
        Console.SetCursorPosition(PostionX, PostionY);
        Console.Write(Character);

        // Console.WriteLine(this.Character);
        // Console.WriteLine("a");
        // Console.ResetColor();
    }
    

}