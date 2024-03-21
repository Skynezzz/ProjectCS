using System;
using System.Numerics;
using Engine.Entities;
using Engine.Entities.Components;
using Engine.Utils;

namespace Sakimon.Entities.Map
{
    class Map : Entity
    {

        public void ReadMap()
        {

            string maptxt = Utils.GetTextFromFile("Map.txt");
            string[] map = maptxt.Split('\n');

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    switch (map[y][x])
                    {
                        case 'T':
                            Tree tree = new Tree(x, y);
                            break;
                        case 'H':
                            House house = new House(x, y);
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

        }
    }

    class Tree : MapEntity
    {
        public Tree(int x, int y) : base(x, y)
        {
            AddComponent(new Drawable("ƐMM3\nƐYY3\n || "));
        }
    }

    class House : MapEntity
    {
        public House(int x, int y) : base(x, y)
        {
            string housetxt = Utils.GetTextFromFile("House.txt");
            string[] house = housetxt.Split('\n');
            AddComponent(new Drawable(""));
        }

    }

}
