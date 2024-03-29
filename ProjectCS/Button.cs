using Engine;
using Engine.Entities;
using Engine.Entities.Components;
using Engine.Utils;
using Sakimon.Entities.Map;
using System.Reflection.Metadata.Ecma335;

namespace Sakimon.Entities
{

    public class Button : Entity
    {
        private Drawable drawable;
        private string text;
        public bool hover = false;

        public Button(string pText, int x, int y, int w, int h) : base()
        {
            text = pText;

            Position position = new Position(x, y, w, h);
            AddComponent(position);
            drawable = new Drawable("", position);
            drawable.SetShapeWithString(text, ConsoleColor.Black, ConsoleColor.White);
            AddComponent(drawable);
        }

        public override void Update()
        {
            base.Update();

            if (hover) drawable.SetShapeWithString(text, ConsoleColor.White, ConsoleColor.Black);
            else drawable.SetShapeWithString(text, ConsoleColor.Black, ConsoleColor.White);
            drawable.Draw();
        }

        public void SetEvent(Event pEvent)
        {
            Position position = GetComponent<Position>();
            Collider collider = new Collider(0, 0, (int)position.size.X, (int)position.size.Y);
            collider.SetOnCollisionEvent(pEvent);
            AddComponent(collider);
        }

        public void Hover()
        {
            drawable.SetShapeWithString(text, ConsoleColor.White, ConsoleColor.Black);
        }
        public void UnHover()
        {
            drawable.SetShapeWithString(text, ConsoleColor.Black, ConsoleColor.White);
        }
    }
    
    public class StartEvent : Event
    {
        Button button;
        public StartEvent(Button pButton) : base() { button = pButton; }

        public override void Update()
        {
            button.Update();

            if (button.hover && Game.GetInstance().inputConsoleKey == (ConsoleKey)Player.INTERACT) GameManager.GetInstance().SetGameState(Utils.GetDictFromFile("Data/Save.txt")["gameState"][0]);
        }

        public override void Run()
        {
            base.Run();
        }
    }
    
    public class ExitEvent : Event
    {
        Button button;

        public ExitEvent(Button pButton) : base() { button = pButton; }

        public override void Update()
        {
            button.Update();

            if (button.hover && Game.GetInstance().inputConsoleKey == (ConsoleKey)Player.INTERACT) Game.GetInstance().running = false;
        }

        public override void Run()
        {
            base.Run();
        }
    }
}