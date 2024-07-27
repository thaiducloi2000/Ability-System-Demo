using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseInterface
{
    public interface ITargetable
    {
        public abstract GameObject SetTarget();
        public abstract void TakeDame(long damage);
    }
}
