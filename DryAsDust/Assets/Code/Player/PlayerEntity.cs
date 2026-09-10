using Unity.VisualScripting;
using UnityEngine;

public class PlayerEntity : Entity
{
    protected virtual void Start()
    {
        RunManager.Instance.SetPlayer(this);
        CheatManager.Instance.OnHeal += Heal;
        CheatManager.Instance.OnDamage += TakeDamage;
    }


    private void OnDestroy()
    {
        CheatManager.Instance.OnHeal -= Heal;
        CheatManager.Instance.OnDamage -= TakeDamage;
    }
}
