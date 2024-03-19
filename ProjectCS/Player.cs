using Engine;
using Engine.Entities;
using Engine.Entities.Components;

class Player : Entity
{
    Player()
    {
        Drawable component = new Drawable("t");
        this.AddComponent(component);
    }

    ~Player()
    {

    }
}