using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseInterface
{
    public interface IBuildable
    {
        public abstract void Init(object data);

        public abstract void Destroy();

        public abstract bool IsBuildable();

        public abstract void SnapToGround();

        public abstract void Build();

        public void Upgrade(int level);
    }
}
