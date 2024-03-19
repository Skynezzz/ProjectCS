namespace Attacks
{
    public class Attack
    {
        char attackName;
        int attackType;
        int attackPp;
        int attackDmg;

        Attack(char pAttackName, int pAttackType, int pAttackPp, int pAttackDmg)
        {
            attackName = pAttackName;
            attackType = pAttackType;
            attackPp = pAttackPp;
            attackDmg = pAttackDmg;
        }
    }
}