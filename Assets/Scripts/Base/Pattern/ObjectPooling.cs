using UnityEngine;
using UnityEngine.Pool;
using BaseInterface;

public class PoolData
{
    public bool colliectionCheck;
    public int defaultCapitaly;
    public int maxSize;

    public PoolData()
    {
        colliectionCheck = false;
        defaultCapitaly = 10;
        maxSize = 10000;
    }
}
public class ObjectPooling<T> where T : Spawnable
{

    private IObjectPool<Spawnable> objectPool;
    private Spawnable m_prefab;

    public ObjectPooling(Spawnable prefab, PoolData data = null) // defaultCapitaly couh
    {
        m_prefab = prefab;

        data = data == null? new PoolData() : data;

        objectPool = new ObjectPool<Spawnable>(OnCreate, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, data.colliectionCheck, data.defaultCapitaly, data.maxSize);
    }

    // invoked when creating an item to populate the object pool
    private Spawnable OnCreate()
    {
        Spawnable obj = GameObject.Instantiate(m_prefab);
        obj.pool = objectPool;
        return obj;
    }

    // invoked when returning an item to the object pool
    private void OnReleaseToPool(Spawnable pooledObject)
    {
        pooledObject?.gameObject.SetActive(false);
    }
    // invoked when retrieving the next item from the object pool
    private void OnGetFromPool(Spawnable pooledObject)
    {
        pooledObject?.gameObject.SetActive(true);
    }

    private void OnDestroyPooledObject(Spawnable pooledObject)
    {
        GameObject.Destroy(pooledObject);
    }

    public Spawnable Get()
    {
        return objectPool.Get();
    }

    public void Realse(Spawnable obj)
    {
        objectPool?.Release(obj);
    }

    public void Clear()
    {
        objectPool?.Clear();
    }
}
