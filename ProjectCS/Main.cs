using Engine;
using Engine.Utils;
using Engine.Entities.Components;
using Sakimon.Entities.Player;
using Sakimon.Entities.Pokemons;
using Sakimon.Entities.Attacks;
using Sakimon.Entities.Map;

namespace Sakimon
{
    class Sakimon
    {
        private readonly Dictionary<string, Attack> attackList = new();
        private readonly Game game;
        private Sakimon()
        {
            game = Game.GetInstance();

            InitAttacks();
            Map map = new();
            Pokemon pikachu = new();
            game.AddMapEntity(pikachu);
            Player player = new();
            game.AddEntity(player);
            if (attackList.ContainsKey("Queue de Fer"))
            {
                pikachu.attackList.Add(attackList.GetValueOrDefault("Queue de Fer"));
            }
        }

        private void InitAttacks()
        {
            Dictionary<string, List<string>> dictAttack = Utils.GetDictFromFile("Data/Attacks.txt");
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
