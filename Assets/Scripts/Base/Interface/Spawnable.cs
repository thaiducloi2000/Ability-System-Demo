using System;
using UnityEngine;
using UnityEngine.Pool;
namespace BaseInterface
{
    public abstract class Spawnable : MonoBehaviour
    {
        public IObjectPool<Spawnable> pool;

        /// <summary>
        /// Init Object , Parse Data and Save it to Pool with Key
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        public abstract void Init(object data);
        public abstract string GetPoolKey();

        public abstract void OnReleaseTrigger();
    }
}
