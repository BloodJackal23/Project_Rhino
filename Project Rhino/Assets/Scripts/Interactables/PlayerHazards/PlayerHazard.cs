using UnityEngine;

public class PlayerHazard : MonoBehaviour
{
    [SerializeField] protected bool killPlayerOnTriggerEnter = true;
    [SerializeField] protected Transform playerSpawn;
    [SerializeField] protected GameObject[] objectsToHide;
    [SerializeField] protected GameObject[] objectsToShow;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(killPlayerOnTriggerEnter && collision.gameObject.tag == "Player")
            KillPlayerButNotReally(collision);
    }

    public void SetHazardData(HazardData _hazardData)
    {
        playerSpawn = _hazardData.PlayerTransform;
        objectsToHide = _hazardData.ObjectsToHide;
        objectsToShow = _hazardData.ObjectsToShow;
    }

    private void SetObjectsActivationStatus(GameObject[] _objects, bool _active)
    {
        foreach(GameObject _gameObject in _objects)
        {
            if (_gameObject)
                _gameObject.SetActive(_active);
        }
    }

    public void KillPlayerButNotReally(Collider2D _hitCollider)
    {
        PlayerController playerController = _hitCollider.GetComponent<PlayerController>();
        playerController.onDeath?.Invoke();
        playerController.transform.position = playerSpawn.position;
        SetObjectsActivationStatus(objectsToHide, false);
        SetObjectsActivationStatus(objectsToShow, true);
    }
}
