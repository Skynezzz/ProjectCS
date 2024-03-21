using Engine;
using Engine.Utils;
using Engine.Entities.Components;
using Sakimon.Entities.Player;
using Sakimon.Entities.Pokemons;

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
            Dictionary<string, List<string>> dictAttack = Utils.GetDictFromFile("Attacks.txt");
            foreach (var attack in dictAttack)
            {
                attackList.Add(attack.Key, new Attack(attack.Key, int.Parse(attack.Value[0]), int.Parse(attack.Value[1]), int.Parse(attack.Value[2])));
            }
        }


        static void Main(string[] args)
        {
            Sakimon sakimon = new();
            sakimon.game.Run();

        }
    }
}
