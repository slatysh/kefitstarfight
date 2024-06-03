using UnityEngine;

namespace View.DI
{
    public class MonoBaseWithInject : MonoBehaviour
    {
        public virtual void Awake()
        {
            DIManager.Container.InjectDependencies(this);
        }
    }
}
