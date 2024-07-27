using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitiesSystem;
using System.Threading.Tasks;
using Sirenix.OdinInspector;

public class Dummy : MonoBehaviour,ITargetAble
{
    [SerializeField] private Animator animatorController;

    private const string ANIMATOR_GET_HIT = "GetHit";
    private const string ANIMATOR_DEAD = "IsDead";

    private void Awake()
    {
        if(animatorController == null)
        {
            animatorController = GetComponent<Animator>();
        }
    }

    [Button]
    public void DealDamge(ActiveAbilityInfor damageInfor = default, Stats effect = Stats.NONE)
    {
        animatorController.SetTrigger(ANIMATOR_GET_HIT);
        if (damageInfor.damage > 0)
        {
            animatorController.SetBool(ANIMATOR_DEAD, true);
            ResetAnimatiorState();
        }
    }

    public Transform TargetTransform()
    {
        return this.transform.root;
    }

    private async void ResetAnimatiorState()
    {
        await Task.Delay(2000);
        animatorController.SetBool(ANIMATOR_DEAD, false);
    }
}
