using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform m_path;
    [SerializeField] private Transform m_platform;
    private Transform[] pathPoints;
    IEnumerator activeCoroutine;

    [Header("Attributes")]
    [SerializeField, Range(1f, 5f)] private float moveSpeed = 2f;
    [SerializeField] private float pointProximetyThreshold = .2f;
    [SerializeField] private float waitTime = 1.5f;
    [SerializeField] private LayerMask targetMask;

    void Start()
    {
        if (!m_platform)
            m_platform = transform.Find("Platform");
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
                m_platform.Translate(dir * moveSpeed * Time.deltaTime, transform);
                yield return null;
            }
            while (distance > pointProximetyThreshold);
            pointIndex++;
            if (pointIndex >= pathPoints.Length)
                pointIndex = 0;
            yield return new WaitForSeconds(waitTime);
        }
    }
}
