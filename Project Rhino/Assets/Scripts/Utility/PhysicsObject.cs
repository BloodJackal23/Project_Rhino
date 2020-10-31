using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    protected Vector2 velocity;
    [SerializeField] protected Rigidbody2D rigidbody;
    [SerializeField] protected float gravityModifier = 1f;
    protected const float MIN_MOVE_DISTANCE = .001f;
    protected const float SHELL_RADIUS = .01f;

    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected ContactFilter2D contactFilter;

    private void OnEnable()
    {
        if (!rigidbody)
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        Vector2 deltaPos = velocity * Time.deltaTime;
        Vector2 move = Vector2.up * deltaPos.y;
        Movement(move);
    }

    void Movement(Vector2 _move)
    {
        float dist = _move.magnitude;
        if(dist > MIN_MOVE_DISTANCE)
        {
            int count = rigidbody.Cast(_move, contactFilter, hitBuffer, dist + SHELL_RADIUS);
        }
        rigidbody.position += _move;
    }
}
