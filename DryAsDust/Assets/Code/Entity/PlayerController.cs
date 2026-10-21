using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : EntityController
{
    protected Vector2 m_mousePos;

    private InputAction m_movement;
    private Vector2 m_movementAmount;
    private InputAction m_shoot;
    private bool m_hasShoot;

    [SerializeField] bool m_debugVisualization = false;


    private WeaponController m_weapon;
  

    private void Awake()
    {
        base.Awake();
        m_movement = InputSystem.actions.FindAction("Player/Move");
        m_shoot = InputSystem.actions.FindAction("Player/Shoot");
        m_weapon = GetComponent<WeaponController>();
       
    }
    protected override void Update()
    {
       
       
        base.Update();
        if (m_canMove)
        {
            HandleInput();
        }
       

        ShootingDirection();
        HandleFlip();
        if (m_weapon.GetAttackTimer() <= 0f && m_hasShoot)
        {
            Dictionary<string, object> eventParams = new Dictionary<string, object>();
            eventParams.Add("hasShoot", true);
            eventParams.Add("direction",AimingDirection);
            EventsManager.GetInstance().TriggerEvent(EEvents.ON_PLAYER_SHOOTING, eventParams);
        }
        
    }

    protected override void HandleAnimation()
    {
        Entity.Animator.SetBool("isRunning", Rb.linearVelocity.magnitude > 0);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        

        HandleMovement();
    }

    private void HandleInput()
    {
        m_movementAmount = m_movement.ReadValue<Vector2>();
        m_hasShoot = m_shoot.IsPressed();

    }

  
    private void HandleMovement()
    {
        Vector2 playerVelocity = Rb.linearVelocity;

        playerVelocity.x = m_movementAmount.x * Entity.Speed;
        playerVelocity.y = m_movementAmount.y * Entity.Speed;

        Rb.linearVelocity = playerVelocity;
    }


    private void ShootingDirection()
    {
        m_mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        m_aimingDirection = (m_mousePos - (Vector2)gameObject.transform.position).normalized;

        if (m_debugVisualization)
        {

            Debug.DrawRay(gameObject.transform.position, m_aimingDirection);
        }
    }


   


    private void HandleFlip()
    {
        Vector2 screenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (screenPosition.x > transform.position.x)
        {
            Entity.SpriteRender.flipX = true;
        }
        else
        {
            Entity.SpriteRender.flipX = false;
        }
    }
}
