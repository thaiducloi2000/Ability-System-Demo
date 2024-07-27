using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseInterface
{
    public interface IMoveable
    {
        public abstract void Move(Vector3 direction);
    }
}
