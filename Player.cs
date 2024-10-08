using System.Data;

namespace Labb2;

public class Player : LevelElement
{
    public string Name {get; set;}
    public int Health {get; set;}
    public Dice AttackDice = new Dice(2, 6, 2);
    public Dice DefenseDice = new Dice(2, 6, 0);

    
    public Player()
    {
        Health = 100;
        Character = '@';
        CharacterColor = ConsoleColor.Cyan;
    }

    public void Update(LevelData levelData)
    {
        // if (IsInCombat(levelData)) Combat(levelData);
        
                // Player player = (Player)levelData.Elements.FirstOrDefault(e => e is Player);
                
                ConsoleKey keyInput = Console.ReadKey(true).Key;


                int newPosX = PostionX;
                int newPosY = PostionY;
                
                switch (keyInput)
                {
                    case ConsoleKey.Escape:
                        return;
                    case ConsoleKey.W:
                        //move up
                        // _player.PostionY -= 1;     
                        // MovePlayer(0, -1);
                        newPosY -= 1;

                        break;
                    case ConsoleKey.A:
                        //move right
                        // _player.PostionX -= 1;
                        // MovePlayer(-1, 0);
                        newPosX -= 1;
                        break;
                    case ConsoleKey.S:
                        //move down
                        // _player.PostionY += 1;     
                        // MovePlayer(0, 1);
                        newPosY += 1;
                        break;
                    case ConsoleKey.D:
                        //move right
                        // _player.PostionX += 1;
                        // MovePlayer(1, 0);
                        newPosX += 1;
                        break;
                    case ConsoleKey.Q:
                        //attack
                        break;
                    case ConsoleKey.E:
                        //Defense
                        break;
                                
                                
                }

                foreach (var element in levelData.Elements)
                {
                    if (element is not Enemy enemy) continue;
                    Console.SetCursorPosition(0, 26);
                    Console.WriteLine(IsInCombat(newPosX, newPosY, enemy)); 
                    while (Health > 0 && enemy.Health > 0 && IsInCombat(newPosX, newPosY, enemy))
                    {
                        Attack(enemy, newPosX, newPosY);
                        // Console.SetCursorPosition(0, 29);
                        // Console.Write(new String(' ', Console.BufferWidth));
                        // Console.WriteLine("PLAYER ATTACk"); 
                        Thread.Sleep(1000);
                        enemy.Attack(this, PostionX, PostionY);
                        Thread.Sleep(1000);

                        // Console.WriteLine(IsInCombat(newPosX, newPosY, enemy));
                    }

                    // if (PostionX == enemy.PostionX && enemy.PostionY == PostionY)
                    // {
                    //     // Console.SetCursorPosition(0, 23);
                    //     // Console.WriteLine("PÅÅÅÅ HAN SÅ");
                    //     while (Health > 0 && enemy.Health > 0)
                    //     {
                    //         Attack(enemy, newPosX, newPosY);
                    //         Thread.Sleep(1000);
                    //         // Console.SetCursorPosition(50, 21);
                    //         // Console.WriteLine("attack2");
                    //         //
                    //         // enemy.Attack(this);
                    //     }
                    // }
                }
                
                Move(newPosX, newPosY, levelData);

                
    }

    
    private void Move(int x, int y, LevelData levelData)
    {
        if (!IsMoveValid(x ,y, levelData)) return;
                
        int prevX = PostionX;
        int prevY = PostionY;
                
        PostionX = x;
        PostionY = y;
                
        Console.SetCursorPosition(prevX, prevY);
        Console.Write(" ");
                
        Draw();
                
                
                
    }
    private bool IsMoveValid(int newX, int newY, LevelData levelData)
    {
        foreach (LevelElement element in levelData.Elements)
        {
            // if (element is Player) continue;
            if (element.PostionX == newX && element.PostionY == newY) return false;
        }
                
        return true;
    }


    public void Attack(Enemy enemy, int x, int y)
    {
        if (x != enemy.PostionX && y != enemy.PostionY) return;

        
        int playerDamage = AttackDice.Throw() - enemy.DefenseDice.Throw();

        if (playerDamage > 0)
        {
            enemy.Health -= playerDamage;
            Console.SetCursorPosition(0, 21);
            // Console.WriteLine();
            Console.Write(new String(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, 21);

            Console.WriteLine($"You hit {enemy.Name} and takes {playerDamage} damage | HP: {Health} | ENEMY HP {enemy.Health}");
        }
        else
        {
            Console.SetCursorPosition(0, 21);
            Console.Write(new String(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, 21);

            Console.WriteLine("You missed the attack");
        }
        
    }
    

    public bool IsInCombat(int x, int y, Enemy enemy)
    {
        // if (element is not Enemy enemy) continue;
        Console.SetCursorPosition(0, 27);
        Console.WriteLine($"x:{x} y:{y} | Ex: {enemy.PostionX} Ey: {enemy.PostionY} ");
        return x == enemy.PostionX && y == enemy.PostionY;
    }
    
    protected void Combat(LevelData levelData)
    {
        foreach (var element in levelData.Elements)
        {
            if (element is not Enemy enemy) continue;
            if (PostionX == enemy.PostionX && PostionY == enemy.PostionY)
            {
               
                
                while (Health > 0 || enemy.Health > 0)
                {
                    int playerDamage = AttackDice.Throw() - enemy.DefenseDice.Throw();
                    int enemyDamage = enemy.AttackDice.Throw() - DefenseDice.Throw();    
                    
                    if (playerDamage > 0 || enemyDamage > 0)
                    {
                        // Console.SetCursorPosition(0, 24);
                        // Console.WriteLine("combat" + combat);
                        // Console.WriteLine("Enemy hp:" + enemy.Health);
                        
                        enemy.Health -= playerDamage;
                        Health -= enemy.Health > 0 ? enemyDamage : 0;
                    }
                    
                    

                }
            }
            
        }
    }
}