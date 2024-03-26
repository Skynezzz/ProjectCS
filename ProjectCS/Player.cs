using Engine;
using Engine.Entities;
using Engine.Entities.Components;
using Engine.Utils;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Sakimon.Entities.Player
{
    class Player : Entity
    {
        PlayerBinds eBinds;
        public const int UP = 0;
        public const int DOWN = 1;
        public const int LEFT = 2;
        public const int RIGHT = 3;
        public const int INTERACT = 4;

        public Player(int x = 0, int y = 0)
        {
            AddComponent(new Position(x, y));
            AddComponent(new Drawable("Assets/Player.txt", GetComponent<Position>()));
            AddComponent(new Collider(new Vector2(0), new Vector2(3)));

            var option = Utils.GetDictFromFile("Data/Options.txt");
            eBinds = new PlayerBinds(this);
            Game.GetInstance().AddEvent(eBinds);
        }

        public override void Update() 
        {
            base.Update();
        }

        public void move(int direction)
        {
            Collider cCollider = GetComponent<Collider>();
            Position cPosition = GetComponent<Position>();
            Vector2 position = cPosition.GetPosition();
            switch (direction)
            {
                case UP:
                    if (cCollider.IsCollidingOn((int)position.X, (int)position.Y - 1) == false) cPosition.SetPosition(position.X, position.Y - 1);
                    break;

                case DOWN:
                    if (cCollider.IsCollidingOn((int)position.X, (int)position.Y + 1) == false) cPosition.SetPosition(position.X, position.Y + 1);
                    break;

                case LEFT:
                    if (cCollider.IsCollidingOn((int)position.X - 1, (int)position.Y) == false) cPosition.SetPosition(position.X - 1, position.Y);
                    break;

                case RIGHT:
                    if (cCollider.IsCollidingOn((int)position.X + 1, (int)position.Y) == false) cPosition.SetPosition(position.X + 1, position.Y);
                    break;
            }
        }
    }

    class PlayerBinds : Event
    {
        Player ownPlayer;
        private Dictionary<ConsoleKey, int> binds;

        public PlayerBinds(Player pOwnPlayer) : base()
        {
            ownPlayer = pOwnPlayer;

            var bindsFromSettings = Utils.GetDictFromFile("Data/Options.txt");

            binds = new();
            binds.Add((ConsoleKey)int.Parse(bindsFromSettings["up"][0]), Player.UP);
            binds.Add((ConsoleKey)int.Parse(bindsFromSettings["down"][0]), Player.DOWN);
            binds.Add((ConsoleKey)int.Parse(bindsFromSettings["left"][0]), Player.LEFT);
            binds.Add((ConsoleKey)int.Parse(bindsFromSettings["right"][0]), Player.RIGHT);
        }

        public override void Update()
        {
            foreach (ConsoleKey key in binds.Keys)
            {
                if (Game.GetInstance().inputConsoleKey == key) ownPlayer.move(binds[key]);
            }
        }

    }

    class Interact : Event
    {
        ConsoleKey bind;
        public Interact() : base()
        {
            var bindsFromSettings = Utils.GetDictFromFile("Data/Options.txt");

            bind = (ConsoleKey)int.Parse(bindsFromSettings["interact"][0]);
        }
        public override void Update()
        {
            if (Game.GetInstance().inputConsoleKey == bind)
            {

            }
        }
    }
}