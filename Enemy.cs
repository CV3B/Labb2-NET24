namespace Labb2;

public abstract class Enemy : LevelElement
{
    public string Name { get; set; }
    public int Health { get; set; }
    public Dice AttackDice { get; set; }
    public Dice DefenseDice { get; set; }

    public abstract void Update(LevelData levelData);

    protected void Move(int x, int y, LevelData levelData)
    {
        if (!IsMoveValid(x, y, levelData)) return;

        int prevX = PostionX;
        int prevY = PostionY;

        PostionX = x;
        PostionY = y;

        Console.SetCursorPosition(prevX, prevY);
        Console.Write(" ");
    }

    protected static bool IsMoveValid(int x, int y, LevelData levelData)
    {
        foreach (LevelElement element in levelData.Elements)
        {
            if (element.PostionX == x && element.PostionY == y) return false;
        }

        return true;
    }

    public void Attack(Player player, int x, int y)
    {
        if (x != player.PostionX && y != player.PostionY) return;
        if (Health <= 0) return;

        Console.ForegroundColor = ConsoleColor.DarkYellow;

        int enemyDamage = AttackDice.Throw() - player.DefenseDice.Throw();

        if (enemyDamage > 0)
        {
            player.Health -= enemyDamage;

            SetCorrectCursorPos();
            Console.Write($"{Name} attacked you with {enemyDamage} damage | HP: {(Health > 0 ? Health : 0)}");
        }
        else
        {
            SetCorrectCursorPos();
            Console.Write($"{Name} missed the attack, with {enemyDamage * -1} defense from you | HP: {(Health > 0 ? Health : 0)}");
        }
        Console.ResetColor();
    }

    private static void SetCorrectCursorPos()
    {
        Console.SetCursorPosition(0, Console.BufferHeight - 2);
        Console.Write(new String(' ', Console.BufferWidth));
        Console.SetCursorPosition(0, Console.BufferHeight - 2);
    }

    private bool IsInCombat(int x, int y, Player player)
    {
        return x == player.PostionX && y == player.PostionY;
    }

    protected void HandleCombat(int x, int y, Player player)
    {
        while (Health > 0 && player.Health > 0 && IsInCombat(x, y, player))
        {
            Attack(player, x, y);

            Thread.Sleep(2000);
            player.Attack(this, PostionX, PostionY);
            Thread.Sleep(2000);
        }
    }
}