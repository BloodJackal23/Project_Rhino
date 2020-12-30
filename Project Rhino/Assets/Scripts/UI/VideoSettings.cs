using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoSettings : UI_Panel
{
    #region Video Elements
    [Header("Video Settings UI Components")]
    [SerializeField] Toggle fullScreenToggle;
    [SerializeField] TMP_Dropdown resolutionDropDown, textureQualityDropdown, vSyncDropdown;
    [SerializeField] Button backVideoSettingsButton;
    [SerializeField] Button defaultVideoSettingsButton;

    public Resolution[] resolutionOptions;
    #endregion

    protected override void OnValidate()
    {
        base.OnValidate();
    }

    private void OnEnable()
    {
        GameManager gameManager = GameManager.instance;
        GetResolutionOptions();
        SetResolutionDropdown();
        SetVideoSettings(gameManager);
        AddListenersToVideoToggles(gameManager);
        AddListenersToVideoDropdowns(gameManager);
        defaultVideoSettingsButton.onClick.AddListener(delegate { gameManager.SetToDefaultSettings(); });
    }

    private void OnDisable()
    {
        GameManager gameManager = GameManager.instance;
        fullScreenToggle.onValueChanged.RemoveListener(delegate { OnFullScreenToggle(gameManager); });
        RemoveListenersToVideoDropdowns(gameManager);
        defaultVideoSettingsButton.onClick.RemoveListener(delegate { gameManager.SetToDefaultSettings(); });
        resolutionDropDown.ClearOptions();
    }

    #region Video Settings Methods
    void SetVideoSettings(GameManager _gameManager)
    {
        GameSettings gameSettings = _gameManager.gameSettings;
        fullScreenToggle.isOn = _gameManager.GetFullScreen(gameSettings);
        resolutionDropDown.value = _gameManager.GetResolution(gameSettings);
        resolutionDropDown.captionText.text = resolutionDropDown.options[resolutionDropDown.value].text; //This is just to fix a minor bug where the selected option for the resolution dropdown appears blank after resetting to default settings
        textureQualityDropdown.value = _gameManager.GetTextureQuality(gameSettings);
        vSyncDropdown.value = _gameManager.GetVSync(gameSettings);
    }

    void OnFullScreenToggle(GameManager _gameManager)
    {
        _gameManager.SetFullScreen(fullScreenToggle.isOn);
        _gameManager.SaveGameSettings();
    }

    void GetResolutionOptions()
    {
        resolutionOptions = Screen.resolutions;
    }

    void SetResolutionDropdown()
    {
        foreach(Resolution resolution in resolutionOptions)
        {
            string optText = resolution.width.ToString() + " X " + resolution.height.ToString();
            TMP_Dropdown.OptionData newOption = new TMP_Dropdown.OptionData(optText);
            resolutionDropDown.options.Add(newOption);
        }
    }

    void OnResoltuionChanged(GameManager _gameManager)
    {
        _gameManager.SetResolution(resolutionDropDown.value);
        _gameManager.SaveGameSettings();
    }

    void OnTextureQualityChanged(GameManager _gameManager)
    {
        _gameManager.SetTextureQuality(textureQualityDropdown.value);
        _gameManager.SaveGameSettings();
    }

    void OnVSyncChanged(GameManager _gameManager)
    {
        _gameManager.SetVSync(vSyncDropdown.value);
        _gameManager.SaveGameSettings();
    }

    void AddListenersToVideoToggles(GameManager _gameManager)
    {
        fullScreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(_gameManager); });
    }

    void AddListenersToVideoDropdowns(GameManager _gameManager)
    {
        resolutionDropDown.onValueChanged.AddListener(delegate { OnResoltuionChanged(_gameManager); });
        textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChanged(_gameManager); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChanged(_gameManager); });
    }

    void RemoveListenersToVideoDropdowns(GameManager _gameManager)
    {
        resolutionDropDown.onValueChanged.RemoveListener(delegate { OnResoltuionChanged(_gameManager); });
        textureQualityDropdown.onValueChanged.RemoveListener(delegate { OnTextureQualityChanged(_gameManager); });
        vSyncDropdown.onValueChanged.RemoveListener(delegate { OnVSyncChanged(_gameManager); });
    }
    #endregion
}
