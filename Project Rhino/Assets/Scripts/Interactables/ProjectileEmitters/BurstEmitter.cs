using UnityEngine;

public class BurstEmitter : ProjectileEmitter
{
    [Header("Burst Attributes")]
    [SerializeField] protected int burstCount = 4;

    [Header("Debugging")]
    [SerializeField] private bool fire = false;

    private int burstCounter = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Projectile spawnedProjectile = FireProjectile(Random.Range(0, 2));
            spawnedProjectile.Init(GetRandomScatterDirection(), force, randomSpinForce);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        SetLimitDirections();
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + GetDirectionFromRotation(startRot, Vector2.right, transform.parent.localScale) * 100);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + GetDirectionFromRotation(endRot, Vector2.right, transform.parent.localScale) * 100);
    }
}
