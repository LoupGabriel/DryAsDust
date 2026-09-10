using System.Collections;
using System.Timers;
using UnityEngine;
using UnityEngine.Pool;

public class BulletItem : PooledObject
{

    [SerializeField] private float m_bulletLifetime = 1f;
    [SerializeField] private int m_damage = 1;

    private void Awake()
    {
        StartCoroutine(BulletLife());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(m_damage);
            
        }
        ReturnToPool();

    }

    private IEnumerator BulletLife()
    {
        float elapse = 0;
        while (elapse < m_bulletLifetime)
        {
            elapse += Time.deltaTime;
            yield return null;
        }
        ReturnToPool();
    }
}

