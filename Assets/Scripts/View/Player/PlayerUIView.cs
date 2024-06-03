using Logic.Player;
using UnityEngine;

namespace View.Player
{
    public class PlayerUIView : MonoBehaviour
    {
        private PlayerModel _playerModel;

        public void SetModel(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }
    }
}
