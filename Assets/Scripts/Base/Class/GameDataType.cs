using System;
using UnityEngine.Events;

namespace AbilitiesSystem
{
    [Serializable]
    public struct AbilityInfor
    {
        public int level;
        public string description;
    }

    [Serializable]
    public struct StatsLevel
    {
        public float damageScale;
        public float countDownScale;
        public float useRangeScale;
    }

    [Serializable]
    public struct ActiveAbilityInfor
    {
        public int amountWave;
        public float startDealDamageTime;
        public float delayPerWaveTime;
        public float damage;
        public float countDown;
        public float critRate;
        public float critDamage;
        public int rangeUse;
        public StatsLevel state;
        public Buff requireBuffToEvovle;
    }

    [Serializable]
    public struct ActivePassiveEvent
    {
        public UnityEvent activeEvent;
    }

    [Serializable]
    public struct PassiveAbilityInfor
    {
        public Buff buffType;
    }

    public enum Buff
    {
        DAMAGE = 0,
        SPEED,
        CRIT,
        HP,
        DEF
    }

    public enum Stats
    {
        NONE = 0,
        SLOW,
        STUN,
        BLOCK,
        BURN,
    }

    public enum AbilityStats
    {
        COUNTDOWN = 0,
        READY,
        WAIT,
        USE,
    }

    public enum BulletSpawnType
    {
        ONE_DIRECTION = 0,
        MULTI_DIRECTION,
        CIRCLE,
        CUSTOME,
    }
}
