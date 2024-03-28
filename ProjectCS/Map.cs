using Engine;
using Engine.Entities;
using Engine.Entities.Components;
using Engine.Utils;

namespace Sakimon.Entities.Map
{
    class Map : Entity
    {
        private string path;

        public Map(string pPath)
        {
            path = pPath;
            ReadMap();
        }
        public void ReadMap()
        {
            string maptxt = Utils.GetTextFromFile(path);
            string[] map = maptxt.Split('\n');
            Game game = Game.GetInstance();

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    switch (map[y][x])
                    {
                        case 'T':
                            Tree tree = new Tree(x, y);
                            game.AddMapEntity(tree);
                            break;
                        case 'H':
                            House house = new House(x, y);
                            game.AddMapEntity(house);
                            break;
                        case 'L':
                            Labo labo = new Labo(x, y);
                            game.AddMapEntity(labo);
                            break;
                        case 'W':
                            Water water = new Water(x, y);
                            game.AddMapEntity(water);
                            break;
                        case 'M':
                            Wall wall = new Wall(x, y);
                            game.AddMapEntity(wall);
                            break;
                        case 'B':
                            Bush bush = new Bush(x, y);
                            game.AddMapEntity(bush);
                            break;

                    }
                }
            }
        }

    }

    public class MapEntity : Entity
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
        public House(int x, int y) : base(x, y, 19, 8)
        {
            AddComponent(new Drawable("Assets/House.txt", GetComponent<Position>()));
            AddComponent(new Collider(0, 0, 19, 8));
        }
    }

    class Labo : MapEntity
    {
        public Labo(int x, int y) : base(x, y, 18, 7)
        {
            AddComponent(new Drawable("Assets/Labo.txt", GetComponent<Position>()));
            AddComponent(new Collider(0, 0, 18, 7));
            Door laboDoor = new Door("Labo", x + 7, y + 6, 4, 1);
            Game.GetInstance().AddMapEntity(laboDoor);
        }
    }

    public class Door : MapEntity
    {
        public Door(string path, int x, int y, int w, int h) : base(x, y, w, h)
        {
            Collider collider = new Collider(0, 0, w, h);
            collider.SetSolid(false);
            collider.SetOnCollisionEvent(new MapEvent(path));
            AddComponent(collider);
            //AddComponent(new Drawable(null, GetComponent<Position>()));
            //GetComponent<Drawable>().SetShapeWithString("XXXX");
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

    class Water : MapEntity
    {
        public Water(int x, int y) : base(x, y, 1, 1)
        {
            Random random = new Random();
            if (random.Next(0, 5) == 0) AddComponent(new Drawable("Assets/Wave.txt", GetComponent<Position>()));
            else AddComponent(new Drawable("Assets/Water.txt", GetComponent<Position>()));
            AddComponent(new Collider(0, 0, 1, 1));
        }
    }

    class Bush : MapEntity
    {
        public Bush(int x, int y) : base(x, y, 1, 1)
        {
            AddComponent(new Drawable("Assets/Bush.txt", GetComponent<Position>()));
            Collider collider = new Collider(0, 0, 1, 1);
            collider.SetSolid(false);
            collider.SetOnCollisionEvent(new BushEvent());
            AddComponent(collider);
        }
    }

    class MapEvent : Event
    {
        private string path;

        public MapEvent(string pPath) { path = pPath; }

        public override void Update() { base.Update(); }
        public override void Run()
        {
            base.Run();
            GameManager.GetInstance().SetGameState(path);
        }
    }

    class BushEvent : Event
    {

        public BushEvent() { }

        public override void Update() { base.Update(); }
        public override void Run()
        {
            base.Run();
            GameManager.GetInstance().WalkOnGrass();
        }
    }
}
