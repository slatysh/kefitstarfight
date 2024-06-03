using UnityEngine;

namespace Logic.Enemy
{
    public class EnemyPlateModel : EnemyModel
    {
        public override EnemyType EnemyType => EnemyType.Plate;

        public EnemyPlateModel(Vector2 position, float speed, float rotation) : base(position, speed, rotation)
        {
        }
    }
}
