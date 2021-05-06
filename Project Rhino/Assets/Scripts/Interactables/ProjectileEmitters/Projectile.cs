using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Header("Members")]
    [SerializeField] protected PlayerHazard m_playerHazard;
    [SerializeField] protected Rigidbody2D m_rigidbody;
    [SerializeField] protected Collider2D m_collider;

    [SerializeField] protected float lifeSpan = 5f;
    [SerializeField] protected bool destroyOnImpact = true;
    [SerializeField] protected LayerMask targetLayers;

    public PlayerHazard PlayerHazard { get { return m_playerHazard; } }
    public Rigidbody2D Rigidbody { get { return m_rigidbody; } }
    public Collider2D Collider { get { return m_collider; } }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyOnImpact && m_collider.IsTouchingLayers(targetLayers))
            Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        
    }

    public void Init(Vector2 _direction, float _force, float _spinForce = 0) //Initialized by the gun that fires this projectile
    {
        Destroy(gameObject, lifeSpan);
        m_rigidbody.AddForce(_direction * _force, ForceMode2D.Impulse);
        m_rigidbody.AddTorque(Random.Range(-_spinForce, _spinForce), ForceMode2D.Impulse);
    }
}
