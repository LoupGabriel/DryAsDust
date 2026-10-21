using UnityEngine;
using System.Collections;
public class EntityController : MonoBehaviour
{
    [SerializeField] private Entity m_entity;
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] protected StateMachine m_stateMachine;
    [SerializeField] private float m_roamingRadius = 5f;
    protected bool m_canMove = false;
    
    protected Vector2 m_aimingDirection;
    public Vector2 AimingDirection => m_aimingDirection;
    public Entity Entity => m_entity;
    public Rigidbody2D Rb => m_rb;
    public float RoamingRadius => m_roamingRadius;
   
    public void SetCanMove(bool canMove)
    {
        m_canMove = canMove;
    }


    protected virtual void Awake() => m_stateMachine.Init(this);
    protected virtual void Start() => m_stateMachine.Start();
    protected virtual void Update() { HandleAnimation(); m_stateMachine.Update(); }
    protected virtual void FixedUpdate() { m_stateMachine.FixedUpdate(); }

    protected virtual void HandleAnimation()
    {
        
    }
    public void Attack()
    {
        StartCoroutine(AttackRoutine());
    }
    private IEnumerator AttackRoutine()
    {
        Entity.Animator.SetTrigger("isAttacking");
       
        yield return null;
    }
    public void Move(Vector2 direction)
    {
        m_rb.linearVelocity = direction;
    }
}
