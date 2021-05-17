using System;
using UnityEngine;

[Serializable]
public class HazardData
{
    [SerializeField] private Transform playerSpawn;
    [SerializeField] private GameObject[] objectsToHide;
    [SerializeField] private GameObject[] objectsToShow;

    public Transform PlayerTransform { get { return playerSpawn; } }
    public GameObject[] ObjectsToHide { get { return objectsToHide; } }
    public GameObject[] ObjectsToShow { get { return objectsToShow; } }

    public void ClearAllData()
    {
        playerSpawn = null;
        objectsToHide = new GameObject[0];
        objectsToShow = new GameObject[0];
    }
}
