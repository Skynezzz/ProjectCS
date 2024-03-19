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

        private Dictionary<string, Components.Component> componentsDict = new Dictionary<string, Components.Component>();

        public void AddComponent(string componentName, Components.Component component)
        {
            component.AttatchEntity(this);
            componentsDict.Add(componentName, component);
        }

        public void Update()
        {
            foreach (var component in componentsDict)
            {
                component.Value.Update();
            }
        }

        public Components.Component? GetComponentByName(string name)
        {
            if (componentsDict.ContainsKey(name)) return componentsDict[name];
            return null;
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
