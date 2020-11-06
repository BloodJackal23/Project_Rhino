using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    protected Vector2 velocity;
    protected Vector2 targetVelocity;
    public bool grounded { get; protected set; }
    protected Vector2 groundNormal;
    [SerializeField] protected Rigidbody2D rigidbody;
    protected float minGroundNormalY = .65f;
    [SerializeField] protected float gravityModifier = 1f;
    protected const float MIN_MOVE_DISTANCE = .001f;
    protected const float SHELL_RADIUS = .01f;

    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected ContactFilter2D contactFilter;

    protected GameManager gameManager;

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
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        targetVelocity = Vector2.zero;
        if(!gameManager.gamePaused)
        {
            ComputeVelocity();
        }
    }

    private void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;
        grounded = false;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        
        Vector2 deltaPos = velocity * Time.deltaTime;
        Vector2 move = moveAlongGround * deltaPos.x;
        Movement(move, false);
        move = Vector2.up * deltaPos.y;
        Movement(move, true);
    }

    void Movement(Vector2 _move, bool _yMovement)
    {
        float dist = _move.magnitude;
        if(dist > MIN_MOVE_DISTANCE)
        {
            int count = rigidbody.Cast(_move, contactFilter, hitBuffer, dist + SHELL_RADIUS);
            hitBufferList.Clear();

            for(int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for(int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;

                if(currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if(_yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                float projection = Vector2.Dot(velocity, currentNormal);
                if(projection < 0)
                {
                    velocity -= projection * currentNormal;
                }
                float modifiedDistance = hitBufferList[i].distance - SHELL_RADIUS;
                dist = modifiedDistance < dist ? modifiedDistance : dist;
            }
        }
        rigidbody.position += _move.normalized * dist;
    }

    protected virtual void ComputeVelocity()
    {

    }
}
