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
        {
            m_Animator = GetComponent<Animator>();
        }
        m_Animator.speed = fireRate;
    }

    private void Update()
    {
        m_Animator.SetBool("canFire", canFire);
    }

    //Mainly used by the animator component
    public void Shoot()
    {
        CreateProjectile(m_emitter.right, 0);
        hazardData.SetupHazard(spawnedProjectile);
        m_AudioSource.Play();
        spawnedProjectile = null;
    }
}
