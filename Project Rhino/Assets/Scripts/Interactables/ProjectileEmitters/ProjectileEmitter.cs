using UnityEngine;

public class ProjectileEmitter : MonoBehaviour
{
    [Header("Members")]
    [SerializeField] protected Transform m_emitter;
    [SerializeField] protected AudioSource m_AudioSource;
    [Space]

    [SerializeField] protected GameObject projectilePrefab;

    [Header("Attributes")]
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected bool canFire = true;
    [SerializeField] protected float force = 5f;
    [SerializeField, Range(-180f, 180f)] private float startAngle = -60f, endAngle = -30f;

    private Quaternion startRot, endRot, currentRot;

    protected virtual void Awake()
    {
        if (!m_AudioSource)
            m_AudioSource = GetComponent<AudioSource>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        SetLimitDirections();
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + GetDirectionFromRotation(startRot, Vector2.right, transform.parent.localScale) * 100);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + GetDirectionFromRotation(endRot, Vector2.right, transform.parent.localScale) * 100);
    }

    protected Projectile CreateProjectile(bool _useScatterAngles)
    {
        Projectile projectile = Instantiate(projectilePrefab, m_emitter.position, m_emitter.rotation).GetComponent<Projectile>();
        if(_useScatterAngles)
            projectile.Init(GetRandomScatterDirection(), force);
        else
            projectile.Init(m_emitter.right, force);
        return projectile;
    }

    private Vector2 GetDirectionFromRotation(Quaternion _rotation, Vector2 _from, Vector2 _parentScale)
    {
        Vector2 newScale = new Vector2(Mathf.Sign(_parentScale.x), Mathf.Sign(_parentScale.y));
        return _rotation * _from * newScale;
    }

    private void SetLimitDirections()
    {
        startRot = Quaternion.Euler(0, 0, startAngle);
        endRot = Quaternion.Euler(0, 0, endAngle);
    }

    protected Vector2 GetRandomScatterDirection()
    {
        float randAngle = Random.Range(Mathf.Min(startAngle, endAngle), Mathf.Max(startAngle, endAngle));
        currentRot = Quaternion.Euler(0, 0, randAngle);
        return GetDirectionFromRotation(currentRot, Vector2.right, transform.parent.localScale);
    }
}
