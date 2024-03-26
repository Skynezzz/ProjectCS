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
                        case 'D':
                            Door door = new Door(x, y);
                            Game.GetInstance().AddMapEntity(door);
                            break;
                        case 'P':
                            Pnj pnj = new Pnj(x, y);
                            Game.GetInstance().AddMapEntity(pnj);
                            break;
                        case 'W':
                            Water water = new Water(x, y);
                            Game.GetInstance().AddMapEntity(water);
                            break;
                        case 'F':
                            Wall wall = new Wall(x, y);
                            Game.GetInstance().AddMapEntity(wall);
                            break;
                    }
                }
            }
        }

    }

    class MapEntity : Entity
    {
        public MapEntity(int x, int y, int sx, int sy)
        {
            AddComponent(new Position(x, y, sx, sy));
        }
    }

    class Tree : MapEntity
    {
        public Tree(int x, int y) : base(x, y, 5, 4)
        {
            AddComponent(new Drawable("Assets/Tree.txt", GetComponent<Position>()));
            AddComponent(new Collider(0, 0, 5, 4));
        }
    }

    class House : MapEntity
    {
        public House(int x, int y) : base(x, y, 18, 8)
        {
            AddComponent(new Drawable("Assets/House.txt", GetComponent<Position>()));
            AddComponent(new Collider(0, 0, 18, 8));
        }
    }

    class Labo : MapEntity
    {
        public Labo(int x, int y) : base(x, y, 18, 6)
        {
            AddComponent(new Drawable("Assets/Labo.txt", GetComponent<Position>()));
            AddComponent(new Collider(0, 0, 18, 6));
        }
    }

    class Door : MapEntity
    {
        public Door(int x, int y) : base(x, y, 3, 3)
        {
            AddComponent(new Drawable("Assets/Door.txt", GetComponent<Position>()));
            Collider collider = new Collider(0, 0, 4, 3);
            collider.SetOnCollisionEvent(new MapEvent("Assets/LaboMap.txt"));
            AddComponent(collider);
        }
    }

    class Wall : MapEntity
    {
        public Wall(int x, int y) : base(x, y, 2, 2)
        {
            AddComponent(new Drawable("Assets/Wall.txt", GetComponent<Position>()));
            AddComponent(new Collider(0, 0, 2, 2));
        }
    }

    class Pnj : MapEntity
    {
        public Pnj(int x, int y) : base(x, y, 3, 3)
        {
            AddComponent(new Drawable("Assets/Pnj.txt", GetComponent<Position>()));
            AddComponent(new Collider(0, 0, 3, 3));
        }
    }

    class Water : MapEntity
    {
        public Water(int x, int y) : base(x, y, 1, 2)
        {
            AddComponent(new Drawable("Assets/Water.txt", GetComponent<Position>()));
            AddComponent(new Collider(0, 0, 1, 2));
        }
    }

    class MapEvent : Event
    {
        private string path;

        public MapEvent(string pPath) { path = pPath; }

        public override void Update()
        {
            base.Update();
            //GameManager.LoadScene(path);
        }
    }
}
