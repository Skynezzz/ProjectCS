using Engine;
using Engine.Entities;
using Engine.Entities.Components;

class Pokemons : Entity
{
    Pokemons()
    {

        Drawable component = new Drawable();
        this.AddComponent("Drawable", component);
    }

    ~Pokemons()
    {

    }
}