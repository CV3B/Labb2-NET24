using System.Text.Json.Serialization.Metadata;

namespace Labb2;

public class Rat : Enemy
{
    // public string Name = "Sewage rat";
    // public int Health;
    
    
    Random rnd = new Random();
    
    String[] directions = new []{"up", "left", "down", "right"};
    
    public Rat()
    {
        Character = 'r';
        CharacterColor = ConsoleColor.Green;
        AttackDice = new Dice(1, 6, 3); 
        DefenseDice = new Dice(1, 6, 1);
        Health = 10;
        Name = "Sewage rat";
    }

    public override void Update(LevelData levelData)
    {
        int rndDirection = rnd.Next(0, directions.Length);
        
        // if (IsInCombat(levelData)) Combat(levelData);
        Player player = (Player)levelData.Elements.FirstOrDefault(e => e is Player);
        

        // if (PostionX == player.PostionX && PostionY == player.PostionY)
        {
            // Console.SetCursorPosition(50, 22);
            // Console.WriteLine("attack1");
            // Console.SetCursorPosition(50, 23);
            // Console.WriteLine(); 
            // Console.WriteLine("attack12");
            // Attack(player);
            // UpdateLivingState(levelData);
        }
        // else
        {
            int newPosX = PostionX;
            int newPosY = PostionY;
            
            switch (directions[rndDirection])
            {
                case "up":
                    // this.PostionY -= 1;
                    // this.Move(0, -1, levelData);
                    newPosY -= 1;
                    break;
                case "left":
                    // this.PostionX -= 1;
                    // this.Move(-1, 0, levelData);
                    newPosX -= 1;
                    break;
                case "down":
                    // this.PostionY += 1;
                    // this.Move(0, 1, levelData);
                    newPosY += 1;
                    break;
                case "right":
                    // this.PostionX += 1;
                    // this.Move(1, 0, levelData);
                    newPosX += 1;
                    break;
            }
            
            
            // while (Health > 0 && player.Health > 0 && IsInCombat(newPosX, newPosY, player))
            {
                // Attack(player, newPosX, newPosY);
                // Thread.Sleep(100);
                // player.Attack(this, newPosX, newPosY);
                // Console.WriteLine(IsInCombat(newPosX, newPosY, player));

            
            }
            HandleCombat(newPosX, newPosY, player);
            Move(newPosX, newPosY, levelData);
        }

        
        
        
    }

    public override void Move(int x, int y, LevelData levelData)
    {
        if (!IsMoveValid(x, y, levelData)) return;
                
        int prevX = PostionX;
        int prevY = PostionY;
                
        this.PostionX = x;
        this.PostionY = y;
                
        Console.SetCursorPosition(prevX, prevY);
        Console.Write(" ");
    }


    
    
}