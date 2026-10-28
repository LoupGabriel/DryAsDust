using System;
using UnityEngine;



[Serializable]
public class SaveData : MonoBehaviour
{
    private Vector3 m_playerPos;
    private int m_playerCurrentLife;

    public Vector3 SavedPlayerPos => m_playerPos;
    public int SavedPlayerCurrentLife => m_playerCurrentLife;

    public SaveData()
    {
        m_playerPos = RunManager.Instance.Player.transform.position;
        m_playerCurrentLife = RunManager.Instance.Player.CurrentHp;
    }
}
