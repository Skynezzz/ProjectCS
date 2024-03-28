using Engine;
using Engine.Entities;
using Engine.Entities.Components;
using Engine.Utils;

namespace Sakimon.Entities
{

    class PnjEntity : Entity
    {
        public PnjEntity(int x, int y)
        {
            AddComponent(new Position(x, y));
            AddComponent(new Drawable("Assets/Pnj.txt", GetComponent<Position>()));
            AddComponent(new Collider(0, 0, 3, 3));
            Dialogue dialogue = new Dialogue(text, x, y);
        }
    }

    class Dialogue : Entity
    {
        public Dialogue(string text, int x, int y)
        {
            AddComponent(new Position(x, y));
            AddComponent(new Drawable("Assets/DialogueBox.txt", GetComponent<Position>()));
            Drawable drawable = new Drawable();
            drawable.SetShapeWithString(text);
            AddComponent(drawable);
        }

    }

}