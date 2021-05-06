using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private float explosionRadius = 3f;

    protected override void OnDestroy()
    {
        base.OnDestroy();
        KillPlayerWithExplosion();
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
}
