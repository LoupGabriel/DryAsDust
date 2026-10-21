using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity :MonoBehaviour, IDamageable
{
    [SerializeField] protected float m_speed;
    [SerializeField] protected int m_maxHp;
    [SerializeField] protected int m_damage;
    [SerializeField] protected int m_currentHp;
    [SerializeField] protected SpriteRenderer m_spriteRenderer;
    [SerializeField] protected UiEntity m_uiEntity;
    [SerializeField] protected Animator m_animator;
    private float m_currentSpeed;
   
    public bool IsAlive => m_currentHp > 0;
    public float Speed => m_speed;
    public int MaxHp => m_maxHp;
    public int CurrentHp => m_currentHp;
    public int Damage => m_damage;
    public float CurrentSpeed => m_currentSpeed;
    public SpriteRenderer SpriteRender => m_spriteRenderer;
    public Animator Animator => m_animator;
   
   
    protected virtual void Awake()
    {
        m_currentHp = m_maxHp;
        m_currentSpeed = m_speed;
    }

    public void SetCurrentSpeed(float speed) => m_currentSpeed = speed;
   
    public void Heal(int amount)
    {
        m_currentHp += amount;
        m_uiEntity.SetHpBarProgress(-amount);

        m_currentHp = Mathf.Clamp(m_currentHp, 0, m_maxHp);
    }

    public void Kill()
    {
        if(m_animator != null)
        {
            m_animator.SetTrigger("isDead");
        }
        else
        {
            Destroy(gameObject);
        }
          
       
       
    }

    public void ReturnToPool()
    {
        //destroy temporary 
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int amount)
    {
        m_currentHp -= amount;
       
        m_currentHp = Mathf.Clamp(m_currentHp, 0, m_maxHp);
        m_uiEntity.SetHpBarProgress(amount);
        if (!IsAlive )
        {
            Kill();
           
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
