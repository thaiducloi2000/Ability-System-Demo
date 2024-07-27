using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BaseInterface
{
    public interface IAttackable
    {
        public abstract void Attack(ITargetable target);
        
    }
}
