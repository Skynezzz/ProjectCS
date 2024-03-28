using Engine;
using Engine.Entities;
using Engine.Entities.Components;
using Engine.Utils;
using Sakimon.Entities.Map;

namespace Sakimon.Entities
{

    public class PnjEntity : Entity
    {
        public List<Dialogue> dialogues;

        public PnjEntity(int x, int y)
        {
            AddComponent(new Position(x, y));
            AddComponent(new Drawable("Assets/Pnj.txt", GetComponent<Position>()));
            AddComponent(new Collider(0, 0, 3, 3));

        }

        protected List<Dialogue> GetDialoguesFromFile(string path)
        {
            List<Dialogue> dialogues = new List<Dialogue>();
            string[] dialoguetxt = Utils.GetTextFromFile("Dialogues/MarieClaire.txt").Split('\n');
            foreach (string dialogue in dialoguetxt)
            {
                dialogues.Add(new Dialogue(dialogue));
            }
            return dialogues;
        }
    }

    public class Dialogue : Entity
    {
        public Dialogue(string text)
        {
            AddComponent(new Position(100 - (text.Split('\n')[0].Length / 2), 50 - text.Split('\n').Length));
            Drawable drawable = new Drawable("", GetComponent<Position>());
            drawable.SetShapeWithString(text);
            AddComponent(drawable);
        }
    }

    public class MarieClaire : PnjEntity
    {

        public MarieClaire(int x, int y) : base(x, y)
        {
            dialogues = GetDialoguesFromFile("Dialogues/MarieClaire.txt");
            Interact interact = new Interact(this, x, y, 3, 3);
            Game.GetInstance().AddMapEntity(interact);
        }
    }

    public class Interact : MapEntity
    {
        public Interact(PnjEntity pnj, int x, int y, int w, int h) : base(x, y, w, h)
        {
            Collider collider = new Collider(0, 0, w, h);
            collider.SetSolid(false);
            PnjEvent evt = new PnjEvent(pnj);
            Game.GetInstance().AddEvent(evt);
            collider.SetOnCollisionEvent(evt);
            AddComponent(collider);
            //AddComponent(new Drawable(null, GetComponent<Position>()));
            //GetComponent<Drawable>().SetShapeWithString("XXXX");
        }
    }

    public class PnjEvent : Event
    {
        private PnjEntity pnj;
        private int dialogueIndex;
        public bool talking = false;

        public PnjEvent(PnjEntity pPnj) { pnj = pPnj; }

        public override void Run()
        {
            base.Run();
            talking = true;
        }
        public override void Update()
        {
            base.Update();
            if (!talking) return;
            dialogueIndex++;
            if (dialogueIndex >= pnj.dialogues.Count) Game.GetInstance().SetDialogue(null);
            else Game.GetInstance().SetDialogue(pnj.dialogues[dialogueIndex]);
        }
    }
}