using UnityEngine;

public class ProjectileEmitter : MonoBehaviour
{
    [Header("Members")]
    [SerializeField] protected Transform m_emitter;
    [SerializeField] protected AudioSource m_AudioSource;
    [Space]

    [SerializeField] protected GameObject[] projectilePrefabs;

    [Header("Attributes")]
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected bool canFire = true;
    [SerializeField] protected float force = 5f;
    [SerializeField, Range(0, 2.5f)] protected float randomSpinForce = 0;
    [SerializeField, Range(-180f, 180f)] private float startAngle = -60f, endAngle = -30f;

    protected Projectile spawnedProjectile;
    protected Quaternion startRot, endRot, currentRot;

    protected virtual void Awake()
    {
        if (!m_AudioSource)
            m_AudioSource = GetComponent<AudioSource>();
    }

    protected virtual void CreateProjectile(Vector2 _direction, int _prefabIndex)
    {
        spawnedProjectile = Instantiate(projectilePrefabs[_prefabIndex], m_emitter.position, m_emitter.rotation).GetComponent<Projectile>();
        spawnedProjectile.Init(_direction, force);
        //if(_useScatterAngles)
        //    projectile.Init(GetRandomScatterDirection(), force);
        //else
        //    projectile.Init(m_emitter.right, force);
        //return projectile;
    }

    protected Vector2 GetDirectionFromRotation(Quaternion _rotation, Vector2 _from, Vector2 _parentScale)
    {
        Vector2 newScale = new Vector2(Mathf.Sign(_parentScale.x), Mathf.Sign(_parentScale.y));
        return _rotation * _from * newScale;
    }

    protected void SetLimitDirections()
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
