using UnityEngine;

public class BossBombGun : BurstEmitter
{
    [Header("Alternate Player Hazard Data")]
    [SerializeField] private GameObject altBombPrefab;
    [SerializeField] private HazardData altHazardData;
    [SerializeField, Range(0f, 1f)] float blueBombProbability = .1f;

    protected override void FireOneProjectile(GameObject _prefab, HazardData _hazardData)
    {
        float rand = Random.Range(0, 1f);
        Debug.Log("Random probability = " + rand);
        if (rand > blueBombProbability)
            base.FireOneProjectile(_prefab, _hazardData);
        else
            base.FireOneProjectile(altBombPrefab, altHazardData);
    }
}
