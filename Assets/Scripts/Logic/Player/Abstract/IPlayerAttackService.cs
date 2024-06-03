using System;

namespace Logic.Player
{
    public interface IPlayerAttackService
    {
        void AttackFirst(bool attack);
        void AttackAlternative(bool attack);
    }
}
