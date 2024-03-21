using Engine.Entities;
using Engine.Entities.Components;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Engine
{
    public class Game
    {
        private static bool running;
        public static ConsoleKey? inputConsoleKey;
        private static char[,] gameGrid;
        public static Entity map;
        private static List<Event> events;

        private Vector2 gameSize;

        private List<Entities.Entity> entities;

        public Game()
        {
            gameSize = new Vector2(200, 50);

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                // Code spécifique à Windows
                Console.SetWindowSize((int)gameSize.X, (int)gameSize.Y + 1);
                Console.SetWindowPosition(0, 0);
            }
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;

            running = true;

            gameGrid = new char[(int)gameSize.Y, (int)gameSize.X];
            ClearGrid();

            inputConsoleKey = null;
            events = new List<Event>();
            entities = new List<Entities.Entity>();
        }

        // STATIC //

        public static bool IsEmptyCase(int x, int y)
        {
            //return gameGrid[x, y] == ' ';
            return true;
        }

        // GAME RUN //

        public void Run()
        {
            while (running)
            {
                Event();
                Update();
                Display();
                inputConsoleKey = Console.ReadKey(true).Key;
            }
        }

        private void Event()
        {
            foreach (var iEvent in events)
            {
                iEvent.Update();
            }
        }

        private void Update()
        {
            foreach (var iEntity in entities)
            {
                if (iEntity != null)
                {
                    iEntity.Update();
                }
            }
        }

        private void Display()
        {
            ClearGrid();
            foreach (var iEntity in entities)
            {
                if (iEntity != null)
                {
                    Draw(iEntity);
                }
            }
            Render();
        }


        // DISPLAY //

        private void ClearGrid()
        {
            for (int i = 0; i < (int)gameSize.Y; i++)
            {
                for (int j = 0; j < (int)gameSize.X; j++)
                {
                    gameGrid[i, j] = ' ';
                }
            }
        }

        private void Render()
        {
            string stringToDraw = "";
            for (int i = 0; i < (int)gameSize.Y; i++)
            {
                for (int j = 0; j < (int)gameSize.X; j++)
                {
                    stringToDraw += gameGrid[i, j];
                }
            }
            Console.WriteLine(stringToDraw);
            Console.SetCursorPosition(0, 0);
        }

        private void Draw(Entity entity)
        {
            if (entity == null) return;
            Drawable? drawable = entity.GetComponent<Drawable>();
            if (drawable == null) return;
            string shape = drawable.GetShape();
            string[] shapeToDraw = shape.Split('\n');

            Position? position = entity.GetComponent<Position>();
            Vector2 drawPos;
            if (position == null) drawPos = new(0, 0);
            else drawPos = position.position;




            for (int i = 0; i < shapeToDraw.Length; i++)
            {
                for (int j = 0; j < shapeToDraw[i].Length; j++)
                {
                    if ((int)drawPos.Y + i >= 0 && (int)drawPos.Y + i < gameSize.Y && (int)drawPos.X + j >= 0 && (int)drawPos.X + j < gameSize.X)
                    {
                        if (shapeToDraw[i][j] != ' ') gameGrid[(int)drawPos.Y + i, (int)drawPos.X + j] = shapeToDraw[i][j];
                    }
                }
            }
        }

        // ENTITIES MANAGEMENT //

        public static void AddEvent(Event pEvent)
        {
            events.Add(pEvent);
        }

        public void AddEntity(Entities.Entity entity)
        {
            entities.Add(entity);
        }

        public void ClearEntities()
        {
            entities.Clear();
        }
    }

    // EVENTS MANAGEMENT //

    public class Event
    {
        public Event() { }

        public virtual void Update() { }
    }
}