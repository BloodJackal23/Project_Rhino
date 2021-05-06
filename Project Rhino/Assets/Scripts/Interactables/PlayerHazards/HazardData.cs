using System;
using UnityEngine;

[Serializable]
public class HazardData
{
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private GameObject[] objectsToHide;
    [SerializeField] private GameObject[] objectsToShow;

    public void SetHazard(PlayerHazard _hazard)
    {
        _hazard.SetHazardData(playerSpawn, objectsToHide, objectsToShow);
    }
}
