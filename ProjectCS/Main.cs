using Engine.Utils;

namespace Sakimon
{
    class Sakimon
    {
        static void Main(string[] args)
        {
            GameManager gameManager = GameManager.GetInstance();
            gameManager.SetGameState("Lunch");
            gameManager.game.Run();
        }
    }
}
