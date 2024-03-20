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
        private bool running;

        private Vector2 gameSize;
        private List<List<char>> gameGrid;

        int frameDelay;
        Stopwatch gameTimer;

        private Dictionary<string, Event> events;
        private List<Entities.Entity> entities;

        public Game()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                // Code spécifique à Windows
                Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
                Console.SetWindowPosition(0, 0);
            }

            running = true;

            gameSize = new Vector2(Console.LargestWindowWidth, Console.LargestWindowHeight);
            gameGrid = new List<List<char>>();

            frameDelay = 1000;
            gameTimer = new();

            Console.WriteLine(gameSize.ToString());

            events = new Dictionary<string, Event>();
            entities = new List<Entities.Entity>();
        }

        // GAME RUN //

        public void Run()
        {
            while (running)
            {
                gameTimer.Restart();
                Event();
                Update();
                Display();
                FpsManager();
            }
        }

        private void Event()
        {
            foreach (var iEvent in events)
            {
                iEvent.Value.Update();
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
            Console.Clear();
            foreach (var iEntity in entities)
            {
                if (iEntity != null)
                {
                    Draw(iEntity);
                }
            }
            Render();
        }

        private void Render()
        {
            foreach(var row in gameGrid)
            {
                Console.WriteLine($"{row.ToString()}");
            }
        }

        private void FpsManager()
        {
            gameTimer.Stop();
            TimeSpan elapsedTime = gameTimer.Elapsed;
            int remainingTime = frameDelay - (int)elapsedTime.TotalMilliseconds;

            if (remainingTime > 0)
            {
                Thread.Sleep(remainingTime);
            }
        }
        // ENTITIES MANAGEMENT //

        public void AddEntity(Entities.Entity entity)
        {
            entities.Add(entity); 
        }

        public void ClearEntities()
        {
            entities.Clear();
        }

        private void Draw(Entity entity)
        {
            if (entity == null) return;
            Drawable? drawable = entity.GetComponent<Drawable>();
            if (drawable == null) return;
            string shape = drawable.GetShape();
            shape.Split('\n');

            Position? position = entity.GetComponent<Position>();

            //gameGrid[]
        }
    }

    // EVENTS MANAGEMENT //

    public class Event
    {
        public virtual void Update() { }
    }
}