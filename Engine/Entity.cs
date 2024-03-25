using Engine.Entities.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

            Game.GetInstance().AddAllEntity(this);
        }

        public void AddComponent(Components.Component component)
        {
            component.AttatchEntity(this);
            componentsList.Add(component);
        }

        public virtual void Update() { }

        public void Draw()
        {
            Drawable? drawable = GetComponent<Drawable>();
            if (drawable == null) return;
            drawable.Draw();
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
            protected Entity? entity;

            public Component() { }

            public void AttatchEntity(Entity pEntity)
            {
                entity = pEntity;
            }

            public virtual void Update() { }
        }

        public class Drawable : Component
        {
            private GridCase[,] shape;
            private Vector2 shapePosition;

            public Drawable(string? path = null, Position? position = null) : base()
            {
                if (position != null) shapePosition = position.GetPosition();
                shape = Utils.Utils.GetSpriteFromFile(path);
                if (shape == null) shape = new GridCase[0, 0];
            }

            public GridCase[,] GetShape()
            {
                return shape;
            }

            public void SetShape(string path)
            {
                shape = Utils.Utils.GetSpriteFromFile(path);
                if (shape == null) shape = new GridCase[0, 0];
            }

            public Vector2 GetShapePosition()
            {
                return shapePosition;
            }

            public void Draw()
            {
                Vector2 gameSize = Game.GetInstance().gameSize;
                for (int i = 0; i < shape.GetLength(0); i++)
                {
                    for (int j = 0; j < shape.GetLength(1); j++)
                    {
                        Vector2 pos = new Vector2(j + (int)shapePosition.X, i + (int)shapePosition.Y);
                        Game.GetInstance().drawGridCase(shape[i, j], (int)pos.X, (int)pos.Y);
                    }
                }
            }

            public Vector2 DrawAtNewPos(Vector2 position)
            {
                shapePosition = position;
                Draw();
                Game.GetInstance().ReplaceCursorPosition();
                return shapePosition;
            }
        }

        public class Position : Component
        {
            private Vector2 position;
            
            public Vector2 size;

            public Position(float posX = 0.0f, float posY = 0.0f, float sizeX = 0.0f, float sizeY = 0.0f)
            {
                position = new Vector2(posX, posY);
                size = new Vector2(sizeX, sizeY);
            }
            
            public Position(Vector2 pPos, Vector2 pSize)
            {
                position = pPos;
                size = pSize;
            }

            public Vector2 GetPosition()
            {
                return position;
            }

            public void SetPosition(float x, float y)
            {
                Drawable? drawable = entity.GetComponent<Drawable>();
                if (drawable != null)
                {
                    Game.GetInstance().drawBack(position, drawable.GetShape());
                    Vector2 shapePosition = drawable.DrawAtNewPos(new Vector2(x, y));
                    position = shapePosition;
                }
                else { position = new Vector2(x, y); }
            }
        }

        public class AliveEntity : Component
        {
            private int maxHealth;
            private float health;

            public AliveEntity(int pMaxHealth = 0) : base()
            {
                maxHealth = pMaxHealth;
                health = maxHealth;
            }

            public void TakeDamage(int damage)
            {
                health -= damage;
            }

            public int GetMaxHealth() { return maxHealth; }
            public float GetHealth() { return health; }
        }

        public class Collider : Component
        {
            private Vector2 relativePosition;
            private Vector2 size;

            public Collider(int relativePosX = 0, int relativePosY = 0, int sizeX = 0, int sizeY = 0)
            {
                relativePosition = new Vector2(relativePosX, relativePosY);
                size = new Vector2 (sizeX, sizeY);
            }
            
            public Collider(Vector2 pRelativePos, Vector2 pSize)
            {
                relativePosition = pRelativePos;
                size = pSize;
            }

            public Vector2 GetRelativePosition() {  return relativePosition; }

            public Vector2 GetSize() { return size; }


            private float Square(float x)
            {
                return x * x;
            }

            private float GetVectDist(Vector2 vect)
            {
                return (float)Math.Sqrt(Square(vect.X) + Square(vect.Y));
            }
            private bool IsCollidingOneD(Vector2 vect, float point)
            {
                return vect.X <= point && vect.Y <= point;
            }

            private bool IsCollidingTwoD(Vector2 vectOne, Vector2 vectTwo)
            {
                if (GetVectDist(vectTwo) > GetVectDist(vectOne))
                {
                    return IsCollidingTwoD(vectTwo, vectOne);
                }
                return IsCollidingOneD(vectOne, vectTwo.X) || IsCollidingOneD(vectOne, vectTwo.Y);
            }

            private bool IsCollidingTwoRect(Vector2 rectOneX, Vector2 rectOneY, Vector2 rectTwoX, Vector2 rectTwoY)
            {
                return IsCollidingTwoD(rectOneX, rectTwoX) && IsCollidingTwoD(rectOneY, rectTwoY);
            }

            public bool IsCollidingOn(int posX, int posY)
            {
                Vector2 position = new Vector2(posX, posY);

                Vector2 ownVectX = new Vector2(position.X + relativePosition.X, position.X + relativePosition.X + size.X);
                Vector2 ownVectY = new Vector2(position.Y + relativePosition.Y, position.Y + relativePosition.Y + size.Y);

                foreach (var other in Game.GetInstance().allEntities)
                {
                    Collider? otherCollider = other.GetComponent<Collider>();
                    if (otherCollider == null) continue;
                    if (otherCollider == this) continue;

                    Position? otherPosition = other.GetComponent<Position>();
                    if (otherPosition == null) continue;

                    if (IsCollidingTwoRect
                        (
                        ownVectX,
                        ownVectY,
                        new Vector2(otherPosition.GetPosition().X + otherCollider.GetRelativePosition().X, otherPosition.GetPosition().X + otherCollider.GetRelativePosition().X + otherCollider.GetSize().X),
                        new Vector2(otherPosition.GetPosition().Y + otherCollider.GetRelativePosition().Y, otherPosition.GetPosition().Y + otherCollider.GetRelativePosition().Y + otherCollider.GetSize().Y)
                        )) return true;
                }
                return false;
            }
        }
    }
}
