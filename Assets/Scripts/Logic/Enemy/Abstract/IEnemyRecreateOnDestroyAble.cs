namespace Logic.Enemy
{
    public interface IEnemyRecreateOnDestroyAble
    {
        (EnemyType, int, float) GetSpawnInfo();
    }
}
