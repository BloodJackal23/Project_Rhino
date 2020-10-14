﻿using UnityEngine;

public class PlayerHazard : MonoBehaviour
{
    [SerializeField] protected Transform playerSpawn;
    [SerializeField] protected GameObject[] objectsToHide;
    [SerializeField] protected GameObject[] objectsToShow;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            Debug.Log("Player Hit!");
            SendPlayerToNewSpawn(collision.transform);
            HideObjects();
            ShowObjects();
        }
    }

    public void SetHazard(Transform _spawn, GameObject[] _hide, GameObject[] _show)
    {
        playerSpawn = _spawn;
        objectsToHide = _hide;
        objectsToShow = _show;
    }

    void SendPlayerToNewSpawn(Transform _playerTransform)
    {
        _playerTransform.position = playerSpawn.position;
    }

    void HideObjects()
    {
        if(objectsToHide.Length > 0)
        {
            foreach (GameObject _gameObject in objectsToHide)
            {
                if(_gameObject)
                {
                    _gameObject.SetActive(false);
                }
            }
        }
    }

    void ShowObjects()
    {
        if(objectsToShow.Length > 0)
        {
            foreach (GameObject _gameObject in objectsToShow)
            {
                if(_gameObject)
                {
                    _gameObject.SetActive(true);
                }
            }
        }
    }
}