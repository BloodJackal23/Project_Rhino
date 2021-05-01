using UnityEngine;

public class Boss_AI: MonoBehaviour
{
    [Header("Memebers")]
    [SerializeField] private Rigidbody2D m_rb;
    [SerializeField] private SpriteRenderer m_spriteRenderer;
    [Space]

    [Header("Attributes")]
    [SerializeField, Range(1f, 1000f)] private float moveSpeed = 250f;
    [SerializeField] private float movementSmoothing = 0.05f;

    #region Private Variables
    private Transform playerTransform;
    private Vector2 targetVelocity;
    private Vector2 currentVelocity = Vector2.zero;
    [SerializeField] private bool lastMoveLeft;
    #endregion

    private void Awake()
    {
        if (!m_rb)
            m_rb = GetComponent<Rigidbody2D>();
        if (!m_spriteRenderer)
            m_spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        FollowPlayerX();
    }

    private void FollowPlayerX()
    {
        float xMaxSpeed = moveSpeed * Time.deltaTime, xDir = playerTransform.position.x - transform.position.x, xVelocity = Mathf.Lerp(-xMaxSpeed, xMaxSpeed, xDir);
        bool currentlyMovingLeft = IsMovingLeft(xDir, lastMoveLeft);
        targetVelocity = new Vector2(xVelocity, 0);

        if(lastMoveLeft != currentlyMovingLeft)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            lastMoveLeft = currentlyMovingLeft;
        }

        //if (xDir < 0)
        //    m_spriteRenderer.flipX = true;
        //else
        //    m_spriteRenderer.flipX = false;
        m_rb.velocity = Vector2.SmoothDamp(m_rb.velocity, targetVelocity, ref currentVelocity, movementSmoothing);
    }

    private bool IsMovingLeft(float _xDir, bool _lastValue)
    {
        bool l = _lastValue;
        if (_xDir < 0)
            return true;
        else if(_xDir > 0)
            return false;
        return l;
    }
}
