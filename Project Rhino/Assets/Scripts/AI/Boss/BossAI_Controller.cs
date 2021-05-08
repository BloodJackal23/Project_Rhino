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
    private Transform targetTransform;
    private Vector2 targetVelocity, currentVelocity = Vector2.zero;
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
            if(!targetTransform)
                targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
            targetTransform = null;
    }

    void FixedUpdate()
    {
        if(targetTransform)
            MoveToTargetX(targetTransform.position.x - transform.position.x);
        else
        {
            if (m_rb.velocity != Vector2.zero)
                m_rb.velocity = Vector2.zero;
        }
    }

    private void MoveToTargetX(float _xDir)
    {
        float lerpedSpeedX = Mathf.Lerp(0, moveSpeed * Time.deltaTime, Mathf.Abs(_xDir) - minFollowDistance);
        float signedX = Mathf.Sign(_xDir);
        transform.localScale = new Vector2(signedX * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        targetVelocity = new Vector2(lerpedSpeedX * signedX, 0);
        m_rb.velocity = Vector2.SmoothDamp(m_rb.velocity, targetVelocity, ref currentVelocity, movementSmoothing);
    }
}
