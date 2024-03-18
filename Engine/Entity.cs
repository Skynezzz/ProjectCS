using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Entities
{
    public class Entity
    {

        private List<Components.Component> componentsList = new List<Components.Component>();

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
    }

    namespace Components
    {
        public class Component
        {
            private Entity? entity;

            public Component()
            {
                entity = null;
            }

            public void AttatchEntity(Entity pEntity)
            {
                entity = pEntity;
            }

            public virtual void Update() { }
        }

        public class Drawable
        {
            private string shape;

            public Drawable(string pShape = "")
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
    }
}
