using UnityEngine;

namespace Logic.Player
{
    [CreateAssetMenu(fileName = "PlayerAttackSettings", menuName = "Settings/PlayerAttackSettings", order = 1)]
    public class PlayerAttackSettings : ScriptableObject
    {
        public float AttackFirstCdTimeout = 0.5f;
        public float AttackFirstOffset = 0.8f;
        public float AttackAdditionalCdTimeout = 10.0f;
        public float AttackAdditionalOffset = 4.0f;
        public float AttackAdditionalCountMax = 3.0f;
        public float AttackAdditionalCountTimeout = 3.0f;
    }
}
