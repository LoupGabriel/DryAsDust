using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionAsset m_inputAAsset;

    private InputAction m_movement;
    private Vector2 m_movementAmount;

    private InputAction m_shoot;
    private bool m_hasShoot;

    [SerializeField] bool m_debugVisualization = false;
    //Player Stats

    private Rigidbody2D m_rb;

    [SerializeField] private SpriteRenderer m_renderer;
    [SerializeField] float m_playerSpeed = 5f;



    private void Awake()
    {
        m_movement = m_inputAAsset.FindAction("Move");
        m_shoot = m_inputAAsset.FindAction("Shoot");
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
        //ShootingDirection();
        HandleFlip();
        if (m_hasShoot)
        {
            Dictionary<string, object> eventParams = new Dictionary<string, object>();
            eventParams.Add("hasShoot", true);
            eventParams.Add("direction",CalculateShootingDirection());
            EventsManager.GetInstance().TriggerEvent(EEvents.ON_PLAYER_SHOOTING, eventParams);
        }

    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleInput()
    {
        m_movementAmount = m_movement.ReadValue<Vector2>();
        m_hasShoot = m_shoot.WasPressedThisFrame();

    }

    private void HandleMovement()
    {
        Vector2 playerVelocity = m_rb.linearVelocity;

        playerVelocity.x = m_movementAmount.x * m_playerSpeed;
        playerVelocity.y = m_movementAmount.y * m_playerSpeed;

        m_rb.linearVelocity = playerVelocity;
    }

    private void ShootingDirection()
    {
        Vector2 screenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = screenPosition - (Vector2)gameObject.transform.position;
        if (m_debugVisualization)
        {

            Debug.DrawRay(gameObject.transform.position, direction);
        }
    }
    private Vector2 CalculateShootingDirection()
    {
        Vector2 screenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = screenPosition - (Vector2)gameObject.transform.position;
        return direction;
    }

    private void HandleFlip()
    {
        Vector2 screenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (screenPosition.x > transform.position.x)
        {
            m_renderer.flipX = true;
        }
        else
        {
            m_renderer.flipX = false;
        }
    }




}
