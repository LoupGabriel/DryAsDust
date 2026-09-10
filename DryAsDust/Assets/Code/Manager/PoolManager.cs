using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;



[System.Serializable]
public struct PoolConfig
{
    public PooledObject prefab;
    [Min(1)] public int initialSize;
}
public class PoolManager : MonoBehaviour, IManager
{
    private Dictionary<GameObject, ObjectPool> m_pools = new Dictionary<GameObject, ObjectPool>();


    //config
    [SerializeField] private PoolConfig[] m_poolConfig;
    [SerializeField] private int m_defaultSize;


    public void Init()
    {
        foreach (var config in m_poolConfig)
        {
            RegisterPool(config.prefab, config.initialSize);
        }
    }
    public void Dispose()
    {

    }

    private void RegisterPool(PooledObject prefab, int initialSize)
    {
        if (m_pools.ContainsKey(prefab.gameObject)) return;
        Transform parent = new GameObject($"Pool_{prefab.name}").transform;
        parent.SetParent(transform);
        m_pools[prefab.gameObject] = ObjectPool.CreatePool(prefab, initialSize, parent);
    }



    public PooledObject Spawn(PooledObject prefab, Vector3 position) => Spawn(prefab, position, Quaternion.identity);

    public PooledObject Spawn(PooledObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!m_pools.ContainsKey(prefab.gameObject))
        {
            RegisterPool(prefab, m_defaultSize);
            return null;
        }

        return m_pools[prefab.gameObject].GetAvailable(position, rotation);

    }

    public void Return(PooledObject obj) => obj?.ReturnToPool();
}
