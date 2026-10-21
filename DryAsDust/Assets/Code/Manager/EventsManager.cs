using System;
using System.Collections.Generic;
using UnityEngine;


public enum EEvents
{
    ON_PLAYER_SHOOTING,
    ON_PLAYER_DEATH,
    ON_PlAYER_TAKE_DAMAGE,
    ON_DESTRUCTIBLE_DAMAGE
}
public class EventsManager 
{
    private static EventsManager m_instance;

    private Dictionary<EEvents,Action<Dictionary<string, object>>> m_events;

    private EventsManager()
    {
        m_events = new Dictionary<EEvents, Action<Dictionary<string, object>>>();
    }

    public static EventsManager GetInstance()
    {
        if(m_instance == null)
        {
            m_instance = new EventsManager();
        }
        return m_instance;
    }


    public void SubscribeTo(EEvents eventId, Action<Dictionary<string, object>> listener)
    {
        if (m_events.ContainsKey(eventId))
        {
            m_events[eventId] += listener;
        }
        else
        {
            m_events.Add(eventId, listener);
        }
    }

    public void UnsubscribeFrom(EEvents eventId , Action<Dictionary<string, object>> listener)
    {
        if (m_events.ContainsKey(eventId))
        {
            m_events[eventId] -= listener;
        }
    }

    public void TriggerEvent(EEvents eventId, Dictionary<string, object> data)
    {

        if(m_events.ContainsKey((eventId)))
        {
            m_events[eventId].Invoke(data);
        }

    }
}
