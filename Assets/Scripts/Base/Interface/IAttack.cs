using System;
using UnityEngine;

namespace BaseInterface
{
    public interface IAttack
    {
        public abstract ITargetable FindTarget(Func<bool> Condition);

        public abstract bool AttackTarget(ITargetable target);
    }
}
