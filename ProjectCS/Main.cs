using Engine;
using Engine.Entities;
using Engine.Entities.Components;
using Sakimon.Pokemons;

namespace Sakimon
{
    class Sakimon
    {
        private readonly Dictionary<string, Attack> attackList = new();
        private readonly Game game;
        private Sakimon()
        {
            game = new();

            InitAttacks();
            Pokemon pikachu = new();
            game.AddEntity(pikachu);
            Player player = new();
            game.AddEntity(player);
            if (attackList.ContainsKey("Queue de Fer"))
            {
                pikachu.attackList.Add(attackList.GetValueOrDefault("Queue de Fer"));
            }
        }

        private void InitAttacks()
        {
            attackList.Add("Vive Attaque", new("Vive Attaque", 0, 10, 20));
            attackList.Add("Queue de Fer", new("Queue de Fer", 1, 15, 40));
        }


        static void Main(string[] args)
        {
            Sakimon sakimon = new();
            sakimon.game.Run();

        }


    }
}
