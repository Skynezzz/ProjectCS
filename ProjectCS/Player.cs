using Engine;
using Engine.Entities;
using Engine.Entities.Components;
using Engine.Utils;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Sakimon.Entities
{
    class Player : Entity
    {
        PlayerBinds eBinds;
        public const int UP = 0;
        public const int DOWN = 1;
        public const int LEFT = 2;
        public const int RIGHT = 3;
        public const int INTERACT = 4;

        public Player(int x, int y)
        {
            AddComponent(new Position(x, y));

            var option = Utils.GetDictFromFile("Data/Options.txt");
            eBinds = new PlayerBinds(this);
            Game.GetInstance().AddEvent(eBinds);
        }

        public override void Update() 
        {
            base.Update();
        }

        public virtual void Move(int direction)
        {
        }

        public void Interact()
        {

        }
    }

    class PlayerEntity : Player
    {
        public PlayerEntity(int x = 0, int y = 0) : base(x, y)
        {
            AddComponent(new Drawable("Assets/Player.txt", GetComponent<Position>()));
            AddComponent(new Collider(new Vector2(0, 2), new Vector2(3, 1)));
        }

        public override void Move(int direction)
        {
            base.Move(direction);

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

    class PlayerCursor : Player
    {
        Entity? selectedButton;

        public PlayerCursor(int x = 0, int y = 0) : base(x, y)
        {
            AddComponent(new Collider(new Vector2(0), new Vector2(1)));
        }

        public override void Move(int direction)
        {
            base.Move(direction);

            Game game = Game.GetInstance();

            Collider cCollider = GetComponent<Collider>();
            Position cPosition = GetComponent<Position>();
            Vector2 position = cPosition.GetPosition();
            Vector2 oldPosition = cPosition.GetPosition();
            switch (direction)
            {
                case UP:

                    while (cCollider.IsCollidingOn((int)position.X, (int)position.Y) == false)
                    {
                        position.Y -= 1;
                        if (position.X >= 0 && position.X < game.gameSize.X && position.Y >= 0 && position.Y < game.gameSize.Y)
                        {
                            break;
                        }
                        cPosition.SetPosition(position.X, position.Y - 1);
                    }

                    while (cCollider.IsCollidingOn((int)position.X, (int)position.Y - 1) == true)
                    {
                        position.Y -= 1;
                        if (position.X >= 0 && position.X < game.gameSize.X && position.Y >= 0 && position.Y < game.gameSize.Y)
                        {
                            cPosition.SetPosition(position.X, position.Y);
                            break;
                        }
                        cPosition.SetPosition(position.X, position.Y - 1);
                    }

                    

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
            selectedButton = cCollider.GetCollideEntity();
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
                if (Game.GetInstance().inputConsoleKey == key) ownPlayer.Move(binds[key]);
            }
        }
    }
}