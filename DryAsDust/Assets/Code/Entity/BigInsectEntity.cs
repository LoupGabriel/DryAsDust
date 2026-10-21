using System;
using UnityEngine;

public class BigInsectEntity : Entity
{
    public Action OnTakeDamage;
    public void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        OnTakeDamage?.Invoke();
    }
}
