using UnityEngine;

public interface IDamageable 
{
    void TakeDamage(int amount);
    void Heal(int heal);
    void Kill();
    bool IsAlive {  get; }
}
