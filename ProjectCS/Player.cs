using Engine;
using Engine.Entities;
using Engine.Entities.Components;
using Engine.Utils;
using System.Numerics;
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

        public Player()
        {
            AddComponent(new Drawable(" o \n-O-\n/ \\"));
            AddComponent(new Position());

            var option = Utils.GetDictFromFile("Options.txt");
            eBinds = new PlayerBinds(this);
            Game.AddEvent(eBinds);
        }

        public override void Update() 
        {
            base.Update();
        }

        public void move(int direction)
        {
            Position CPosition = GetComponent<Position>();
            switch (direction)
            {
                case UP:
                    if (Engine.Game.IsEmptyCase((int)CPosition.position.X, (int)CPosition.position.Y + 1)) CPosition.SetPosition(CPosition.position.X, CPosition.position.Y - 1);
                    break;

                case DOWN:
                    if (Engine.Game.IsEmptyCase((int)CPosition.position.X, (int)CPosition.position.Y - 1)) CPosition.SetPosition(CPosition.position.X, CPosition.position.Y + 1);
                    break;

                case LEFT:
                    if (Engine.Game.IsEmptyCase((int)CPosition.position.X - 1, (int)CPosition.position.Y)) CPosition.SetPosition(CPosition.position.X - 1, CPosition.position.Y);
                    break;

                case RIGHT:
                    if (Engine.Game.IsEmptyCase((int)CPosition.position.X + 1, (int)CPosition.position.Y)) CPosition.SetPosition(CPosition.position.X + 1, CPosition.position.Y);
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

            var bindsFromSettings = Utils.GetDictFromFile("Options.txt");

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
                if (Game.inputConsoleKey == key) ownPlayer.move(binds[key]);
            }
        }
    }
}