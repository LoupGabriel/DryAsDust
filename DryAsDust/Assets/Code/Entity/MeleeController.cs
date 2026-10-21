using System.Collections;
using UnityEngine;

public class MeleeController : EntityController
{
    [SerializeField] private float m_distanceToChase = 5f;
    [SerializeField] private float m_distanceToStopChasing = 2f;
    [SerializeField] private float m_minAttackDistance = 2f;
    
    [SerializeField] private GameObject m_collider;
  
    private float m_currentDistance;

    private bool m_hasAttack = false;
    private BigInsectEntity m_insectEntity;
    private void Awake()
    {
        base.Awake();
        m_insectEntity = GetComponent<BigInsectEntity>();
       
    }

   
   
    protected override void Update()
    {
        
        m_currentDistance = Vector2.Distance(RunManager.Instance.Player.transform.position, transform.position);
        base.Update();
        HandleFlip();

        if(m_currentDistance < m_distanceToChase && m_currentDistance > m_minAttackDistance)
        {
            m_stateMachine.SetState<BigInsectChaseState>();
        }
        else if (m_currentDistance >= m_distanceToStopChasing)
        {
            m_stateMachine.SetState<BigInsectMoveState>();
        }
        else if(m_currentDistance <= m_minAttackDistance && !m_hasAttack)
        {
            m_stateMachine.SetState<BigInsectAttackState>();
            m_hasAttack = true;
        }
    }

    protected virtual void HandleAnimation()
    {
       
    }
    private void HandleFlip()
    {
        Transform playerPos = RunManager.Instance.Player.transform;
        if (playerPos.position.x < transform.position.x)
        {
            Entity.SpriteRender.flipX = true;
            m_collider.transform.localScale = new Vector3(-1, 1, 1); 
        }
        else
        {
            Entity.SpriteRender.flipX = false;
            m_collider.transform.localScale = Vector3.one;
        }
    }

    
    protected void Attack()
    {
        m_hasAttack = false;
        m_stateMachine.SetState<BigInsectMoveState>();
    }
   




}
