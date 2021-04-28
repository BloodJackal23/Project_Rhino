using System.Collections;
using UnityEngine;

public class MovingPlatformGroup : MonoBehaviour
{
    [SerializeField] private Transform m_path;
    [SerializeField] private Transform m_platform;
    private Rigidbody2D platformRb;
    private Transform[] pathPoints;
    IEnumerator activeCoroutine;

    [Header("Attributes")]
    [SerializeField, Range(1f, 5f)] private float moveSpeed = 2f;
    [SerializeField] private float pointProximetyThreshold = .2f;
    [SerializeField] private float waitTime = 1.5f;

    void Start()
    {
        if (!m_platform)
            m_platform = transform.Find("Platform");
        platformRb = m_platform.GetComponent<Rigidbody2D>();
        GetPath();
        activeCoroutine = PlatformMovementLoop();
        StartCoroutine(activeCoroutine);
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

    private IEnumerator PlatformMovementLoop()
    {
        float distance = 0;
        int pointIndex = 0;
        Vector2 dir;
        while (pointIndex < pathPoints.Length)
        {
            dir = pathPoints[pointIndex].position - m_platform.position;
            dir.Normalize();
            do
            {
                distance = Vector2.Distance(m_platform.position, pathPoints[pointIndex].position);
                platformRb.velocity = dir * moveSpeed;
                //m_platform.Translate(dir * moveSpeed * Time.fixedDeltaTime, transform);
                yield return new WaitForFixedUpdate();
            }
            while (distance > pointProximetyThreshold);
            pointIndex++;
            if (pointIndex >= pathPoints.Length)
                pointIndex = 0;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
