using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AbilitiesSystem
{
    public interface IActiveAbility
    {
        /// <summary>
        /// Find Target To Block
        /// </summary>
        /// <param name="targets"></param>
        /// <returns></returns>
        public abstract bool BlockTarget(Vector3[] targets);

        /// <summary>
        /// Active Ability after Block Targets
        /// </summary>
        public abstract void ActiveAbility(Vector3 source, Vector3 direction, ITargetAble target);

        /// <summary>
        ///     Evovle 
        /// </summary>
        public abstract void EvovleAbility();
    }

    public interface IPassiveAbility
    {

    }

    public interface ITargetAble
    {
        public Transform TargetTransform();
        public void DealDamge(ActiveAbilityInfor damageInfor = new(), Stats effect = Stats.NONE);
    }

    public interface IAutoCast
    {
        public abstract void RegisterAbility(IActiveAbility ability); // Add To Listenr

        public abstract void RemoveAbility(IActiveAbility ability); // Remove Ability to Listener

        public abstract void Cast(IActiveAbility ability);
    }
}
