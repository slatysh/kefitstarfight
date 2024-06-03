using UnityEngine;

namespace Logic.Bullet
{
    [CreateAssetMenu(fileName = "BulletSettings", menuName = "Settings/BulletSettings", order = 1)]
    public class BulletSettings : ScriptableObject
    {
        public float Speed = 0.02f;
        public float Lifetimer = 2.0f;
    }
}
