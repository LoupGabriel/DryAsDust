
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private List<BulletItem> m_bulletList= new List<BulletItem>();

    [SerializeField] private GameObject m_bullet;

    public BulletItem GetAvailableBullet()
    {
        foreach (BulletItem item in m_bulletList)
        {
            if (!item.isActiveAndEnabled)
            {
                return item;
            }
       
        }
        GameObject newBullet = Instantiate(m_bullet);
        BulletItem newBulletItem = m_bullet.GetComponent<BulletItem>();

        m_bulletList.Add(newBulletItem);
        return newBulletItem;
    }
}
