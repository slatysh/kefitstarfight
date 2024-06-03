using Logic.Bullet;
using UnityEngine;
using View.Common;

namespace View.Bullet
{
    public class BulletView : MonoBehaviour
    {
        private BulletModel _bulletModel;
        private PoolBase<BulletView> _pool;

        public void SetModel(BulletModel bulletModel, PoolBase<BulletView> pool)
        {
            _bulletModel = bulletModel;
            _pool = pool;

            bulletModel.OnPositionChange += OnPositionChange;
            bulletModel.OnRemove += OnRemove;
        }

        public void OnDestroy()
        {
            if (_bulletModel != null)
            {
                _bulletModel.OnPositionChange -= OnPositionChange;
                _bulletModel.OnRemove -= OnRemove;
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
