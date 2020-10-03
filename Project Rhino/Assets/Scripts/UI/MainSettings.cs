using UnityEngine;
using UnityEngine.UI;

public class MainSettings : MonoBehaviour
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

    private void Start()
    {
        gameManager = GameManager.instance;
        audioSettings.InitAudioSettings(gameManager);        
        videoSettings.InitVideoSettings(gameManager);
    }
}
