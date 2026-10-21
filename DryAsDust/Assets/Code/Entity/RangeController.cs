using UnityEngine;

public class RangeController : EntityController
{
    [SerializeField] private float m_distanceToChase = 5f;
    [SerializeField] private float m_distanceToStopChasing = 2f;
    [SerializeField] private float m_minAttackDistance = 2f;

    

    private float m_currentDistance;

    private bool m_hasAttack = false;
  
    private void Awake()
    {
        base.Awake();
        

    }



    protected override void Update()
    {

        m_currentDistance = Vector2.Distance(RunManager.Instance.Player.transform.position, transform.position);
        base.Update();
        HandleFlip();

        if (m_currentDistance < m_distanceToChase && m_currentDistance <= m_minAttackDistance && !m_hasAttack)
        {
            m_stateMachine.SetState<ShooterAttackState>();
            //m_hasAttack = true;
        }
        else if (m_currentDistance > m_minAttackDistance && m_currentDistance < m_distanceToChase)
        {
            m_stateMachine.SetState<ShooterChaseState>();
            
        }
        else
        {
            m_stateMachine.SetState<ShooterMoveState>();
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
            
        }
        else
        {
            Entity.SpriteRender.flipX = false;
          
        }
    }


    protected void Attack()
    {
        m_hasAttack = false;
        
    }
}
