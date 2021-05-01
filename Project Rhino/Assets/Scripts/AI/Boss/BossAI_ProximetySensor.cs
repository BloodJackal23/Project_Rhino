using UnityEngine;

public class BossAI_ProximetySensor : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private Transform target;
    [SerializeField, Range(1f, 10f)] private float sensorRadius = 10f;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        target = GetTargetTransform();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sensorRadius);
    }

    private Transform GetTargetTransform()
    {
        Collider2D targetCollider = Physics2D.OverlapCircle(transform.position, sensorRadius, targetMask);
        if (targetCollider)
            return targetCollider.transform;
        return null;

    }
}
