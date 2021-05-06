using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private float explosionRadius = 3f;

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
}
