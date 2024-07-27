using System;
using System.Collections;

namespace BaseInterface
{
    public interface IAbility
    {
        /// <summary>
        /// Active Ability
        /// </summary>
        /// <returns></returns>
        public abstract bool UseAbility();

        /// <summary>
        /// Return Status Ability , True : In Count Down, False : Ready to Use
        /// </summary>
        /// <returns></returns>
        public abstract bool IsAbilityCountDown();

        /// <summary>
        /// Count Down Ability
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerator CountDownAbility();

        /// <summary>
        /// CallBack When Ability Ready To Use
        /// </summary>
        /// <param name="callBack"></param>
        public void AssignAbility(Action<bool> callBack);
    }
}
