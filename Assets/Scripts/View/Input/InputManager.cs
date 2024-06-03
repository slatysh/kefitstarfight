using Core.DI;
using Logic.Player;
using UnityEngine;
using View.DI;

namespace View.Input
{
    public class InputManager : MonoBaseWithInject
    {
        private IPlayerMoveService _playerMoveService;
        private IPlayerAttackService _playerAttackService;
        private InputSettings _playerInputSettings;

        [Inject]
        public void Construct(IPlayerMoveService playerMoveService, IPlayerAttackService playerAttackService)
        {
            _playerMoveService = playerMoveService;
            _playerAttackService = playerAttackService;

            _playerInputSettings = new InputSettings();
            _playerInputSettings.Player.Move.Enable();
            _playerInputSettings.Player.Fire.Enable();
            _playerInputSettings.Player.FireAlternative.Enable();
            _playerInputSettings.UI.Click.Enable();
        }

        private void Update()
        {
            if (_playerInputSettings == null)
            {
                return;
            }

            var moveVector = _playerInputSettings.Player.Move.ReadValue<Vector2>();
            var attack = _playerInputSettings.Player.Fire.ReadValue<float>();
            var attackAlternative = _playerInputSettings.Player.FireAlternative.ReadValue<float>();

            _playerMoveService.Move(moveVector);
            _playerAttackService.AttackFirst(attack > 0);
            _playerAttackService.AttackAlternative(attackAlternative > 0);
        }
    }
}
