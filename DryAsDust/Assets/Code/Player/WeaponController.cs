using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] float m_shootingForce = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventsManager.GetInstance().SubscribeTo(EEvents.ON_PLAYER_SHOOTING, Shooting);
    }

    private void OnDestroy()
    {
        EventsManager.GetInstance().UnsubscribeFrom(EEvents.ON_PLAYER_SHOOTING, Shooting);
    }



    private void Shooting(Dictionary<string, object> data)
    {
        bool shoot = (bool)data["hasShoot"];
        Vector2 direction = (Vector2)data["direction"];
        
        GameObject currentBullet = Instantiate(m_bulletPrefab,transform.position, Quaternion.identity);
        //BulletItem currentBullet = GameManager.GetInstance().m_bulletPool.GetAvailableBullet();
        Rigidbody2D rb = currentBullet.gameObject.GetComponent<Rigidbody2D>();

        rb.AddForce(direction * m_shootingForce, ForceMode2D.Impulse);

    }
}
