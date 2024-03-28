using Engine;
using Engine.Entities;
using Engine.Entities.Components;
using Engine.Utils;
using Sakimon.Entities.Map;
using Sakimon.Entities.Attacks;

namespace Sakimon.Entities.Pokemons
{
    class PokemonEntity : Entity
    {
        public readonly List<Attack?> attackList;

        public PokemonEntity(int x, int y)
        {
            attackList = new List<Attack?>(4);
            AddComponent(new Position(x, y));
            Drawable drawable = new Drawable("", GetComponent<Position>());
            AddComponent(drawable);
        }

        public void AddAttack(Attack attack, int index = 0)
        {
            if (index == 0 && attackList.Count() < 4)
            {
                attackList.Add(attack);
                return;
            }
            if (index == 0) { return; }
            attackList[index] = attack;
        }

        public List<Attack?> GetAttacks()
        {
            List<Attack?> returnList = new(4);
            foreach (var attack in attackList)
            {
                returnList.Add(attack);
            }
            return returnList;
        }

        public class Pikachu : PokemonEntity
        {
            public Pikachu(int x, int y) : base(x, y)
        
            {
                AddComponent(new Position(x, y));
                Drawable drawable = new Drawable("Assets/Pikachu.txt", GetComponent<Position>());
                AddComponent(drawable);
            }
            
        }





    }
}