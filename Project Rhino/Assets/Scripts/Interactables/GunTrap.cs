using System;
using UnityEngine;

public class GunTrap : MonoBehaviour
{
    [SerializeField] Transform emitter;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Animator animator;
    Projectile projectile;
    [SerializeField] bool gunActive = true;
    [SerializeField] float force = 5f;

    [Serializable]
    public class HazardData
    {
        [SerializeField] Transform playerSpawn;
        [SerializeField] GameObject[] objectsToHide;
        [SerializeField] GameObject[] objectsToShow;

        public void SetupHazard(PlayerHazard _hazard)
        {
            _hazard.SetHazard(playerSpawn, objectsToHide, objectsToShow);
        }
    }
    public HazardData hazardData;

    public void CreateProjectile()
    {
        projectile = Instantiate(projectilePrefab, emitter.position, emitter.rotation).GetComponent<Projectile>();
        projectile.Init();
        hazardData.SetupHazard(projectile);
        projectile.AddForce(emitter.right, force);
    }
}
