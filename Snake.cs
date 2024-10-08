namespace Labb2;

public class Snake : Enemy
{
    // public string Name = "snake";
    // public int Health;
    //public Dice AttackDice = null;
    //public Dice DefenseDice = null;
   
    public Snake()
    {
        Character = 's';
        CharacterColor = ConsoleColor.Red;
        AttackDice = new Dice(3, 4, 2);
        DefenseDice = new Dice(1, 8, 5);
        Health = 25;
        Name = "Snake";
    }

    public override void Update(LevelData levelData)
    {
        Player player = (Player)levelData.Elements.FirstOrDefault(e => e is Player);
        


        if (PostionX == player.PostionX && PostionY == player.PostionY)
        {
            // Attack(player);
            // UpdateLivingState(levelData);

        }
        
        double distance = Math.Sqrt(Math.Pow(player.PostionX - PostionX, 2) + Math.Pow(player.PostionY - PostionY, 2));
        
        if (distance > 2) return;

        // if (PostionX >= player.PostionX)
        // {
        //     Move(0, -1, levelData);
        //     
        // } else if (PostionX <= player.PostionX)
        // {
        //     Move(-1, 0, levelData);
        //     
        // } else if (PostionY >= player.PostionY)
        // {
        //     Move(0, 1, levelData);
        //     
        // } else if (PostionY <= player.PostionY)
        // {
        //     Move(1, 0, levelData);
        // }
        
        
        int distanceX = Math.Abs(PostionX - player.PostionX);
        int distanceY = Math.Abs(PostionY - player.PostionY);
        
        int newPosX = PostionX;
        int newPosY = PostionY;
        if (distanceX >= distanceY)
        {
            newPosX += PostionX > player.PostionX ? 1 : -1; 
        }
        else
        {
            newPosY += PostionY > player.PostionY ? 1 : -1; 
        }
        
        
        HandleCombat(newPosX, newPosY, player);

        Move(newPosX, newPosY, levelData);
    } // 0,1 0,0

    public override void Move(int x, int y, LevelData levelData)
    {
        if (!IsMoveValid(x, y, levelData)) return;
                
        int prevX = this.PostionX;
        int prevY = this.PostionY;
                
        this.PostionX = x;
        this.PostionY = y;
                
        Console.SetCursorPosition(prevX, prevY);
        Console.Write(" ");
    }
    
}

/*
 *
 *
          if (distanceX >= distanceY)  // Prioritize moving away horizontally
            {
                moveX = this.PostionX > player.PostionX ? 1 : -1;  // Move right if left of player, left if right
            }
            else  // Otherwise, move away vertically
            {
                moveY = this.PostionY > player.PostionY ? 1 : -1;  // Move down if above player, up if below
            }
 * 
 */
