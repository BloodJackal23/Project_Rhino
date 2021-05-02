using UnityEngine;

public class BossAI_Controller: MonoBehaviour
{
    [Header("Memebers")]
    [SerializeField] private Rigidbody2D m_rb;
    [Space]

    [Header("Attributes")]
    [SerializeField, Range(1f, 1000f)] private float moveSpeed = 250f;
    [SerializeField] private float movementSmoothing = 0.05f;
    [SerializeField, Range(0.1f, 5f)] private float minFollowDistance = 3f;
    public bool huntingPlayer = false;

    #region Private Variables
    private Transform playerTransform;
    private Vector2 targetVelocity, currentVelocity = Vector2.zero;
    private float lastDirX = 0;
    #endregion

    private void Awake()
    {
        if (!m_rb)
            m_rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (huntingPlayer)
        {
            if(!playerTransform)
                SearchForPlayer();
        }
        else
            playerTransform = null;
    }

    void FixedUpdate()
    {
        if(playerTransform)
            FollowPlayer();
        else
        {
            if (m_rb.velocity != Vector2.zero)
                m_rb.velocity = Vector2.zero;
        }
    }

    private void MoveToTargetX(float _xDir)
    {
        float lerpedSpeedX = Mathf.Lerp(0, moveSpeed * Time.deltaTime, Mathf.Abs(_xDir) - minFollowDistance);
        targetVelocity = new Vector2(lerpedSpeedX * Mathf.Sign(_xDir), 0);
        m_rb.velocity = Vector2.SmoothDamp(m_rb.velocity, targetVelocity, ref currentVelocity, movementSmoothing);
    }

    private void SetLookByScale(float _xDir)
    {
        if (lastDirX != _xDir)
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    private void FollowPlayer()
    {
        float xDir = playerTransform.position.x - transform.position.x;
        float signedX = Mathf.Sign(xDir);
        MoveToTargetX(xDir);
        SetLookByScale(signedX);
        lastDirX = signedX;
    }

    private void SearchForPlayer()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
