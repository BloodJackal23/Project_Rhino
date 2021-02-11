using UnityEngine;

public class Goal : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] SceneLoadingSystem.GameScene newScene;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelManager levelManager = LevelManager.instance;
            LevelManager.GameLevel newLevel = levelManager.GetGameLevel(newScene);
            if(newLevel != null)
            {
                levelManager.AddToUnlockedLevels(newLevel);
            }
            gameManager.LoadScene(newScene);
        }
    }
}
