using Engine;
using Engine.Entities;
using Engine.Entities.Components;

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();

        game.Run();
        Entity entity = new Entity();
        Drawable component = new Drawable();
    }
}