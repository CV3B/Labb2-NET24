namespace Labb2;

public class GameLoop
{       
        private LevelData _levelData;
        private Player _player;

        private List<Wall> expoloredWalls = new List<Wall>();

        public GameLoop()
        {
                _levelData = new LevelData();
                _levelData.Load("Level1.txt");

                //IsMoveValid(4, 3);
                _player = (Player)_levelData.Elements.FirstOrDefault(elm => elm is Player);

                
                 //keyInput = Console.ReadKey().Key;0
                 //Console.WriteLine(_player);
                while (true)
                {
                        Console.SetCursorPosition(0, 20);
                        Console.WriteLine(_player.Health + "\n\n");
                        
                        
                        DrawLoop();


                        _player.Update(_levelData);
                        // ConsoleKey keyInput = Console.ReadKey(true).Key;
                        //
                        // switch (keyInput)
                        // {
                        //         case ConsoleKey.Escape:
                        //                 return;
                        //         case ConsoleKey.W:
                        //                 //move up
                        //                 // _player.PostionY -= 1;     
                        //                 MovePlayer(0, -1);
                        //
                        //                 break;
                        //         case ConsoleKey.A:
                        //                 //move right
                        //                 // _player.PostionX -= 1;
                        //                 MovePlayer(-1, 0);
                        //                 break;
                        //         case ConsoleKey.S:
                        //                 //move down
                        //                 // _player.PostionY += 1;     
                        //                 MovePlayer(0, 1);
                        //                 break;
                        //         case ConsoleKey.D:
                        //                 //move right
                        //                 // _player.PostionX += 1;
                        //                 MovePlayer(1, 0);
                        //                 break;
                        //         case ConsoleKey.Q:
                        //                 //attack
                        //                 break;
                        //         case ConsoleKey.E:
                        //                 //Defense
                        //                 break;
                        //         
                        //         
                        // }
                        
                        // _player.Update(_levelData);
                        
                        // if (_player.IsInCombat())

                        foreach (LevelElement element in _levelData.Elements.ToList())
                        {
                                if (element is Enemy enemy)
                                {
                                        enemy.Update(_levelData);
                                        
                                        // Console.SetCursorPosition(10, 26);
                                        // Console.WriteLine("ENEMY HPPP" + enemy.Health);

                                        // Console.SetCursorPosition(0, 26);
                                        // Console.WriteLine();
                                        // Console.WriteLine("jas");
                                        if (enemy.Health <= 0)
                                        {
                                                _levelData.Elements.Remove(element);
                                                Console.SetCursorPosition(0, 26);
                                                Console.WriteLine("dead");
                                                Console.WriteLine(enemy.GetType());
                                        }

                                }
                        }


                        if (_player.Health <= 0)
                        {
                                Console.Clear();
                                Console.WriteLine("\nGame Over!");
                        }
                        //Console.Clear();

                }
        }

        private void DrawLoop()
        {
                
                foreach (var element in _levelData.Elements)
                {
                        //if (element is Player player) _player = player; 
                        
                        double distance = Math.Sqrt(Math.Pow(element.PostionX - _player.PostionX, 2) + Math.Pow(element.PostionY - _player.PostionY, 2));
                        // Console.WriteLine(distance);
                        if (distance <= 5000 && !expoloredWalls.Contains(element))
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

        // private void MovePlayer(int x, int y)
        // {
        //         if (!IsMoveValid(_player.PostionX + x ,_player.PostionY + y)) return;
        //         
        //         int prevX = _player.PostionX;
        //         int prevY = _player.PostionY;
        //         
        //         _player.PostionX += x;
        //         _player.PostionY += y;
        //         
        //         Console.SetCursorPosition(prevX, prevY);
        //         Console.Write(" ");
        //         
        //         _player.Draw();
        //         
        //         
        //         
        // }
        // private bool IsMoveValid(int newX, int newY)
        // {
        //         foreach (LevelElement element in _levelData.Elements)
        //         {
        //                 // if (element is Player) continue;
        //                 if (element.PostionX == newX && element.PostionY == newY) return false;
        //         }
        //         
        //         return true;
        // }

        public void CombatTurn(bool playersTurn)
        {
                if (playersTurn)
                {
                        // _player.Attack(); 
            
                        playersTurn = false;
                } else if (!playersTurn)
                {
                        // enemy.Attack();
                        playersTurn = true;

                }
        }

        private void UpdateFight(Enemy enemy)
        {
                while (_player.Health > 0 && enemy.Health > 0)
                {
                        continue;
                }
        }
        
        
}