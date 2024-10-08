using System.Runtime.CompilerServices;

namespace Labb2;

public abstract class Enemy : LevelElement
{
    public string Name {get; set;}
    public int Health {get; set;}
    public Dice AttackDice  {get; set;}
    public Dice DefenseDice {get; set;}

    // public bool IsInCombat = false;

    public abstract void Update(LevelData levelData);

    public abstract void Move(int x, int y, LevelData levelData);
    
    protected static bool IsMoveValid(int x, int y, LevelData levelData)
    {
        
        foreach (LevelElement element in levelData.Elements)
        {
            // if (element is Player) continue;
            if (element.PostionX == x && element.PostionY == y) return false;
        }
                
        return true;
    }
    
    public void Attack(Player player, int x, int y)
    {
        if (x != player.PostionX && y != player.PostionY) return;
        
        int enemyDamage = AttackDice.Throw() - player.DefenseDice.Throw();

        if (enemyDamage > 0)
        {
            player.Health -= enemyDamage;
            Console.SetCursorPosition(0, 22);
            Console.Write(new String(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, 22);

            Console.Write($"{Name} attacked you with {enemyDamage} damage | HP: {Health} | PLAYER HP: {player.Health}");
        }
        else
        {
            Console.SetCursorPosition(0, 22);
            Console.Write(new String(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, 22);

            Console.Write($"{Name} missed the attack");
        }

    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    // public bool CombatTurn(LevelElement element)
    // {
    //     if (element is Player) 
    // }
    //
    protected bool IsInCombat(int x, int y, Player player)
    {
        // if (element is not Enemy enemy) continue;
        // Console.SetCursorPosition(0, 28);
        // Console.WriteLine($"x:{x} y:{y} | Player_x: {player.PostionX} Player_y: {player.PostionY} ");
        
        if (x == player.PostionX && y == player.PostionY) return true;
        
        return false;
    } 

    protected void HandleCombat(int newPosX, int newPosY, Player player)
    {
        while (Health > 0 && player.Health > 0 && IsInCombat(newPosX, newPosY, player))
        {
            // Console.SetCursorPosition(20, 29);
            // Console.Write(new String(' ', Console.BufferWidth));
            // Console.WriteLine("ENEMY ATTACk");
            Attack(player, newPosX, newPosY);

            Thread.Sleep(1000);
            player.Attack(this, PostionX, PostionY);
            Thread.Sleep(1000);


        }
    }

    protected void Combat(LevelData levelData)
    {
        foreach (var element in levelData.Elements)
        {
            if (element is not Player player) continue;
            
            if (PostionX == player.PostionX && PostionY == player.PostionY)
            {
                
                // while (Health > 0 || player.Health > 0)
                {
                    int combat = AttackDice.Throw() - player.DefenseDice.Throw();

                    if (combat > 0)
                    {
                        Console.SetCursorPosition(0, 22);
                        Console.WriteLine("combat" + combat);

                        player.Health -= combat;
                    }

                    player.Update(levelData);
                }
            }
        }
    }
}