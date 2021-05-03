using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public delegate void OnLaserStart();
    public OnLaserStart onLaserStart;

    public delegate void OnLaserEnd();
    public OnLaserEnd onLaserEnd;

    [Header("Members")]
    [SerializeField] private LineRenderer m_lineRenderer;
    [Space]

    [Header("Attributes")]
    [SerializeField] private LayerMask whatToHit;
    [SerializeField] private Vector2 startVector = Vector2.down, endVector = Vector2.right;
    [SerializeField] private float startAngle = 60f, endAngle = 30f, maxDistance = 100f;
    [SerializeField] private string targetTag = "Player";

    private Quaternion startRot, endRot;

    private void Awake()
    {
        if (!m_lineRenderer)
            m_lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        startRot = Quaternion.Euler(0, 0, -startAngle);
        endRot = Quaternion.Euler(0, 0, -endAngle);
        transform.rotation = startRot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        startRot = Quaternion.Euler(0, 0, -startAngle);
        endRot = Quaternion.Euler(0, 0, -endAngle);

        float sinStart = Mathf.Sin(-startAngle * Mathf.Deg2Rad), cosStart = Mathf.Cos(-startAngle * Mathf.Deg2Rad), sinEnd = Mathf.Sin(-endAngle * Mathf.Deg2Rad), cosEnd = Mathf.Cos(-endAngle * Mathf.Deg2Rad);
        Vector2 startDir = new Vector2(-cosStart * (1 / sinStart), sinStart * (1 / cosStart));
        Vector2 endDir = new Vector2(-cosEnd * (1 / sinEnd), sinEnd * (1 / cosEnd));

        Gizmos.DrawLine(transform.position, startDir * maxDistance);
        Gizmos.DrawLine(transform.position, endDir * maxDistance);

        Gizmos.color = Color.blue;
        Vector2 startPerp = Vector2.Perpendicular(startDir);

        Gizmos.DrawLine(transform.position, startPerp * maxDistance);

    }

    private void RaycastStart()
    {
        
    }
}
