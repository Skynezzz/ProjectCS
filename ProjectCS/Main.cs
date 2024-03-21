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
            string textAttack = Utils.GetTextFromFile("Attacks.txt");
            string[] attacks = textAttack.Split('\n');
            foreach (string attack in attacks)
            {
                string[] props = attack.Split(';');

                string name = props[0];
                int type = int.Parse(props[1]);
                int pp = int.Parse(props[2]);
                int dmg = int.Parse(props[3]);

                attackList.Add(name, new Attack(name, type, pp, dmg));
            }
        }


        static void Main(string[] args)
        {
            Sakimon sakimon = new();
            sakimon.game.Run();

        }
    }
}
