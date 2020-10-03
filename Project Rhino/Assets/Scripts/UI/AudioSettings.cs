using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    #region Audio Elements
    [Header("Audio Settings UI Components")]
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider fxVolumeSlider;
    [SerializeField] Button backAudioSettingsButton;
    [SerializeField] Button saveAudioSettingsButton;
    #endregion

    #region Audio Settings Methods
    public void InitAudioSettings(GameManager _gameManager)
    {
        GameSettings gameSettings = _gameManager.gameSettings;
        masterVolumeSlider.value = _gameManager.SetMasterVolume(gameSettings);
        musicVolumeSlider.value = _gameManager.SetMusicVolume(gameSettings);
        fxVolumeSlider.value = _gameManager.SetFXVolume(gameSettings);
        AddListenersToAudioSliders(_gameManager);
        saveAudioSettingsButton.onClick.AddListener(delegate { _gameManager.SaveGameSettings(); });
    }

    #region UI Listeners Methods
    void OnMasterSliderChanged(GameManager _gameManager)
    {
        _gameManager.SetMasterVolume(masterVolumeSlider.value);
    }

    void OnMusicSliderChanged(GameManager _gameManager)
    {
        _gameManager.SetMusicVolume(musicVolumeSlider.value);
    }

    void OnFXSliderChanged(GameManager _gameManager)
    {
        _gameManager.SetFXVolume(fxVolumeSlider.value);
    }

    void AddListenersToAudioSliders(GameManager _gameManager)
    {
        masterVolumeSlider.onValueChanged.AddListener(delegate { OnMasterSliderChanged(_gameManager); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicSliderChanged(_gameManager); });
        fxVolumeSlider.onValueChanged.AddListener(delegate { OnFXSliderChanged(_gameManager); });
    }
    #endregion
    #endregion
}
