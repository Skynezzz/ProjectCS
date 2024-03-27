using Engine;
using Engine.Utils;
using Sakimon.Entities.Attacks;
using Sakimon.Entities.Map;
using Sakimon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakimon
{
    internal class GameManager
    {
        public static GameManager? instance = null;

        public readonly Game game;
        private Dictionary<string, Dictionary<string, List<string>>> gameStates;
        private Dictionary<string, List<string>> currentGameStates;
        private string indexGameState;

        public readonly Dictionary<string, Attack> attackList = new();

        public static GameManager GetInstance()
        {
            if (instance == null) instance = new GameManager();
            return instance;
        }

        private GameManager()
        {
            game = Game.GetInstance();
            gameStates = new Dictionary<string, Dictionary<string, List<string>>>();
            currentGameStates = new Dictionary<string, List<string>>();
            InitAttacks();
        }

        public void SetGameState(string state)
        {
            if (gameStates.ContainsKey(state) == false)
            {
                gameStates[state] = Engine.Utils.Utils.GetDictFromFile("Data/GameState/" + state + ".txt");
            }
            indexGameState = state;
            currentGameStates = gameStates[state];

            InitEntities();
        }

        private void InitEntities()
        {
            Game.GetInstance().AddMapEntity(new Map(currentGameStates["mapPath"][0]));
            if (bool.Parse(currentGameStates["playerEntity"][0]))
            {
                Entities.PlayerEntity player = new(42, 20);
                game.AddEntity(player);
            }
        }

        private void InitAttacks()
        {
            Dictionary<string, List<string>> dictAttack = Utils.GetDictFromFile("Data/Attacks.txt");
            foreach (var attack in dictAttack)
            {
                attackList.Add
                    (
                    attack.Key,
                    new Attack(
                        attack.Key,
                        int.Parse(attack.Value[0]),
                        int.Parse(attack.Value[1]),
                        int.Parse(attack.Value[2])
                        )
                    );
            }
        }
    }
}
