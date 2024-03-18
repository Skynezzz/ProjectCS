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
        private List<char> gameGrid;

        private Dictionary<string, Event> events;
        private List<Entities.Entity> entities;

        public Game()
        {
            gameSize = new Vector2(120, 30);

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
            foreach (var iEntity in entities)
            {
                if (iEntity != null)
                {
                    iEntity.GetShape();
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
    }

    // EVENTS MANAGEMENT //

    public class Event
    {
        public virtual void Update() { }
    }
}