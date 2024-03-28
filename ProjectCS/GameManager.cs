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
using Sakimon.Entities.Pokemons;
using Microsoft.VisualBasic.FileIO;
using Engine.Entities.Components;
using System.Numerics;

namespace Sakimon
{
    internal class GameManager
    {
        public static GameManager? instance = null;

        public readonly Game game;
        private Dictionary<string, Dictionary<string, List<string>>> gameStates;
        private Dictionary<string, List<string>> currentGameStates;
        private string indexGameState;
        private string backIndexGameState;

        Entities.PlayerEntity? player;
        private Dictionary<string, Tuple<int, int>> playerPosition;
        public readonly Dictionary<string, Attack> attackList;
        private List<Tuple<string, int>> Pokemons;
        private Dictionary<string, int> inventory;

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

            playerPosition = new Dictionary<string, Tuple<int, int>>();
            attackList = new Dictionary<string, Attack>();
            Dictionary<string, List<String>> save = Utils.GetDictFromFile("Data/Save.txt");
            for (int i = 0; i < save["pokemons"].Count; i++)
            {
                //Pokemons.Add(new Tuple<string, int>(save["pokemons"][i], int.Parse(save["pokemons"][i + 1])));
            }
            InitAttacks();
        }

        public void SetGameState(string state)
        {
            game.ClearGame();

            if (player != null)
            {
                Vector2 playerPos = player.GetComponent<Position>().GetPosition();
                playerPosition[indexGameState] = new Tuple<int, int>((int)playerPos.X, (int)playerPos.Y);
                player = null;
            }
            if (gameStates.ContainsKey(state) == false)
            {
                gameStates[state] = Engine.Utils.Utils.GetDictFromFile("Data/GameState/" + state + ".txt");
            }
            backIndexGameState = indexGameState;
            indexGameState = state;
            currentGameStates = gameStates[state];


            InitEntities();
            game.RefreshDisplay();
        }

        private void InitEntities()
        {
            game.AddMapEntity(new Map(currentGameStates["mapPath"][0]));
            if (bool.Parse(currentGameStates["playerEntity"][0]))
            {
                if (playerPosition.ContainsKey(indexGameState)) player = new(playerPosition[indexGameState].Item1, playerPosition[indexGameState].Item2);
                else player = new(int.Parse(currentGameStates["playerPosition"][0]), int.Parse(currentGameStates["playerPosition"][1]));
                game.AddEntity(player);
            }

            if (currentGameStates.ContainsKey("back"))
            {
                List<string> back = currentGameStates["back"];
                Door exitDoor = new Door(backIndexGameState, int.Parse(back[0]), int.Parse(back[1]), int.Parse(back[2]), int.Parse(back[3]));
            }
            if (currentGameStates.ContainsKey("door"))
            {
                List<string> doors = currentGameStates["door"];
                for (int i = 0;  i < doors.Count; i += 5)
                {
                    Door exitDoor = new Door(doors[0], int.Parse(doors[1]), int.Parse(doors[2]), int.Parse(doors[3]), int.Parse(doors[4]));
                }
            }

            if (currentGameStates.ContainsKey("ennemi"))
            {
                string ennemi = currentGameStates["ennemi"][0];
                Dictionary<string, List<string>> pokemonData = Utils.GetDictFromFile("Data/Pokemons.txt");
                PokemonEntity ennemiPokemon = new PokemonEntity(12, 148);
                game.AddEntity(ennemiPokemon);
            }

            InitPnjs();
        }

        private void InitPnjs()
        {
            MarieClaire mc = new MarieClaire(10, 10);
            game.AddMapEntity(mc);
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

        public void WalkOnGrass()
        {
            Random random = new Random();
            if (random.Next(0, 100) != 0) return;
            SetGameState("GrassFight" + currentGameStates["difficulty"][0]);
        }
    }
}
