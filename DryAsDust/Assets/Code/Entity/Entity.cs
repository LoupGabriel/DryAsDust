using Unity.VisualScripting;
using UnityEngine;

public class Entity :MonoBehaviour, IDamageable
{
    [SerializeField] protected float m_speed;
    [SerializeField] protected int m_maxHp;
    [SerializeField] protected int m_damage;
    [SerializeField] protected int m_currentHp;
    [SerializeField] protected SpriteRenderer m_spriteRenderer;
    [SerializeField] protected Animator m_animator;
    public bool IsAlive => m_currentHp > 0;
    public float Speed => m_speed;
    public int MaxHp => m_maxHp;
    public int Damage => m_damage;
    public SpriteRenderer SpriteRender => m_spriteRenderer;
    public Animator Animator => m_animator;


    protected virtual void Awake()
    {
        m_currentHp = m_maxHp;
    }

   
    public void Heal(int heal)
    {
        m_currentHp += heal;
        m_currentHp = Mathf.Clamp(m_currentHp, 0, m_maxHp);
    }

    public void Kill()
    {
        m_currentHp = 0;
    }

    public void TakeDamage(int amount)
    {
        m_currentHp -= amount;
        m_currentHp = Mathf.Clamp(m_currentHp, 0, m_maxHp);
    }
}
