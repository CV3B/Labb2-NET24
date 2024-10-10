namespace Labb2;

public class Rat : Enemy
{
    Random rnd = new Random();

    String[] directions = new[] { "up", "left", "down", "right" };

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

        Player player = (Player)levelData.Elements.FirstOrDefault(e => e is Player);

        int newPosX = PostionX;
        int newPosY = PostionY;

        switch (directions[rndDirection])
        {
            case "up":
                newPosY -= 1;
                break;
            case "left":

                newPosX -= 1;
                break;
            case "down":

                newPosY += 1;
                break;
            case "right":
                newPosX += 1;
                break;
        }

        HandleCombat(newPosX, newPosY, player);
        Move(newPosX, newPosY, levelData);
    }
}