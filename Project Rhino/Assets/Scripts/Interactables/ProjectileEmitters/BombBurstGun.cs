using UnityEngine;

public class BombBurstGun : BurstEmitter
{
    [SerializeField, Range(0, 1f)] private float redBombPercentage = 0.5f, blueBombPercentage = 0.5f;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        blueBombPercentage = 1f - redBombPercentage;
    }

    protected override int GetProjectileIndex()
    {
        float rand = Random.Range(0, 1f);
        if (rand <= blueBombPercentage)
            return 1; //Blue bomb index
        return 0; //Red bomb index
    }
}
