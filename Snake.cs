namespace Labb2;

public class Snake : Enemy
{
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

        double distance = Math.Sqrt(Math.Pow(player.PostionX - PostionX, 2) + Math.Pow(player.PostionY - PostionY, 2));

        if (distance > 2) return;

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
    }
}