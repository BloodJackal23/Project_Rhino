using UnityEngine;

public class Boss_AI : MonoBehaviour
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
    Vector2 targetVelocity;
    Vector2 currentVelocity = Vector2.zero;
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
        float currentSpeed = Mathf.Lerp(0, moveSpeed * Time.deltaTime, PlayerDistanceX());
        float xDirection = PlayerRelativePosX();
        targetVelocity = new Vector2(currentSpeed * xDirection, 0);
        if (xDirection < 0)
            m_spriteRenderer.flipX = true;
        else
            m_spriteRenderer.flipX = false;
        m_rb.velocity = Vector2.SmoothDamp(m_rb.velocity, targetVelocity, ref currentVelocity, movementSmoothing);
    }

    private float PlayerDistanceX()
    {
        return Mathf.Abs(transform.position.x - playerTransform.position.x);
    }

    private float PlayerRelativePosX()
    {
        return Mathf.Sign(playerTransform.position.x - transform.position.x);
    }
}
