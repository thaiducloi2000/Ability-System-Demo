using AbilitiesSystem;
using BaseInterface;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using static Unity.VisualScripting.Member;

namespace AbilitiesSystem
{
    public class OrenAbility : Ability, IActiveAbility
    {
        [Header("Genaral Setting")]
        [Tooltip("Setting Is Ability Can Stop On Hit ?")]
        [SerializeField] private bool isDestroyOnHit = true;
        [SerializeField] private BulletSpawnType spawBulletType = BulletSpawnType.ONE_DIRECTION;

        [Header("Base Ability Setup")]
        [SerializeField] private BulletEffect baseEffects;

        [Header("Evole Ability Setup")]
        [SerializeField] private BulletEffect evolEffects;

        private ObjectPooling<Spawnable> poolBase;
        private ObjectPooling<Spawnable> poolEvole;
        private Func<bool> conditionLevelUpCheck;

        private CancellationTokenSource cancellationToken;
        public ObjectPooling<Spawnable> PoolBase
        {
            get
            {
                if (poolBase == null)
                {
                    poolBase = PoolManager.Pool(baseEffects);
                }
                return poolBase;
            }
        }

        public ObjectPooling<Spawnable> PoolEvole
        {
            get
            {
                if (poolBase == null)
                {
                    poolEvole = PoolManager.Pool(evolEffects);
                }
                return poolEvole;
            }
        }

        public void ActiveAbility(Vector3 source, Vector3 direction, ITargetAble target)
        {
            if (stats != AbilityStats.READY) return;

            Vector3[] directions = MathfHelper.CalculateDirection(direction, baseInfor.amountWave, baseInfor.rangeUse);

            CountDownToRelease(source, directions, isEvole ? PoolEvole : PoolBase);
        }

        public override void AssignLevelUpCondition(Func<bool> condition = null)
        {
            conditionLevelUpCheck = condition;
        }

        public bool BlockTarget(Vector3[] targets)
        {
            throw new NotImplementedException();
        }

        public void EvovleAbility()
        {
            throw new NotImplementedException();
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
        private async void CountDownToRelease(Vector3 source, Vector3[] directions, ObjectPooling<Spawnable> pool)
        {

            cancellationToken = new CancellationTokenSource();
            try
            {
                SetStats(AbilityStats.WAIT);

                await Task.Delay((int)(baseInfor.startDealDamageTime * 1000), cancellationToken.Token);

                SetStats(AbilityStats.USE);

                foreach (Vector3 dir in directions)
                {
                    BulletEffect bullet = pool.Get() as BulletEffect;
                    bullet.transform.position = source;
                    bullet.transform.forward = dir;
                    bullet.Init(dir);
                }
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
