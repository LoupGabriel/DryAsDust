using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class WeaponController : MonoBehaviour
{

    [SerializeField] PooledObject m_bulletPrefab;
    [SerializeField] float m_shootingForce = 5f;
    [SerializeField] private Transform m_gunPivot;
    [SerializeField] private Transform m_gunTip;
    [SerializeField] private float m_attackRate;
    [SerializeField] private float m_attackTimer;

    private bool m_hasShoot;
    private SpriteRenderer m_gunSpriteRenderer;

    public float GetAttackTimer() => m_attackTimer;
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

        m_attackTimer -= Time.deltaTime;
    }


    private void Shooting(Dictionary<string, object> data)
    {
        m_hasShoot = (bool)data["hasShoot"];
        Vector2 direction = (Vector2)data["direction"];

        m_attackTimer = 1f / m_attackRate;
        var currentBullet = GameManager.Instance.PoolManager.Spawn(m_bulletPrefab, m_gunTip.position,Quaternion.identity) as BulletItem;
        currentBullet.gameObject.SetActive(true);
        currentBullet.transform.position = m_gunTip.position;


        SfxManager.PlaySfx("Riffle_Shoot");

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
