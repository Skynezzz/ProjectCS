using Engine.Entities;
using Engine.Entities.Components;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Engine
{
    public struct GridCase
    {
        public char value;
        public ConsoleColor fgColor;
        public ConsoleColor? bgColor;
    }
    public class Game
    {
        private static Game gameInstance;

        // Variables privées
        private bool running;
        private GridCase[,] gameGrid;
        private List<Event> events;
        public List<Entities.Entity> allEntities {  get; private set; }
        private List<Entities.Entity> entities;
        private List<Entities.Entity> mapEntities;
        private Entity? cameraFocusOn;

        // Variables publiques
        public Vector2 gameSize;
        public ConsoleKey? inputConsoleKey {  get; private set; }


        private Game()
        {
            gameSize = new Vector2(201, 52);

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                // Code spécifique à Windows
                Console.SetWindowSize((int)gameSize.X, (int)gameSize.Y);
                Console.SetWindowPosition(0, 0);
            }
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;

            running = true;

            gameGrid = new GridCase[(int)gameSize.Y, (int)gameSize.X];
            ClearGrid();

            inputConsoleKey = null;
            events = new List<Event>();
            allEntities = new List<Entities.Entity>();
            entities = new List<Entities.Entity>();
            mapEntities = new List<Entities.Entity>();
        }

        // STATIC //

        public static Game GetInstance()
        {
            if (gameInstance == null) gameInstance = new Game();
            return gameInstance;
        }

        // GAME RUN //

        public void Run()
        {
            DisplayMap();
            Display();
            while (running)
            {
                Event();
                Update();
                inputConsoleKey = Console.ReadKey(true).Key;
            }
        }

        // EVENTS MANAGEMENT //

        private void Event()
        {
            foreach (var iEvent in events)
            {
                iEvent.Update();
            }
        }

        public void AddEvent(Event pEvent)
        {
            events.Add(pEvent);
        }

        public void ClearEvents()
        {
            events.Clear();
        }

        // ENTITIES MANAGEMENT //

        private void Update()
        {
            foreach (var iEntity in entities)
            {
                if (iEntity != null)
                {
                    iEntity.Update();
                }
            }
        }

        public void AddAllEntity(Entities.Entity entity)
        {
            allEntities.Add(entity);
        }
        
        public void ClearAllEntities()
        {
            allEntities.Clear();
        }
        
        public void AddEntity(Entities.Entity entity)
        {
            entities.Add(entity);
        }
        
        public void ClearEntities()
        {
            entities.Clear();
        }
        
        public void AddMapEntity(Entities.Entity entity)
        {
            mapEntities.Add(entity);
            Drawable? drawable = entity.GetComponent<Drawable>();
            if (drawable != null) DrawOnGameGrid(drawable.GetShapePosition(), drawable.GetShape());
        }

        public void ClearMapEntities()
        {
            mapEntities.Clear();
        }

        // DISPLAY //

        private void DisplayMap()
        {
            for (var i = 0; i < gameGrid.GetLength(0); i++)
            {
                for (var j = 0; j < gameGrid.GetLength(1); j++)
                {
                    drawGridCase(gameGrid[i, j], j, i);
                }
            }
        }

        private void Display()
        {
            foreach (var iEntity in entities)
            {
                if (iEntity != null)
                {
                    iEntity.Draw();
                }
            }
        }

        private void ClearGrid()
        {
            Random rand = new();
            for (int i = 0; i < (int)gameSize.Y; i++)
            {
                for (int j = 0; j < (int)gameSize.X; j++)
                {
                    gameGrid[i, j].value = ' ';
                    gameGrid[i, j].fgColor = ConsoleColor.Gray;
                    gameGrid[i, j].bgColor = ConsoleColor.Gray;
                }
            }
        }

        private void DrawOnGameGrid(Vector2 position, GridCase[,] shape)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    Vector2 pos = new Vector2(position.X + j, position.Y + i);
                    if (pos.X >= 0 && pos.X < gameSize.X && pos.Y >= 0 && pos.Y < gameSize.Y)
                    {
                        if (shape[i, j].value != ' ')
                        {
                            if (shape[i, j].bgColor == null) shape[i, j].bgColor = gameGrid[(int)pos.Y, (int)pos.X].bgColor;
                            gameGrid[(int)pos.Y, (int)pos.X] = shape[i, j];
                        }
                    }
                }
            }
        }

        public void drawBack(Vector2 position, GridCase[,] shape)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    Vector2 pos = new Vector2(position.X + j, position.Y + i);
                    if ((int)pos.X >= 0 && (int)pos.X < gameSize.X && (int)pos.Y >= 0 && (int)pos.Y < gameSize.Y) drawGridCase(gameGrid[(int)pos.Y, (int)pos.X], (int)pos.X, (int)pos.Y);
                }
            }
        }

        public void drawGridCase(GridCase gridCase, int x, int y)
        {
            if (x >= 0 && x < gameSize.X && y >= 0 && y < gameSize.Y)
            {
                Console.SetCursorPosition((int)x, (int)y);
                Console.BackgroundColor = (ConsoleColor)gameGrid[y, x].bgColor;

                if (gridCase.value == ' ')
                {
                    if (gameGrid[y, x].bgColor != null) Console.BackgroundColor = (ConsoleColor)gameGrid[y, x].bgColor;
                    Console.ForegroundColor = (ConsoleColor)gameGrid[y, x].fgColor;
                    Console.Write(gameGrid[y, x].value);
                }
                else
                {
                    if (gridCase.bgColor != null) Console.BackgroundColor = (ConsoleColor)gridCase.bgColor;
                    Console.ForegroundColor = gridCase.fgColor;
                    Console.Write(gridCase.value);
                }

            }
        }

        // UTILS //

        public void SetEntityCameraFocusOn(Entity entity)
        {
            cameraFocusOn = entity;
        }

        public void ReplaceCursorPosition()
        {
            // Pas de focus
            if (cameraFocusOn == null)
            {
                Console.SetCursorPosition(0, 0);
                return;
            }
            // Load du composant
            Position? position = cameraFocusOn.GetComponent<Position>();
            Vector2 entityPosition = new(0, 0);
            if (position != null) entityPosition = position.GetPosition();

            Vector2 cursorPosition = entityPosition;

            if(cursorPosition.X < 0) cursorPosition.X = 0;
            if(cursorPosition.Y < 0) cursorPosition.Y = 0;
            if(cursorPosition.X > gameSize.X) cursorPosition.X = gameSize.X;
            if(cursorPosition.Y > gameSize.Y) cursorPosition.Y = gameSize.Y;

            Console.SetCursorPosition((int)cursorPosition.X, (int)cursorPosition.Y);
        }

    }

    // EVENTS MANAGEMENT //

    public class Event
    {
        public Event() { }

        public virtual void Update() { }
    }

}