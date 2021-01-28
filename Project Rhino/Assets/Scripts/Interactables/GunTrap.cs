using System;
using UnityEngine;

public class GunTrap : MonoBehaviour
{
    [SerializeField] Transform emitter;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Animator m_Animator;
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] float fireAnimSpeed = 1f;
    [SerializeField] bool gunActive = true;
    [SerializeField] float force = 5f;

    [SerializeField] HazardData hazardData;

    private void OnEnable()
    {
        if(!m_Animator)
        {
            m_Animator = GetComponent<Animator>();
        }

        if(!m_AudioSource)
        {
            m_AudioSource = GetComponent<AudioSource>();
        }
        m_Animator.speed = fireAnimSpeed;
    }

    public void CreateProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, emitter.position, emitter.rotation).GetComponent<Projectile>();
        projectile.Init();
        hazardData.SetupHazard(projectile);
        projectile.AddForce(emitter.right, force);
    }

    //Mainly used by the animator component
    void Shoot()
    {
        CreateProjectile();
        m_AudioSource.Play();
    }
}
