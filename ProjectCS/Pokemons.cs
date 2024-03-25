using Engine;
using Engine.Entities;
using Engine.Entities.Components;
using Sakimon;
using Sakimon.Entities.Attacks;
using System.Numerics;

namespace Sakimon.Entities.Pokemons
{
    class Pokemon : Entity
    {
        public readonly List<Attack?> attackList;

        public Pokemon()
        {
            attackList = new List<Attack?>(4);

            AddComponent(new Position(20, 10));
            AddComponent(new Drawable("Assets/Pikachu.txt", GetComponent<Position>()));
            AddComponent(new AliveEntity(100));
            AddComponent(new Collider(new Vector2(0), new Vector2(3)));
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
    }
}