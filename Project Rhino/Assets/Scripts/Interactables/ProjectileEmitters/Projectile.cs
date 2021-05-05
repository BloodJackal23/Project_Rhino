using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : PlayerHazard
{
    public Rigidbody2D rigidbody { get; private set; }
    Collider2D collider;
    [SerializeField] protected float lifeSpan = 5f;
    [SerializeField] protected bool destroyOnImpact = true;
    [SerializeField] LayerMask targetLayers;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(destroyOnImpact && collider.IsTouchingLayers(targetLayers))
        {
            Destroy(gameObject);
        }
    }

    public void Init(Vector2 _direction, float _force) //Initialized by the gun that fires this projectile
    {
        Destroy(gameObject, lifeSpan);
        if(!rigidbody)
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }
        collider = GetComponent<Collider2D>();
        AddForce(_direction, _force);
    }

    private void AddForce(Vector2 _direction, float _force)
    {
        rigidbody.AddForce(_direction * _force, ForceMode2D.Impulse);
    }
}
