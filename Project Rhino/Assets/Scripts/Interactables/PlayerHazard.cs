using UnityEngine;

public class PlayerHazard : MonoBehaviour
{
    [SerializeField] Transform playerSpawn;
    [SerializeField] GameObject[] objectsToHide;
    [SerializeField] GameObject[] objectsToShow;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            Debug.Log("Player Hit!");
            SendPlayerToNewSpawn(collision.transform);
            HideObjects();
            ShowObjects();
        }
    }

    void SendPlayerToNewSpawn(Transform _playerTransform)
    {
        _playerTransform.position = playerSpawn.position;
    }

    void HideObjects()
    {
        foreach(GameObject gameObject in objectsToHide)
        {
            gameObject.SetActive(false);
        }
    }

    void ShowObjects()
    {
        foreach (GameObject gameObject in objectsToShow)
        {
            gameObject.SetActive(true);
        }
    }
}
