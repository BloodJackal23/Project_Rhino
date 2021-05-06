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

    public void SetHazardData(Transform _spawn, GameObject[] _hide, GameObject[] _show)
    {
        playerSpawn = _spawn;
        objectsToHide = _hide;
        objectsToShow = _show;
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
