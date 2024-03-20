using System;

class Map
{
    public void DrawMap()
    {
        // D�finir les caract�res pour les diff�rents �l�ments de la carte
        char playerChar = '@';
        char wallChar = ' ';
        char groundChar = ' ';

        // D�finir les couleurs pour les diff�rents �l�ments de la carte
        ConsoleColor playerColor = ConsoleColor.Red;
        ConsoleColor wallColor = ConsoleColor.Gray;
        ConsoleColor groundColor = ConsoleColor.Green;

        // D�finir la taille de la carte
        int mapWidth = Console.LargestWindowWidth;
        int mapHeight = Console.LargestWindowHeight;

        // Dessiner la carte
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                // V�rifier les coordonn�es pour dessiner le joueur, les murs et le sol
                if (x == 1 && y == 1)
                {
                    Console.ForegroundColor = playerColor;
                    Console.Write(playerChar);
                }
                else if (x == 0 || x == mapWidth - 1 || y == 0 || y == mapHeight - 1)
                {
                    Console.BackgroundColor = wallColor;
                    Console.Write(wallChar);
                }
                else
                {
                    Console.BackgroundColor = groundColor;
                    Console.Write(groundChar);
                }
            }
            // Passer � la ligne suivante pour dessiner la prochaine rang�e de la carte
            Console.WriteLine();
        }

        Console.ResetColor(); // R�initialiser la couleur de la console
    }
}