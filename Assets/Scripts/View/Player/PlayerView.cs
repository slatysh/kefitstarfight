using Core.Utils;
using Logic.Player;
using UnityEngine;

namespace View.Player
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerModel _playerModel;

        public void SetModel(PlayerModel playerModel)
        {
            _playerModel = playerModel;

            _playerModel.OnPositionChange += OnPositionChange;
            _playerModel.OnRotationChange += OnRotationChange;
            _playerModel.OnRemove += OnRemove;
        }

        public void OnDestroy()
        {
            if (_playerModel != null)
            {
                _playerModel.OnPositionChange -= OnPositionChange;
                _playerModel.OnRotationChange -= OnRotationChange;
                _playerModel.OnRemove -= OnRemove;
            }
        }

        private void OnPositionChange(Vector2 newPos)
        {
            transform.position = newPos;
        }

        private void OnRotationChange(float newRotation)
        {
            transform.rotation = Quaternion.Euler(0, 0, newRotation);
        }

        private void OnRemove()
        {
            Destroy(gameObject);
        }
    }
}
