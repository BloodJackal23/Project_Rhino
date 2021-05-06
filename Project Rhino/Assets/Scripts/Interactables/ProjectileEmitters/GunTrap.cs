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
        Projectile spawnedProjectile = FireProjectile(0);
        spawnedProjectile.Init(m_emitter.right, force);
        if (spawnedProjectile.PlayerHazard)
            hazardData.SetHazard(spawnedProjectile.PlayerHazard);
    }
}
