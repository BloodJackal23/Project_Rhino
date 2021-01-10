using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : UI_Panel
{
    #region Audio Elements
    [Header("Audio Settings UI Components")]
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider fxVolumeSlider;
    [SerializeField] Button backAudioSettingsButton;
    [SerializeField] Button defaultAudioSettingsButton;
    #endregion

    protected override void Start()
    {
        base.Start();
    }

    private void OnEnable()
    {
        GameManager gameManager = GameManager.instance;
        SetSlidersValues(gameManager);
        AddListenersToAudioSliders(gameManager);
        defaultAudioSettingsButton.onClick.AddListener(delegate { gameManager.SetToDefaultSettings(); });
    }

    private void OnDisable()
    {
        GameManager gameManager = GameManager.instance;
        RemoveListenersToAudioSliders(gameManager);
        defaultAudioSettingsButton.onClick.RemoveListener(delegate { gameManager.SetToDefaultSettings(); });
    }

    #region Audio Settings Methods

    #region UI Listeners Methods
    void OnMasterSliderChanged(GameManager _gameManager)
    {
        _gameManager.SetMasterVolume(masterVolumeSlider.value);
        _gameManager.SaveGameSettings();
    }

    void OnMusicSliderChanged(GameManager _gameManager)
    {
        _gameManager.SetMusicVolume(musicVolumeSlider.value);
        _gameManager.SaveGameSettings();
    }

    void OnFXSliderChanged(GameManager _gameManager)
    {
        _gameManager.SetFXVolume(fxVolumeSlider.value);
        _gameManager.SaveGameSettings();
    }

    void AddListenersToAudioSliders(GameManager _gameManager)
    {
        masterVolumeSlider.onValueChanged.AddListener(delegate { OnMasterSliderChanged(_gameManager); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicSliderChanged(_gameManager); });
        fxVolumeSlider.onValueChanged.AddListener(delegate { OnFXSliderChanged(_gameManager); });
    }

    void RemoveListenersToAudioSliders(GameManager _gameManager)
    {
        masterVolumeSlider.onValueChanged.RemoveListener(delegate { OnMasterSliderChanged(_gameManager); });
        musicVolumeSlider.onValueChanged.RemoveListener(delegate { OnMusicSliderChanged(_gameManager); });
        fxVolumeSlider.onValueChanged.RemoveListener(delegate { OnFXSliderChanged(_gameManager); });
    }

    void SetSlidersValues(GameManager _gameManager)
    {
        GameSettings gameSettings = _gameManager.gameSettings;
        masterVolumeSlider.value = _gameManager.GetMasterVolume(gameSettings);
        musicVolumeSlider.value = _gameManager.GetMusicVolume(gameSettings);
        fxVolumeSlider.value = _gameManager.GetFXVolume(gameSettings);
    }
    #endregion
    #endregion
}
