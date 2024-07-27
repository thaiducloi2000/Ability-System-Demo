using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace AbilitiesSystem
{
    public abstract class Ability : MonoBehaviour
    {
        [SerializeField] protected AbilityInfor[] infor;

        [Header("Base Ability Setup")]
        [SerializeField] protected ActiveAbilityInfor baseInfor;
        public ActiveAbilityInfor BaseInfor => baseInfor;
        [SerializeField] protected Stats[] baseEffectives;

        [Header("Evole Ability Setup")]
        [SerializeField] protected ActiveAbilityInfor evoleInfor;
        [SerializeField] protected Stats[] evolEffectives;

        protected Func<bool> activeCondition;
        protected int currentLevel = 0;
        protected bool isEvole = false;

        [SerializeField] protected AbilityStats stats = AbilityStats.READY;

        public virtual void SetStats(AbilityStats nextStats)
        {
            stats = nextStats;
        }
        public abstract void Levelup();

        public abstract void AssignLevelUpCondition(Func<bool> condition = null);

        public virtual async void CountDownToAbility() {
            await Task.Delay((int) (baseInfor.countDown * 1000));
            SetStats(AbilityStats.READY);
        }
    }
}
