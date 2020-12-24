using UnityEngine;
using UnityEngine.UI;

public class MainSettings : UI_Panel
{
    GameManager gameManager;
    #region Main Settings Menu
    [Header("Main Settings UI Components")]
    [SerializeField] Button videoSettingsButton;
    [SerializeField] Button audioSettingsButton;
    [SerializeField] Button backButton;
    #endregion

    [SerializeField] VideoSettings videoSettings;
    [SerializeField] AudioSettings audioSettings;

    protected override void OnValidate()
    {
        base.OnValidate();
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        audioSettings.InitAudioSettings(gameManager);        
        videoSettings.InitVideoSettings(gameManager);
    }
}
