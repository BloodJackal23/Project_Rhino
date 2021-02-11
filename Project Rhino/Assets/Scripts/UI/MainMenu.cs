using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UI_Panel
{
    [SerializeField] Button newGame;
    [SerializeField] Button howToPlay;

    protected override void Start()
    {
        base.Start();
        newGame.onClick.RemoveAllListeners();
        howToPlay.onClick.RemoveAllListeners();
        newGame.onClick.AddListener(delegate { GameManager.instance.LoadScene(SceneLoadingSystem.GameScene.Level_01); });
        howToPlay.onClick.AddListener(delegate { GameManager.instance.LoadScene(SceneLoadingSystem.GameScene.Tutorial); });
    }
}
