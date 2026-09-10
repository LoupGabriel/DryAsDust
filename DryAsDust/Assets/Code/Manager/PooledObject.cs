using UnityEngine;

public class PooledObject : MonoBehaviour
{
    protected ObjectPool m_pool;

    public void SetPool(ObjectPool pool) => m_pool = pool;
    public void ReturnToPool()
    {
        if(m_pool != null)
        {

            m_pool.Return(this);
        }


    }
   


    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }
}
