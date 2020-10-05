using UnityEngine;

public class Goal : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] int newSceneIndex = 0;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Hit!");
            gameManager.LoadScene(newSceneIndex);
        }
    }
}
