using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    #region Video Elements
    [Header("Video Settings UI Components")]
    [SerializeField] Toggle fullScreenToggle;
    [SerializeField] Dropdown resolutionDropDown, textureQualityDropdown, antiAliasingDropdown, vSyncDropdown;
    [SerializeField] Button backVideoSettingsButton;
    [SerializeField] Button saveVideoSettingsButton;

    public Resolution[] resolutionOptions;
    #endregion

    #region Video Settings Methods
    public void InitVideoSettings(GameManager _gameManager)
    {
        GameSettings gameSettings = _gameManager.gameSettings;
        fullScreenToggle.isOn = _gameManager.SetFullScreen(gameSettings);
        GetResolutionOptions();
        SetResolutionDropdown();
        resolutionDropDown.value = _gameManager.SetResolution(gameSettings);
        textureQualityDropdown.value = _gameManager.SetTextureQuality(gameSettings);
        antiAliasingDropdown.value = _gameManager .SetAntiAliasing(gameSettings);
        vSyncDropdown.value = _gameManager .SetVSync(gameSettings);
        AddListenersToVideoToggles(_gameManager);
        AddListenersToVideoDropdowns(_gameManager);
        saveVideoSettingsButton.onClick.AddListener(delegate { _gameManager.SaveGameSettings(); });
    }

    void OnFullScreenToggle(GameManager _gameManager)
    {
        _gameManager.SetFullScreen(fullScreenToggle.isOn);
    }

    void GetResolutionOptions()
    {
        resolutionOptions = Screen.resolutions;
    }

    void SetResolutionDropdown()
    {
        foreach(Resolution resolution in resolutionOptions)
        {
            Dropdown.OptionData newOption = new Dropdown.OptionData(resolution.ToString());
            resolutionDropDown.options.Add(newOption);
        }
    }

    void OnResoltuionChanged(GameManager _gameManager)
    {
        _gameManager.SetResolution(resolutionDropDown.value);
    }

    void OnTextureQualityChanged(GameManager _gameManager)
    {
        _gameManager.SetTextureQuality(textureQualityDropdown.value);
    }

    void OnAntiAliasingChanged(GameManager _gameManager)
    {
        _gameManager.SetAntiAliasing(antiAliasingDropdown.value);
    }

    void OnVSyncChanged(GameManager _gameManager)
    {
        _gameManager.SetVSync(vSyncDropdown.value);
    }

    void AddListenersToVideoToggles(GameManager _gameManager)
    {
        fullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(_gameManager); });
    }

    void AddListenersToVideoDropdowns(GameManager _gameManager)
    {
        resolutionDropDown.onValueChanged.AddListener(delegate { OnResoltuionChanged(_gameManager); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChanged(_gameManager); });
        antiAliasingDropdown.onValueChanged.AddListener(delegate { OnAntiAliasingChanged(_gameManager); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChanged(_gameManager); });
    }
    #endregion
}
