using UnityEngine;

public class MovingPlatformGroup : MonoBehaviour
{
    private Transform m_path;
    private Transform m_platform;
    private Rigidbody2D platformRb;
    private Transform[] pathPoints;

    [Header("Attributes")]
    [SerializeField, Range(1f, 5f)] private float moveSpeed = 2f;
    [SerializeField] private float pointProximetyThreshold = .2f;
    [SerializeField] private float waitTime = 1.5f;

    private bool isMoving = false;
    private float waitTimer = 0;
    private int pointIndex = 0;

    void Awake()
    {
        if (!m_platform)
        {
            m_platform = GetTransform("Platform");
        }
            
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
                pointIndex++;
                pointIndex %=  pathPoints.Length;
                platformRb.velocity = SetPlatformDirection(pointIndex) * moveSpeed;
                isMoving = true;
                waitTimer = 0;
            }
        }
    }

    private Transform GetTransform(string _tag)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.tag == _tag)
                return child;
        }
        Debug.LogError("Platform not found! Check that the platform object has the " + _tag + " tag assigned to it");
        return null;
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
