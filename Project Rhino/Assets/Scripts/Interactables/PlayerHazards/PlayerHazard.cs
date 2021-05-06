using UnityEngine;

public class PlayerHazard : MonoBehaviour
{
    [SerializeField] protected Transform playerSpawn;
    [SerializeField] protected GameObject[] objectsToHide;
    [SerializeField] protected GameObject[] objectsToShow;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        KillPlayerButNotReally(collision);
    }

    public void SetHazardData(Transform _spawn, GameObject[] _hide, GameObject[] _show)
    {
        playerSpawn = _spawn;
        objectsToHide = _hide;
        objectsToShow = _show;
    }

    private void HideObjects()
    {
        if(objectsToHide.Length > 0)
        {
            foreach (GameObject _gameObject in objectsToHide)
            {
                if(_gameObject)
                    _gameObject.SetActive(false);
            }
        }
    }

    private void ShowObjects()
    {
        if(objectsToShow.Length > 0)
        {
            foreach (GameObject _gameObject in objectsToShow)
            {
                if(_gameObject)
                    _gameObject.SetActive(true);
            }
        }
    }

    public void KillPlayerButNotReally(Collider2D _hitCollider)
    {
        if (_hitCollider.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit!");
            PlayerController playerController = _hitCollider.GetComponent<PlayerController>();
            playerController.onDeath?.Invoke();
            playerController.transform.position = playerSpawn.position;
            HideObjects();
            ShowObjects();
        }
    }
}
