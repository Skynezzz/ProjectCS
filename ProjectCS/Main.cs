using Engine;
using Engine.Entities;
using Engine.Entities.Components;
using Attacks;

class Sakimon
{
    List<Attack> attackList;
    Game game;
    private void Init()
    {
        game = new Game();
        InitAttacks();
        Pokemons Pikachu = Pokemon();
    }

    private void InitAttacks()
    {
        attackList.add(new Attack("Vive Attaque", 0, 10, 20));
        attackList.add(new Attack("Queue de Fer", 1, 15, 40));
    }


    static void Main(string[] args)
    {
        Init();
        game.Run();
        Entity entity = new Entity();
        Drawable component = new Drawable();

    }


}