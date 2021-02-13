using UnityEngine;

public class Goal : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] SceneSystem.GameScene newScene;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelManager levelManager = LevelManager.instance;
            GameLevel newLevel = levelManager.GetGameLevel(newScene);
            if(newLevel != null)
            {
                levelManager.AddToUnlockedLevels(newLevel);
                SaveSystem.SaveGame(levelManager.UnlockedLevels.ToArray());
            }
            gameManager.LoadScene(newScene);
        }
    }
}
