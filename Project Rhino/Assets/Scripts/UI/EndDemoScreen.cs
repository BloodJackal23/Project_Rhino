using UnityEngine;
using UnityEngine.UI;

public class EndDemoScreen : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Button backToMainButton;
    [SerializeField] Button quitGameButton;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        backToMainButton.onClick.AddListener(delegate { gameManager.LoadMainMenuScene(); });
        quitGameButton.onClick.AddListener(delegate { gameManager.QuitGame(); });
    }
}
