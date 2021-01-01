using UnityEngine;

public class Goal : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] string newScene;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.LoadScene(newScene);
        }
    }
}
