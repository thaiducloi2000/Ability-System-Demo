using System;
using UnityEngine.Events;

namespace BaseInterface
{
    public interface ICondition
    {
        /// <summary>
        /// Condition to callback Event
        /// </summary>
        /// <returns></returns>
        public abstract void Condition(Func<bool> conditionFunc);

        ///// <summary>
        ///// Assign callback to Condition
        ///// </summary>
        ///// <param name="callback"></param>
        //public void AssignCondition(UnityAction callback);
    }
}
