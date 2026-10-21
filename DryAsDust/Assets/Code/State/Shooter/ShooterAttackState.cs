using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShooterAttackState : State
{
    private Vector2 m_shootingDirection;
    private float m_shootElapse = 0;
    [SerializeField] private float m_shootingRate = 1f;
    [SerializeField] private Transform m_gunPivot;
    [SerializeField] private Transform m_gunTip;
    [SerializeField] private WeaponSo m_weapon;
    [SerializeField] private SpriteRenderer m_gunSpriteRenderer;

    private float m_attackTimer;
    private Vector3 m_playerPos;
    public override void OnEnter()
    {
        base.OnEnter();
        m_owner.Rb.linearVelocity = Vector3.zero;

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        m_playerPos = RunManager.Instance.Player.transform.localPosition;
        m_shootingDirection = m_playerPos - m_owner.transform.position;
        Rotation();

        m_attackTimer -= Time.deltaTime;
        m_shootElapse += Time.deltaTime;
        if(m_shootElapse > m_shootingRate)
        {
            Shooting();
            m_shootElapse = 0;
        }

    }

    private void Shooting()
    {
        
        


        m_attackTimer = 1f / m_weapon.attackRate;
        var currentBullet = GameManager.Instance.PoolManager.Spawn(m_weapon.projectilePrefab, m_gunTip.position, Quaternion.identity) as BulletItem;
        currentBullet.gameObject.SetActive(true);
        currentBullet.transform.position = m_gunTip.position;


        SfxManager.PlaySfx("Riffle_Shoot");

        Rigidbody2D rb = currentBullet.gameObject.GetComponent<Rigidbody2D>();
        currentBullet.SetDirection(m_shootingDirection);
        rb.AddForce(m_shootingDirection * m_weapon.shootinForce, ForceMode2D.Impulse);
        //bullet rotation
        float angle = Mathf.Atan2(m_shootingDirection.y, m_shootingDirection.x) * Mathf.Rad2Deg;
        rb.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);

    }

  

    private void Rotation()
    {

        m_gunPivot.transform.right = m_shootingDirection;
        Vector2 screenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (screenPosition.x < m_playerPos.x)
        {
            m_gunSpriteRenderer.flipY = true;
        }
        else
        {
            m_gunSpriteRenderer.flipY = false;
        }
    }



}
