namespace Labb2;

public class Player : LevelElement
{
    public string Name { get; set; }
    public int Health { get; set; }
    public Dice AttackDice = new Dice(2, 6, 2);
    public Dice DefenseDice = new Dice(2, 6, 0);

    public Player()
    {
        Health = 100;
        Character = '@';
        CharacterColor = ConsoleColor.Cyan;
        Name = "Player";
    }

    public void Update(LevelData levelData)
    {
        ConsoleKey keyInput = Console.ReadKey(true).Key;

        int newPosX = PostionX;
        int newPosY = PostionY;

        switch (keyInput)
        {
            case ConsoleKey.Escape:
                // FUNGERAR INTE - orkar inte fixa
                break;
            case ConsoleKey.W:
                newPosY -= 1;
                break;
            case ConsoleKey.A:
                newPosX -= 1;
                break;
            case ConsoleKey.S:
                newPosY += 1;
                break;
            case ConsoleKey.D:
                newPosX += 1;
                break;
        }

        foreach (var element in levelData.Elements)
        {
            if (element is not Enemy enemy) continue;

            while (Health > 0 && enemy.Health > 0 && IsInCombat(newPosX, newPosY, enemy))
            {
                Attack(enemy, newPosX, newPosY);
                Thread.Sleep(2000);
                enemy.Attack(this, PostionX, PostionY);
                Thread.Sleep(2000);
            }
        }

        Move(newPosX, newPosY, levelData);
    }

    private void Move(int x, int y, LevelData levelData)
    {
        if (!IsMoveValid(x, y, levelData)) return;

        int prevX = PostionX;
        int prevY = PostionY;

        PostionX = x;
        PostionY = y;

        Console.SetCursorPosition(prevX, prevY);
        Console.Write(" ");

        Draw();
    }

    private bool IsMoveValid(int x, int y, LevelData levelData)
    {
        foreach (LevelElement element in levelData.Elements)
        {
            if (element.PostionX == x && element.PostionY == y) return false;
        }

        return true;
    }

    public void Attack(Enemy enemy, int x, int y)
    {
        if (x != enemy.PostionX && y != enemy.PostionY) return;
        if (Health <= 0) return;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(0, Console.BufferHeight - 4);
        Console.Write(new String(' ', Console.BufferWidth));
        Console.SetCursorPosition(0, Console.BufferHeight - 4);
        Console.WriteLine($"Hp: {Health}");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan;

        int playerDamage = AttackDice.Throw() - enemy.DefenseDice.Throw();

        if (playerDamage > 0)
        {
            enemy.Health -= playerDamage;

            SetCorrectCursorPos();
            Console.WriteLine($"You hit {enemy.Name} and it takes {playerDamage} damage");
        }
        else
        {
            SetCorrectCursorPos();
            Console.WriteLine($"You missed the attack, with {playerDamage * -1} defense from {enemy.Name}");
        }

        Console.ResetColor();
    }

    public bool IsInCombat(int x, int y, Enemy enemy)
    {
        return x == enemy.PostionX && y == enemy.PostionY;
    }

    private static void SetCorrectCursorPos()
    {
        Console.SetCursorPosition(0, Console.BufferHeight - 3);
        Console.Write(new String(' ', Console.BufferWidth));
        Console.SetCursorPosition(0, Console.BufferHeight - 3);
    }
}