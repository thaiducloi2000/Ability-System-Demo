using BaseInterface;
using Sirenix.Utilities;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using ITargetable = AbilitiesSystem.ITargetAble;

namespace AbilitiesSystem
{
    public class TargetAbility : Ability, IActiveAbility
    {
        [SerializeField] private LayerMask layerInteract;

        [Header("Base Ability Setup")]
        [SerializeField] private AbilityEffect baseEffects;

        [Header("Evole Ability Setup")]
        [SerializeField] private AbilityEffect evolEffects;
        
        private ObjectPooling<Spawnable> pool;
        private Func<bool> conditionLevelUpCheck;
        //private bool isEvole = false;
        private CancellationTokenSource cancellationToken;

        public ObjectPooling<Spawnable> Pool
        {
            get
            {
                if (pool == null)
                {
                    pool = PoolManager.Pool(baseEffects);
                }
                return pool;
            }
        }

        public void ActiveAbility(Vector3 source,Vector3 direction, ITargetable target)
        {
            if (conditionLevelUpCheck != null && !conditionLevelUpCheck() || stats != AbilityStats.READY)
            {
                return;
            }


            AbilityEffect effect = Pool.Get() as AbilityEffect;

            if (target == null) // aoe skill at target position
            {
                effect.transform.position = direction;

                Collider[] obj = Physics.OverlapSphere(effect.transform.position, baseInfor.rangeUse, layerInteract, QueryTriggerInteraction.Ignore);
                ITargetable[] targets = new ITargetable[obj.Length];
                for (int i = 0; i < obj.Length; i++)
                {
                    targets[i] = obj[i].transform.root.GetComponent<ITargetable>();
                }
                CountDownToRelease(effect, targets);
                if (isEvole)
                {
                    EvovleAbility();
                }
            }
            else // target skill
            {
                effect.transform.position = target.TargetTransform().root.position;

                CountDownToRelease(effect, new ITargetable[] { target });
                if(isEvole)
                {
                    EvovleAbility();
                }
            }
        }

        public override void AssignLevelUpCondition(Func<bool> condition = null)
        {
            conditionLevelUpCheck = condition;
        }

        public bool BlockTarget(Vector3[] targets)
        {
            throw new System.NotImplementedException();
        }

        public void EvovleAbility()
        {
            throw new System.NotImplementedException();
        }

        public override void Levelup()
        {
            if (baseEffectives.IsNullOrEmpty()) return;

            if (currentLevel == (baseEffectives.Length - 1) || conditionLevelUpCheck())
            {
                isEvole = true;
                return;
            }

            if (currentLevel < (baseEffectives.Length - 1))
            {
                currentLevel++;
            }
        }

        private async void CountDownToRelease(AbilityEffect effect, ITargetAble[] targets)
        {
            
            cancellationToken = new CancellationTokenSource();
            try
            {
                SetStats(AbilityStats.WAIT);
                
                await Task.Delay((int)(baseInfor.startDealDamageTime * 1000),cancellationToken.Token);
                
                SetStats(AbilityStats.USE);

                foreach (ITargetable target in targets)
                {
                    target.DealDamge(baseInfor);
                }
                
                for (int i = 1; i < baseInfor.amountWave; i++)
                {
                    await Task.Delay((int)(baseInfor.delayPerWaveTime * 1000),cancellationToken.Token);
                    foreach (ITargetable target in targets)
                    {
                        target.DealDamge(baseInfor);
                    }
                }

                await Task.Delay(2000,cancellationToken.Token); // wait 2s for release object to pool
                effect.OnReleaseTrigger();
            }
            catch
            {
                return;
            }
            finally
            {
                cancellationToken.Dispose();
                cancellationToken = null;
            }
            SetStats(AbilityStats.COUNTDOWN);
            CountDownToAbility();
        }

        public override async void CountDownToAbility()
        {
            cancellationToken = new CancellationTokenSource();
            try
            {

                await Task.Delay((int)(baseInfor.countDown * 1000), cancellationToken.Token);
            }
            catch
            {
                return;
            }
            finally
            {
                cancellationToken.Dispose();
                cancellationToken = null;
            }
            SetStats(AbilityStats.READY);
        }
    }
}
