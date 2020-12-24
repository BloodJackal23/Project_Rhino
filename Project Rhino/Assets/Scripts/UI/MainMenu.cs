using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UI_Panel
{
    [SerializeField] Button newGame;
    [SerializeField] Button howToPlay;

    protected override void OnValidate()
    {
        base.OnValidate();
        newGame.onClick.AddListener(delegate { GameManager.instance.LoadScene("Level_01"); });
        howToPlay.onClick.AddListener(delegate { GameManager.instance.LoadScene("Tutorial"); });
    }
}
