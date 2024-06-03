using UnityEngine;

namespace Logic.Player
{
    [CreateAssetMenu(fileName = "PlayerMoveSettings", menuName = "Settings/PlayerMoveSettings", order = 1)]
    public class PlayerMoveSettings : ScriptableObject
    {
        public float DirectSpeedAcceleration = 0.048f;
        public float DirectSpeedDeceleration = 0.0092f;
        public float DirectSpeedMax = 4.8f;
        public float RotationSpeedAcceleration = 2.4f;
        public float RotationSpeedDeceleration = 0.48f;
        public float RotationSpeedMax = 120f;
    }
}
