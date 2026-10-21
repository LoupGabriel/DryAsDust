using System.Collections;
using System.Timers;
using UnityEngine;
using UnityEngine.Pool;

public enum EBulletCamp
{
    Enemy,
    Player
}
public class BulletItem : PooledObject
{

    [SerializeField] private float m_bulletLifetime = 1f;
    [SerializeField] private int m_damage = 1;
    [SerializeField] private EBulletCamp m_bulletCamp;

    private Vector3 m_direction;

    public void SetDirection(Vector3 direction)
    {
        m_direction = direction;
    }

    private void Awake()
    {
        StartCoroutine(BulletLife());
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = null;
        switch (m_bulletCamp)
        {
            case EBulletCamp.Enemy:
                tag = "Enemy";
                break;
            case EBulletCamp.Player:
                tag = "Player";
                break;
        }
        if (collision.gameObject.CompareTag(tag))
        {
            return;
        }
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

