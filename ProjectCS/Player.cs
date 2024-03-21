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

            string option = Utils.GetTextFromFile("Options.txt");
        }

        public override void Update() 
        {
            base.Update();
            Position CPosition = GetComponent<Position>();
            CPosition.SetPosition(CPosition.position.X + 1, CPosition.position.Y);
        }

        public void move(int direction)
        {
            Position CPosition = GetComponent<Position>();
            switch (direction)
            {
                case UP:
                    if (Engine.Game.IsEmptyCase((int)CPosition.position.X, (int)CPosition.position.Y + 1)) CPosition.SetPosition(CPosition.position.X, CPosition.position.Y + 1);
                    break;

                case DOWN:
                    if (Engine.Game.IsEmptyCase((int)CPosition.position.X, (int)CPosition.position.Y - 1)) CPosition.SetPosition(CPosition.position.X, CPosition.position.Y);
                    break;

                case LEFT:
                    if (Engine.Game.IsEmptyCase((int)CPosition.position.X - 1, (int)CPosition.position.Y)) CPosition.SetPosition(CPosition.position.X, CPosition.position.Y);
                    break;

                case RIGHT:
                    if (Engine.Game.IsEmptyCase((int)CPosition.position.X + 1, (int)CPosition.position.Y)) CPosition.SetPosition(CPosition.position.X, CPosition.position.Y);
                    break;
            }
        }
    }

    class PlayerBinds : Event
    {
        Player ownPlayer;
        private Dictionary<ConsoleKeyInfo, int> binds;

        public PlayerBinds(Player pOwnPlayer, List<ConsoleKeyInfo> bindsFromSettings) : base(bindsFromSettings)
        {
            ownPlayer = pOwnPlayer;

            binds = new();
            binds.Add(bindsFromSettings[0], Player.UP);
            binds.Add(bindsFromSettings[1], Player.DOWN);
            binds.Add(bindsFromSettings[2], Player.LEFT);
            binds.Add(bindsFromSettings[3], Player.RIGHT);
        }

        public override void Update()
        {
            foreach (ConsoleKeyInfo key in binds.Keys)
            {
                if (Game.eventList[key]) ownPlayer.move(binds[key]);
            }
        }
    }
}