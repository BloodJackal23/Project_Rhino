using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button newGame;
    // Start is called before the first frame update
    void Start()
    {
        newGame.onClick.AddListener(delegate { GameManager.instance.LoadGameScene(); });
    }
}
