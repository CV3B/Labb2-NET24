namespace Labb2;

public class LevelData
{
    // DET BLIR TVÅ INSTANSER AV DETTA
    private List<LevelElement> elements = new List<LevelElement>();
    public List<LevelElement> Elements { get { return elements; } }

    public void Load(string filename)
    {
        string currDirectory = Directory.GetCurrentDirectory();
        string path = Path.Combine(currDirectory, filename);
        string[] file = File.ReadAllLines(path);

        for (int i = 0; i < file.Length; i++)
        {
            for (int j = 0; j < file[i].Length; j++)
            {
                {
                    switch (file[i][j])
                    {
                        case '#':
                        {
                            elements.Add(new Wall() {PostionX = j, PostionY = i});
                            break;
                        }
                        case 'r':
                        {
                            elements.Add(new Rat() {PostionX = j, PostionY = i});
                            break;
                        }
                        case 's':
                        {
                            elements.Add(new Snake() {PostionX = j, PostionY = i});
                            break;
                        }
                        case '@':
                        {
                            elements.Add(new Player() { PostionX = j, PostionY = i });
                            break;
                        }
                    }
                }
            }
        }
        
    }
}