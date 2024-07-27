using BaseInterface;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using ITargetable = AbilitiesSystem.ITargetAble;

public class BulletEffect : Spawnable
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject normalEffect;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float delayRelease;
    [SerializeField] private float distanceCheck;
    private Vector3 flyDirection;
    public float speed;

    private float time;
    private CancellationTokenSource cancellationToken = new();
    bool isRelease = false;
    private void Update()
    {
        //time += Time.deltaTime;
        //float sinValue = Mathf.Sin(time * 10f) * 1f;
        //Vector3 offset = Vector3.Cross(flyDirection, Vector3.up).normalized * sinValue;
        //transform.position += (flyDirection * speed + offset) * Time.deltaTime;
        ////transform.Translate((flyDirection * speed + offset) * Time.deltaTime, Space.World);
        transform.Translate(flyDirection.normalized * speed * Time.deltaTime, Space.World);
        if (isRelease == false)
        {
            CheckHitObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ITargetable target = other.GetComponent<ITargetable>();
        if (target != null)
        {
            target.DealDamge();
        }
    }

    private void CheckHitObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, distanceCheck, groundLayer, QueryTriggerInteraction.Ignore))
        {
            isRelease = true;
            normalEffect.SetActive(false);
            hitEffect.SetActive(true);
            flyDirection = Vector3.zero;
            DelayRelease();
        }
    }

    public override string GetPoolKey()
    {
        return "Effect";
    }

    public override void Init(object data)
    {
        flyDirection = (Vector3)data;
        isRelease = false;
        // vfx 
        normalEffect.SetActive(false);
        normalEffect.SetActive(true);
    }

    public override void OnReleaseTrigger()
    {
        pool.Release(this);
    }

    private async void DelayRelease()
    {
        cancellationToken = new CancellationTokenSource();

        try
        {
            await Task.Delay((int)(delayRelease * 1000), cancellationToken.Token);
            hitEffect.SetActive(false);
            OnReleaseTrigger();
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
    }
}
