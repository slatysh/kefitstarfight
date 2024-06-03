using Logic.Enemy;
using UnityEngine;
using View.Common;

namespace View.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private EnemyModel _enemyModel;
        private PoolBase<EnemyView> _pool;

        public void SetModel(EnemyModel enemyModel, PoolBase<EnemyView> pool)
        {
            _enemyModel = enemyModel;
            _pool = pool;

            _enemyModel.OnPositionChange += OnPositionChange;
            _enemyModel.OnRemove += OnRemove;
        }

        public void OnDestroy()
        {
            if (_enemyModel != null)
            {
                _enemyModel.OnPositionChange -= OnPositionChange;
                _enemyModel.OnRemove -= OnRemove;
            }
        }

        private void OnPositionChange(Vector2 newPos)
        {
            transform.position = newPos;
        }

        private void OnRemove()
        {
            _pool.Release(this);
        }
    }
}
