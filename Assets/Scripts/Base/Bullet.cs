using BaseInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : Spawnable, IBullet, ICondition
{
    [SerializeField] private LayerMask layer;
    private long damage = 10;
    //private float rangeDamaged = 3f;
    private float speed = 10f;
    private Func<bool> conditionFunction;
    private Func<Vector3, Vector3> moveTrajectory;
    private Vector3 moveDirection;
    public Func<bool> CheckCondition => conditionFunction;
    public long Damage => damage;
    public virtual void Hit()
    {
        OnReleaseTrigger();
    }

    private void Update()
    {
        this.transform.position = moveTrajectory(moveDirection);
    }

    public void MoveDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    public override void Init(object data)
    {
        throw new NotImplementedException();
    }

    public override string GetPoolKey()
    {
        return "BULLET";
    }

    public override void OnReleaseTrigger()
    {
        pool.Release(this);
    }

    public void Condition(Func<bool> conditionFunc)
    {
        conditionFunction += conditionFunc;
    }

    public virtual void AssignTrajectory(Func<Vector3,Vector3> trajectory)
    {
        moveTrajectory = trajectory;
    }
}
