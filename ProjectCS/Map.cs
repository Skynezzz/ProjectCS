using System;
using System.Numerics;
using Engine;
using Engine.Entities;
using Engine.Entities.Components;
using Engine.Utils;

namespace Sakimon.Entities.Map
{
    class Map : Entity
    {
        public Map() 
        {
            ReadMap();
        }
        public void ReadMap()
        {

            string maptxt = Utils.GetTextFromFile("Assets/Map.txt");
            string[] map = maptxt.Split('\n');

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    switch (map[y][x])
                    {
                        case 'T':
                            Tree tree = new Tree(x, y);
                            Game.GetInstance().AddMapEntity(tree);
                            break;
                        case 'H':
                            House house = new House(x, y);
                            Game.GetInstance().AddMapEntity(house);
                            break;
                        case 'L':
                            Labo labo = new Labo(x, y);
                            Game.GetInstance().AddMapEntity(labo);
                            break;
                    }
                }
            }
        }

    }

    class MapEntity : Entity
    {
        public MapEntity(int x, int y)
        {
            AddComponent(new Position(x, y, 5, 4));
        }
    }

    class Tree : MapEntity
    {
        public Tree(int x, int y) : base(x, y)
        {
            AddComponent(new Drawable("Assets/Tree.txt", GetComponent<Position>()));
            AddComponent(new Collider(0, 0, 5, 4));
        }
    }

    class House : MapEntity
    {
        public House(int x, int y) : base(x, y)
        {
            AddComponent(new Drawable("Assets/House.txt", GetComponent<Position>()));
            AddComponent(new Collider(2, 3, 14, 4));
        }

    }

    class Labo : MapEntity
    {
        public Labo(int x, int y) : base(x, y)
        {
            AddComponent(new Drawable("Assets/Labo.txt", GetComponent<Position>()));
        }

    }

}
