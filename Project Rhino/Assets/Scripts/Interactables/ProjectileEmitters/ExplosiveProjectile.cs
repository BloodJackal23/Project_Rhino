using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [Header("Prefabs")]
    [SerializeField] private GameObject explosionEffectPrefab;
    [Space]

    [Header("Attributes")]
    [SerializeField] private float explosionRadius = 3f;

    protected override void OnDestroy()
    {
        base.OnDestroy();
        KillPlayerWithExplosion();
        SpawnExplosionEffectOnExplosion();
    }

    private void KillPlayerWithExplosion()
    {
        Collider2D[] targetColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, targetLayers);

        foreach(Collider2D target in targetColliders)
        {
            if (target.gameObject.tag == "Player")
            {
                m_playerHazard.KillPlayerButNotReally(target);
                return;
            }  
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void SpawnExplosionEffectOnExplosion()
    {
        GameObject fx = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(fx, 1.5f);
    }
}
