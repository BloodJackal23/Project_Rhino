using UnityEngine;

public class GunTrap : ProjectileEmitter
{
    [Header("Trap Members")]
    [SerializeField] private Animator m_Animator;
    [Space]

    [SerializeField] private HazardData hazardData;

    protected override void Awake()
    {
        base.Awake();
        if (!m_Animator)
            m_Animator = GetComponent<Animator>();
        m_Animator.speed = fireRate;
    }

    private void Update()
    {
        m_Animator.SetBool("canFire", canFire);
    }

    //Mainly used by the animator component
    public void Shoot()
    {
        Projectile spawnedProjectile = FireProjectile(projectilePrefab);
        spawnedProjectile.Init(m_emitter.right, force);
        if (spawnedProjectile.PlayerHazard)
            spawnedProjectile.PlayerHazard.SetHazardData(hazardData);
    }

    protected override Projectile FireProjectile(GameObject _prefab)
    {
        m_AudioSource.Play();
        return base.FireProjectile(_prefab);
    }

    public void SetActive(bool _isActive)
    {
        canFire = _isActive;
    }
}
