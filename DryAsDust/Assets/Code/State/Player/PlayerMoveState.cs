using System;
using UnityEngine;



[Serializable]
public class PlayerMoveState : State
{




    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Enter Playermove");
        m_owner.SetCanMove(true);
      
    }
   

    

   


}
