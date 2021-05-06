using UnityEngine;

public class BossAI_ProximitySensor : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask;
    [SerializeField, Range(1f, 10f)] private float sensorRadius = 10f;

    public Transform Target { get; private set; }

    public delegate void OnTargetDetected();
    public OnTargetDetected onTargetDetected;

    public delegate void WhileTargetDetected();
    public WhileTargetDetected whileTargetDetected;

    public delegate void OnTargetLost();
    public OnTargetLost onTargetLost;

    private void OnEnable()
    {
        onTargetDetected += DebugOnTargetDetected;
        onTargetLost += DebugOnTargetLost;
    }

    private void OnDisable()
    {
        onTargetDetected = null;
        whileTargetDetected = null;
        onTargetLost = null;

        Target = null;
    }

    private void Update()
    {
        if (Target)
            whileTargetDetected?.Invoke();
    }

    private void FixedUpdate()
    {
        RunSensor();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sensorRadius);
    }

    private void RunSensor()
    {
        bool currentTargetState = Target == null ? false : true;
        Collider2D targetCollider = Physics2D.OverlapCircle(transform.position, sensorRadius, targetMask);
        if (targetCollider)
        {
            Target = targetCollider.transform;
            if (!currentTargetState)
            {
                onTargetDetected?.Invoke();
            }
        }
        else
        {
            if (currentTargetState)
            {
                onTargetLost?.Invoke();
            }
            Target = null;
        }
        
    }

    void DebugOnTargetDetected()
    {
        Debug.Log(Target.gameObject.name + " has been detected!");
    }

    void DebugOnTargetLost()
    {
        Debug.Log(Target.gameObject.name + " is lost!");
    }
}
