using AbilitiesSystem;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AbilitiesHandler : MonoBehaviour/*, IAutoCast*/
{
    [SerializeField] private Ability[] abilities;

    private Func<ITargetAble[]> CheckUseAbility;

    private int currentAbilityIndex = 0;

    private List<IActiveAbility> activeAbility = new();

    private void Start()
    {
        LoadAllActiveAbility();
    }

    private void Update()
    {
        AutoCast();
    }

    private void AutoCast()
    {
        foreach(IActiveAbility ability in activeAbility)
        {
            Cast(ability);
        }
    }

    private void LoadAllActiveAbility()
    {
        foreach (Ability ability in abilities)
        {
            Ability tmpAbility = Instantiate(ability,this.transform);
            if (tmpAbility is IActiveAbility)
            {
                activeAbility.Add(tmpAbility as IActiveAbility);
            }
        }
    }

    private void Cast(IActiveAbility ability)
    {
        ITargetAble[] targets = CheckUseAbility();
        if (targets.IsNullOrEmpty()) return;
        int rd = Random.Range(0, targets.Length - 1);
        Vector3 direction = targets[rd].TargetTransform().position - this.transform.root.position;
        UseAbility(ability, direction, targets[rd]);
    }

    //public void RegisterAbility(IActiveAbility ability)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void RemoveAbility(IActiveAbility ability)
    //{
    //    throw new System.NotImplementedException();
    //}

    private void UseAbility(IActiveAbility ability, Vector3 position, ITargetAble target = null)
    {
        ability.ActiveAbility(transform.position, position, target);
    }

    public void AssignCheckTarget(Func<ITargetAble[]> callback)
    {
        CheckUseAbility = callback;
    }
}
