using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : PlayerHazard
{
    Rigidbody2D rigidbody;
    Collider2D collider;
    [SerializeField] float lifeSpan = 5f;
    [SerializeField] LayerMask targetLayers;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collider.IsTouchingLayers(targetLayers))
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
