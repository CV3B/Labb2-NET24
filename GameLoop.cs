namespace Labb2;

public class GameLoop
{
    private LevelData levelData;
    private Player player;

    private List<Wall> expoloredWalls = new List<Wall>();

    public GameLoop()
    {
        levelData = new LevelData();
        levelData.Load("Level1.txt");

        player = (Player)levelData.Elements.FirstOrDefault(elm => elm is Player);

        Run();
    }

    private void DrawLoop()
    {
        foreach (var element in levelData.Elements)
        {
            double distance = Math.Sqrt(Math.Pow(element.PostionX - player.PostionX, 2) + Math.Pow(element.PostionY - player.PostionY, 2));

            if (distance <= 5 && !expoloredWalls.Contains(element))
            {
                element.Draw();
                if (element is Wall wall) expoloredWalls.Add(wall);
            }
        }

        foreach (var exploredWall in expoloredWalls)
        {
            exploredWall.Draw();
        }
    }

    private void Run()
    {
        while (true)
        {
            DrawLoop();

            player.Update(levelData);

            if (player.Health <= 0)
            {
                Console.Clear();
                Console.WriteLine("Game Over - You died - Press any key to exit.");
                Console.ReadKey(true);
                break;
            }

            foreach (LevelElement element in levelData.Elements.ToList())
            {
                if (element is not Enemy enemy) continue;

                enemy.Update(levelData);

                if (enemy.Health > 0) continue;

                levelData.Elements.Remove(element);
                Console.SetCursorPosition(enemy.PostionX, enemy.PostionY);
                Console.Write('*');
                Console.SetCursorPosition(0, Console.BufferHeight - 5);
                Console.Write(new string(' ', Console.BufferWidth));
                Console.WriteLine($"You killed {enemy.Name}");
            }
        }
    }
}