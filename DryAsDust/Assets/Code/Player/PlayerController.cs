using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputActionAsset m_inputAAsset;

    private InputAction m_movement;
    private Vector2 m_movementAmount;

    [SerializeField] bool m_debugVisualization = false;
    //Player Stats

    private Rigidbody2D m_rb;

    [SerializeField] private SpriteRenderer m_renderer;
    [SerializeField] float m_playerSpeed = 5f;
    


    private void Awake()
    {
        m_movement = m_inputAAsset.FindAction("Move");
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
        CalculateShootingDirection();
        HandleFlip();

    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleInput()
    {
        m_movementAmount = m_movement.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        Vector2 playerVelocity = m_rb.linearVelocity;

        playerVelocity.x = m_movementAmount.x * m_playerSpeed;
        playerVelocity.y = m_movementAmount.y * m_playerSpeed;

        m_rb.linearVelocity = playerVelocity;
    }

    private void CalculateShootingDirection()
    {
        Vector2 screenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = screenPosition - (Vector2)gameObject.transform.position;
        if (m_debugVisualization)
        {

            Debug.DrawRay(gameObject.transform.position, direction);
        }
    }

    private void HandleFlip()
    {
        Vector2 screenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(screenPosition.x > transform.position.x)
        {
            m_renderer.flipX = true;
        }
        else
        {
            m_renderer.flipX = false;
        }
    }


}
