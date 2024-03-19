using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Entities
{
    public class Entity
    {

        private List<Components.Component> componentsList;

        public Entity()
        {
            componentsList = new List<Components.Component>();
        }

        public void AddComponent(Components.Component component)
        {
            component.AttatchEntity(this);
            componentsList.Add(component);
        }

        public void Update()
        {
            foreach (var component in componentsList)
            {
                component.Update();
            }
        }

        public T? GetComponent<T>() where T : Components.Component
        {
            return componentsList.FirstOrDefault(c => c.GetType() == typeof(T)) as T;
        }
    }

    namespace Components
    {
        public class Component
        {
            private Entity? entity;

            public Component() { }

            public void AttatchEntity(Entity pEntity)
            {
                entity = pEntity;
            }

            public virtual void Update() { }
        }

        public class Drawable : Component
        {
            private string shape;

            public Drawable(string pShape = "") : base()
            {
                shape = pShape;
            }

            public void SetShape(string pShape)
            {
                shape = pShape;
            }

            public string GetShape()
            {
                return shape;
            }
        }

        public class Position : Component
        {
            private Vector2 _position { get; set; }

            public Vector2 position
            {
                get { return _position; }
                set { _position = value; }
            }

        public Position()
            {
                position = new Vector2(0, 0);
            }
        }
    }
}
