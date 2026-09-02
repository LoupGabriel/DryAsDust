using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponController : MonoBehaviour
{

    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] float m_shootingForce = 5f;
    [SerializeField] private Transform m_gunPivot;
    [SerializeField] private Transform m_gunTip;
    private SpriteRenderer m_gunSpriteRenderer;
    
    void Start()
    {
        EventsManager.GetInstance().SubscribeTo(EEvents.ON_PLAYER_SHOOTING, Shooting);
        m_gunSpriteRenderer = m_gunPivot.GetComponentInChildren<SpriteRenderer>();
    }

    private void OnDestroy()
    {
        EventsManager.GetInstance().UnsubscribeFrom(EEvents.ON_PLAYER_SHOOTING, Shooting);
    }

    private void Update()
    {
        Rotation();
    }


    private void Shooting(Dictionary<string, object> data)
    {
        bool shoot = (bool)data["hasShoot"];
        Vector2 direction = (Vector2)data["direction"];
        Debug.Log("Shoot");
       // GameObject currentBullet = Instantiate(m_bulletPrefab,m_gunTip.position, Quaternion.identity);
        BulletItem currentBullet = GameManager.GetInstance().m_bulletPool.GetAvailableBullet();
        currentBullet.gameObject.SetActive(true);
        currentBullet.transform.position = m_gunTip.position;

        Rigidbody2D rb = currentBullet.gameObject.GetComponent<Rigidbody2D>();

        rb.AddForce(direction * m_shootingForce, ForceMode2D.Impulse);

    }

    private Vector2 GetDirection()
    {

        Vector2 screenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = screenPosition - (Vector2)gameObject.transform.position;
        return direction;
    }

    private void Rotation()
    {
       
        m_gunPivot.transform.right = GetDirection();
        Vector2 screenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (screenPosition.x < transform.position.x)
        {
            m_gunSpriteRenderer.flipY = true;
        }
        else
        {
            m_gunSpriteRenderer.flipY = false;
        }
    }
}
