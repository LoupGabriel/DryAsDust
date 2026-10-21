using System.Collections.Generic;
using UnityEngine;

public abstract class UiEntity : MonoBehaviour
{
    protected Entity m_owner;
    [SerializeField] protected GameObject m_hpBar;

    public virtual void Start()
    {
        m_owner = GetComponent<Entity>();
        m_hpBar.gameObject.transform.localScale = Vector3.one;
    }
    public virtual void SetHpBarProgress(int amount)
    {

        float hpPercent = (float)m_owner.CurrentHp / m_owner.MaxHp;
        Vector3 newScale = new Vector3(hpPercent, 1, 1);
        m_hpBar.transform.localScale = newScale;
    }
}
