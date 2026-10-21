using System;
using UnityEngine;
[Serializable]
public class BigInsectChaseState : State
{
    private Vector2 m_direction;
    [SerializeField] private float m_chasingSpeed = 300f;
    public override void OnEnter()
    {
        base.OnEnter();
        m_owner.Entity.SpriteRender.color = Color.red;
        m_owner.Entity.SetCurrentSpeed(m_chasingSpeed);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        m_direction = RunManager.Instance.Player.transform.localPosition - m_owner.transform.localPosition;
        Move();
    }

    private void Move()
    {
        Vector2 insectVelocity = m_owner.Rb.linearVelocity;

        insectVelocity.x = m_direction.x * m_owner.Entity.CurrentSpeed * Time.deltaTime;
        insectVelocity.y = m_direction.y * m_owner.Entity.CurrentSpeed * Time.deltaTime;

        m_owner.Rb.linearVelocity = insectVelocity;
    }

}
