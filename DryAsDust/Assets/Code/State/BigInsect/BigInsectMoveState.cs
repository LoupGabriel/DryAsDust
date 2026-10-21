using System;
using UnityEngine;

[Serializable]
public class BigInsectMoveState : State
{
    private Vector2 m_direction;
    private float m_directionTimer = 1f;
    private float m_force = 0.025f;

    private Vector3 m_startingPos;
    private float m_currentDistanceFromStart;
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("BigInsect enter Movestate");
        m_startingPos = m_owner.gameObject.transform.position;
        m_owner.Entity.SpriteRender.color = Color.white;
        m_owner.Entity.SetCurrentSpeed(m_owner.Entity.Speed);
    }


    public override void OnUpdate()
    {
        m_currentDistanceFromStart = Vector2.Distance(m_startingPos, m_owner.transform.position);
        m_directionTimer -= Time.deltaTime;


        if (m_directionTimer <= 0f ) 
        {
            ChangeDirection();
        }
       

        Move();
    }

    private void ChangeDirection()
    {
        if (m_currentDistanceFromStart >= m_owner.RoamingRadius)
        {
            m_direction = m_startingPos - m_owner.transform.position;
        }
        else
        {
            m_direction = UnityEngine.Random.insideUnitCircle.normalized;
        }
      
        m_directionTimer = UnityEngine.Random.Range(1f, 3f);
    }

    private void Move()
    {
        Vector2 insectVelocity = m_owner.Rb.linearVelocity;

        insectVelocity.x = m_direction.x * m_owner.Entity.CurrentSpeed * Time.deltaTime;
        insectVelocity.y = m_direction.y * m_owner.Entity.CurrentSpeed * Time.deltaTime;

        m_owner.Rb.linearVelocity = insectVelocity;
    }



}
