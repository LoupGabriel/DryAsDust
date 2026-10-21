using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    IDamageable m_damageable;
    [SerializeField] private Entity m_entity;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                m_damageable = collision.GetComponent<IDamageable>();

                m_damageable.TakeDamage(m_entity.Damage);
            }
        }
        
    }
}
