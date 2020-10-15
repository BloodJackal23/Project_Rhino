using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button newGame;
    [SerializeField] Button howToPlay;
    // Start is called before the first frame update
    void Start()
    {
        newGame.onClick.AddListener(delegate { GameManager.instance.LoadScene("Level_01"); });
        //howToPlay.onClick.AddListener(delegate { GameManager.instance.LoadScene("Tutorial"); }); //TODO: Make a tutorial level
    }
}
