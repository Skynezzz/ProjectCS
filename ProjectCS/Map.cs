using System;

class Map
{
    public void DrawMap()
    {
        // Définir les caractères pour les différents éléments de la carte
        char playerChar = '@';
        char wallChar = ' ';
        char groundChar = ' ';

        // Définir les couleurs pour les différents éléments de la carte
        ConsoleColor playerColor = ConsoleColor.Red;
        ConsoleColor wallColor = ConsoleColor.Gray;
        ConsoleColor groundColor = ConsoleColor.Green;

        // Définir la taille de la carte
        int mapWidth = Console.LargestWindowWidth;
        int mapHeight = Console.LargestWindowHeight;

        // Dessiner la carte
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                // Vérifier les coordonnées pour dessiner le joueur, les murs et le sol
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
            // Passer à la ligne suivante pour dessiner la prochaine rangée de la carte
            Console.WriteLine();
        }

        Console.ResetColor(); // Réinitialiser la couleur de la console
    }
}