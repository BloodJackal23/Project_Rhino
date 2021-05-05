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

    protected virtual void Awake()
    {
        if (!m_AudioSource)
            m_AudioSource = GetComponent<AudioSource>();
    }

    protected Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, m_emitter.position, m_emitter.rotation).GetComponent<Projectile>();
        projectile.Init(m_emitter.right, force);
        return projectile;
    }
}
