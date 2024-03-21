namespace Sakimon.Entities.Attacks
{
    public readonly struct Attack
    {
        public string attackName { get; }
        public int attackType { get; }
        public int attackPp { get; }
        public int attackDmg { get; }

        public Attack(string pAttackName, int pAttackType, int pAttackPp, int pAttackDmg)
        {
            attackName = pAttackName;
            attackType = pAttackType;
            attackPp = pAttackPp;
            attackDmg = pAttackDmg;
        }
    }
}