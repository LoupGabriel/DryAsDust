using UnityEngine;


[CreateAssetMenu(fileName ="newWeaponType", menuName = "ScriptableObjects/WeaponData")]
public class WeaponSo : ScriptableObject
{

    [Header("Visual")]

    public Sprite sprite;
    public PooledObject projectilePrefab;
    public float shootinForce;
    public int magasize;
    public int shotBeforeReload;
    public float attackRate;
    
}
