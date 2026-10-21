using System.Collections.Generic;
using UnityEngine;

public class UiEntityDestructible : UiEntity
{

    public override void Start()
    {
        base.Start();
        m_hpBar.gameObject.transform.localScale = Vector3.one;
        
    }

   

    
}
