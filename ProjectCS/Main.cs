using Engine;
using Engine.Utils;
using Engine.Entities.Components;
using Sakimon.Entities.PlayerEntity;
using Sakimon.Entities.Pokemons;
using Sakimon.Entities.Attacks;
using Sakimon.Entities.Map;

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
