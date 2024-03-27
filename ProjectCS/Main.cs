using Engine.Utils;

namespace Sakimon
{
    class Sakimon
    {
        static void Main(string[] args)
        {
            GameManager gameManager = GameManager.GetInstance();
            gameManager.SetGameState(Utils.GetDictFromFile("Data/Save.txt")["gameState"][0]);
            gameManager.game.Run();
        }
    }
}
