using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class StateMachine
{
    [SerializeReference,SubclassSelector] List<State> m_states = new List<State>();

    private EntityController m_owner;
    private State m_currentState;


    public void Init(EntityController owner)
    {
        m_owner = owner;
        m_currentState = null;
        foreach(var state in m_states)
        {
            state.Init(m_owner);

        }
    }

    public void Start()
    {
        foreach(var state in m_states)
        {
            if(state.IsDefault)
            {
                SetState(state);
                break;
            }
        }
    }

    public void Update() { m_currentState?.OnUpdate(); }
    public void FixedUpdate() { m_currentState?.OnFixedUpdate(); }

    public State GetState<T>()
    {
        foreach (var state in m_states) 
            if (state is T) return state;
        return null;
    }

    public void SetState<T>() where T : State
    {
        State state = GetState<T>();
        if (state != null) SetState(state);
    }

    public void SetState(State state)
    {
        m_currentState?.OnExit();
        m_currentState = state;
        m_currentState.OnEnter();
    }
}
