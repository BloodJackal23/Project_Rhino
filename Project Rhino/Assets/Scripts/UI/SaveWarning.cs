using UnityEngine;
using UnityEngine.UI;

public class SaveWarning : UI_Panel
{
    [SerializeField] private Button yesButton;

    protected override void Start()
    {
        base.Start();
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(delegate { StartNewGame(); });
    }

    public void StartNewGame()
    {
        SaveSystem.DeleteSaveFile(SaveSystem.Path);
        GameManager.instance.LoadScene(SceneSystem.GameScene.Level_01);
    }
}
