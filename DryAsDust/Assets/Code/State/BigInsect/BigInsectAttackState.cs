using System;
using UnityEngine;

[Serializable]
public class BigInsectAttackState : State
{
  
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Enter Attack State");
        m_owner.Attack();
        m_owner.Entity.SpriteRender.color = Color.white;

    }

    
}
