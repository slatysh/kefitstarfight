using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace View.Common
{
    public class PoolBase<T> where T: MonoBehaviour
    {
        private T _prefab;
        private Transform _parent;
        private List<T> _objs;

        public PoolBase(T prefab, Transform parent, int countPrewarm)
        {
            _prefab = prefab;
            _parent = parent;
            _objs = new List<T>(countPrewarm);

            for (var i = 0; i < countPrewarm; i++)
            {
                Create();
            }
        }

        public T Get()
        {
            var obj = _objs.FirstOrDefault(x => !x.isActiveAndEnabled);

            if (obj == null)
            {
                obj = Create();
            }

            obj.gameObject.SetActive(true);

            return obj;
        }

        public void Reset()
        {
            _objs.ForEach(x=> Release(x));
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private T Create()
        {
            var inst = Object.Instantiate(_prefab, _parent);
            inst.gameObject.SetActive(false);
            _objs.Add(inst);
            return inst;
        }
    }
}
