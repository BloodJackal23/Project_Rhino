using UnityEngine;

public class MovingPlatformGroup : MonoBehaviour
{
    [SerializeField] private Transform m_path;
    [SerializeField] private Transform m_platform;
    private Rigidbody2D platformRb;
    private Transform[] pathPoints;

    [Header("Attributes")]
    [SerializeField, Range(1f, 5f)] private float moveSpeed = 2f;
    [SerializeField] private float pointProximetyThreshold = .2f;
    [SerializeField] private float waitTime = 1.5f;

    private bool isMoving = false;
    private float waitTimer = 0;
    private int pointIndex = 0;

    void Start()
    {
        if (!m_platform)
            m_platform = transform.Find("Platform");
        platformRb = m_platform.GetComponent<Rigidbody2D>();
        GetPath();
    }

    private void FixedUpdate()
    {
        if(isMoving)
        {
            if (ReachedDestination(m_platform.position, pathPoints[pointIndex].position, pointProximetyThreshold))
            {
                platformRb.velocity = Vector2.zero;
                isMoving = false;
            }
        }
        else
        {
            if (waitTimer < waitTime)
                waitTimer += Time.fixedDeltaTime;
            else
            {
                if (pointIndex < pathPoints.Length - 1)
                    pointIndex++;
                else
                    pointIndex = 0;
                platformRb.velocity = SetPlatformDirection(pointIndex) * moveSpeed;
                isMoving = true;
                waitTimer = 0;
            }
        }
    }

    private void GetPath()
    {
        if (!m_path)
            m_path = transform.Find("Path");
        pathPoints = new Transform[m_path.childCount];
        for (int i = 0; i < pathPoints.Length; i++)
        {
            pathPoints[i] = m_path.GetChild(i);
        }
    }

    private bool ReachedDestination(Vector2 _from, Vector2 _to, float _threshold)
    {
        if (Vector2.Distance(_from, _to) > _threshold)
            return false;
        return true;
    }

    private Vector2 SetPlatformDirection(int _pointIndex)
    {
        Vector2 dir = pathPoints[pointIndex].position - m_platform.position;
        dir.Normalize();
        return dir;
    }
}
