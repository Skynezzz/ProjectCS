using Engine;
using Engine.Entities;
using Engine.Entities.Components;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Sakimon.Entities.player
{
    class Player : Entity
    {
        public const int UP = 0;
        public const int DOWN = 1;
        public const int LEFT = 2;
        public const int RIGHT = 3;
        public Player()
        {
            this.AddComponent(new Drawable(" o \n-O-\n/ \\"));
            AddComponent(new Position());
        }

        ~Player()
        {

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
        private Dictionary<ConsoleKey, int> binds;
        public PlayerBinds()
        {
            binds = new();
            binds.Add(ConsoleKey.Z, Player.UP);
            binds.Add(ConsoleKey.S, Player.DOWN);
            binds.Add(ConsoleKey.Q, Player.LEFT);
            binds.Add(ConsoleKey.D, Player.RIGHT);
        }

        public void Run()
        {

        }
    }
}