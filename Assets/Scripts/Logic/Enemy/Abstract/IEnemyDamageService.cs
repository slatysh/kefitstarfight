using System;

namespace Logic.Enemy
{
    public interface IEnemyDamageService
    {
        void Damage(EnemyModel enemyModel, int damage, bool isAddScore = false);
    }
}
