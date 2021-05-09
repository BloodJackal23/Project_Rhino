using System.Collections;
using UnityEngine;

public class BurstEmitter : ProjectileEmitter
{
    public delegate void OnBurstStart();
    public OnBurstStart onBurstStart;

    public delegate void OnBurstEnd();
    public OnBurstEnd onBurstEnd;

    [Header("Burst Attributes")]
    [SerializeField] protected int burstCount = 4;
    [Space]

    [SerializeField] protected HazardData hazardData;

    private bool isFiring = false;

    public bool IsFiring { get { return isFiring; } }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        SetLimitDirections();
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + GetDirectionFromRotation(startRot, Vector2.right, transform.parent.localScale) * 100);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + GetDirectionFromRotation(endRot, Vector2.right, transform.parent.localScale) * 100);
    }

    protected virtual void FireOneProjectile(GameObject _prefab, HazardData _hazardData)
    {
        Projectile spawnedProjectile = FireProjectile(_prefab);
        spawnedProjectile.Init(GetRandomScatterDirection(), force, randomSpinForce);
        if (spawnedProjectile.PlayerHazard)
            _hazardData.SetHazard(spawnedProjectile.PlayerHazard);
    }

    private IEnumerator FireSequence()
    {
        onBurstStart?.Invoke();
        int burstCounter = 0;
        float fireRateTimer = fireRate;
        isFiring = true;
        do
        {
            while(fireRateTimer < 1f / fireRate)
            {
                fireRateTimer += Time.deltaTime;
                yield return null;
            }
            fireRateTimer = 0;
            burstCounter++;
            FireOneProjectile(projectilePrefab, hazardData);
        }
        while (burstCounter < burstCount);
        isFiring = false;
        onBurstEnd?.Invoke();
    }

    public void FireBurst()
    {
        if (!isFiring)
            StartCoroutine(FireSequence());
    }
}
