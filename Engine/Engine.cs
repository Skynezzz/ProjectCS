using Engine.Entities;
using Engine.Entities.Components;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Engine
{
    public class Game
    {
        private Vector2 gameSize;
        private List<List<char>> gameGrid;

        private Dictionary<string, Event> events;
        private List<Entities.Entity> entities;

        public Game()
        {

            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.CursorVisible = false;
            Console.SetWindowPosition(0, 0);

            gameSize = new Vector2(Console.LargestWindowWidth, Console.LargestWindowHeight);

            Console.WriteLine(gameSize.ToString());

            events = new Dictionary<string, Event>();
            entities = new List<Entities.Entity>();
        }

        // GAME RUN //

        public void Run()
        {
            Event();
            Update();
            Display();
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

            gameGrid[]
        }
    }

    // EVENTS MANAGEMENT //

    public class Event
    {
        public virtual void Update() { }
    }
}