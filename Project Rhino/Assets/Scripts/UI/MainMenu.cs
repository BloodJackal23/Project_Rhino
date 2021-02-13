using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : UI_Panel
{
    #region Buttons
    [Header("Buttons")]
    [SerializeField] private Button newGame;
    [SerializeField] private Button howToPlay;
    [Space]
    #endregion

    #region Other Panels
    [Header("Other Panels")]
    [SerializeField] private GameObject saveWarningPanel;
    #endregion

    protected override void Start()
    {
        base.Start();
        newGame.onClick.RemoveAllListeners();
        howToPlay.onClick.RemoveAllListeners();

        newGame.onClick.AddListener(delegate { CheckForSaveFileBeforeNewGame(); });
        howToPlay.onClick.AddListener(delegate { GameManager.instance.LoadScene(SceneSystem.GameScene.Tutorial); });
    }

    private void CheckForSaveFileBeforeNewGame()
    {
        if (File.Exists(SaveSystem.Path))
        {
            saveWarningPanel.SetActive(true);
        }
        else
        {
            GameManager.instance.LoadScene(SceneSystem.GameScene.Level_01);
        }
    }
}
