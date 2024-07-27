using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using BaseInterface;

public static class PoolManager
{
    private static Dictionary<string, ObjectPooling<Spawnable>> poolObject = new() ;

    public static ObjectPooling<Spawnable> Pool(Spawnable preafab, PoolData data = null)
    {
        string namePool = preafab.name;
        if (poolObject.ContainsKey(namePool))
        {
            return poolObject[namePool];
        }

        ObjectPooling<Spawnable> objectPooling = new ObjectPooling<Spawnable>(preafab, data);

        poolObject.Add(namePool, objectPooling);

        return objectPooling;
    }

    public static void RemovePool(Spawnable preafab)
    {
        string namePool = preafab.name;
        if (poolObject.ContainsKey(namePool))
        {
            poolObject.Remove(namePool);
        }
    }
}
