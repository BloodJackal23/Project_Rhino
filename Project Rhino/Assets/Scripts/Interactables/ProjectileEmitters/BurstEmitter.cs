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
            CreateProjectile(GetRandomScatterDirection(), Random.Range(0, 2));
            spawnedProjectile.rigidbody.AddTorque(Random.Range(-randomSpinForce, randomSpinForce), ForceMode2D.Impulse);
            spawnedProjectile = null;
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
