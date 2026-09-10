using System;
using UnityEngine;


[Serializable]
public abstract class State 
{
    [SerializeField] protected bool isDefault;
    protected EntityController m_owner;

    public bool IsDefault => isDefault;
    

    public void Init(EntityController owner) => m_owner = owner;

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
    public virtual void OnEnable() { }
    public virtual void OnDisable() { }
    public virtual void OnDestroy() { }
}
