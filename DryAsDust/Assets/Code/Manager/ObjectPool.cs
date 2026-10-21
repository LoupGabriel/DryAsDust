using UnityEngine;

using System.Collections.Generic;


using Unity.VisualScripting;

public class ObjectPool : MonoBehaviour
{
    private PooledObject m_prefab;
    private Queue<PooledObject> m_pool =  new Queue<PooledObject>();

    public int AvailableCount => m_pool.Count;
    


    public static ObjectPool CreatePool(PooledObject prefab,int initializeSize , Transform parent)
    {
        ObjectPool pool = parent.AddComponent<ObjectPool>();
        pool.m_prefab = prefab;
        for(int i = 0; i< initializeSize; i++)
        {
            pool.CreateNew();
        }

        return pool;
    }


    /// <summary>
    /// Create a new PoolObject and Enqueue it
    /// </summary>
    /// <returns></returns>
    private PooledObject CreateNew()
    {
        PooledObject obj = Object.Instantiate(m_prefab, transform);
        obj.SetPool(this);
        obj.gameObject.SetActive(false);
        m_pool.Enqueue(obj);
        return obj;

    }

    /// <summary>
    /// Get available pooled object
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public PooledObject GetAvailable(Vector3 position,Quaternion rotation)
    {
        if (m_pool.Count == 0) CreateNew();

        PooledObject obj = m_pool.Dequeue();
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.gameObject.SetActive(true);
        return obj;
    }

    /// <summary>
    /// Return pooled object to his object pool 
    /// </summary>
    /// <param name="obj"></param>
    public void Return(PooledObject obj)
    {
        
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        m_pool.Enqueue(obj);
    }


}
