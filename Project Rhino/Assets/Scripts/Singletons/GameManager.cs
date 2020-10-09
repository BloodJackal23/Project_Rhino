using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Game Settings Variables
    public GameSettings gameSettings { get; private set; }
    #endregion

    #region Audio Variables
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] MusicLibrary musicLibrary;
    [SerializeField] AudioSource musicSource, FX_Source;

    [SerializeField] SceneMusic sceneMusic;
    public enum AudioChannels { MasterVol, MusicVol, FX_Vol }
    #endregion

    public delegate void OnGamePaused();
    public OnGamePaused gamePausedDelegate;

    public delegate void OnGameResumed();
    public OnGamePaused gameResumedDelegate;
    public bool gamePaused { get; private set; }

    protected override void Awake()
    {
        dontDestroyOnLoad = true;
        gamePaused = false;
        base.Awake();
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable Called!");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        Debug.Log("Start Called!");
        GetSceneMusic();
        InitGameSettings();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #region Scene Management
    public void LoadScene(int _index)
    {
        SceneManager.LoadScene(_index);
    }

    public void LoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }

    public void LoadMainMenuScene()
    {
        LoadScene(0);
    }

    public void LoadFirstLevel()
    {
        LoadScene(1);
    }

    void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        Debug.Log("New scene loaded");
        GetSceneMusic();
        if (sceneMusic.playNewTrack)
        {
            InitSceneSoundtrack();
        }
    }
    #endregion

    public void QuitGame()
    {
        Debug.Log("Game exited...");
        Application.Quit();
    }

    #region Settings Main Methods
    void InitGameSettings()
    {
        LoadGameSettings();
        InitVideoSettings(gameSettings);
        InitAudioSettings(gameSettings);
    }

    public void SaveGameSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/GameSettings.json", jsonData);
        Debug.Log("Game Settings saved at " + Application.persistentDataPath + "/GameSettings.json");
    }

    void LoadGameSettings()
    {
        string path = Application.persistentDataPath + "/GameSettings.json";
        if (File.Exists(path))
        {
            gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(path));
        }
        else
        {
            gameSettings = new GameSettings(false, 0, 0, 0, 1, 7, 5, 5);
            SaveGameSettings();
        }
    }
    #endregion

    #region Video Settings Methods
    public void InitVideoSettings(GameSettings _gameSettings)
    {
        SetFullScreen(_gameSettings);
        SetResolution(_gameSettings);
        SetTextureQuality(_gameSettings);
        SetAntiAliasing(_gameSettings);
        SetVSync(_gameSettings);
    }

    public bool SetFullScreen(GameSettings _gameSettings) //Gets value from Game Settings
    {
        Screen.fullScreen = _gameSettings.fullScreen;
        return _gameSettings.fullScreen;
    }

    public bool SetFullScreen(bool _value) //Gets value from external source
    {
        Screen.fullScreen = gameSettings.fullScreen = _value;
        return Screen.fullScreen;
    }

    public int SetResolution(GameSettings _gameSettings) //Gets value from Game Settings
    {
        Resolution[] resolutions = Screen.resolutions;
        int index = _gameSettings.resolutionIndex;
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen);
        return index;
    }

    public int SetResolution(int _index) //Gets value from external source
    {
        Resolution[] resolutions = Screen.resolutions;
        gameSettings.resolutionIndex = _index;
        Screen.SetResolution(resolutions[_index].width, resolutions[_index].height, Screen.fullScreen);
        return _index;
    }

    public int SetTextureQuality(GameSettings _gameSettings) //Gets value from Game Settings
    {
        QualitySettings.masterTextureLimit = _gameSettings.textureQuality;
        return QualitySettings.masterTextureLimit;
    }

    public int SetTextureQuality(int _value) //Gets value from external source
    {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = _value;
        return QualitySettings.masterTextureLimit;
    }

    public int SetAntiAliasing(GameSettings _gameSettings) //Gets value from Game Settings
    {
        QualitySettings.antiAliasing = (int)Mathf.Pow(2, _gameSettings.antiAliasing);
        return QualitySettings.antiAliasing;
    }

    public int SetAntiAliasing(int _value) //Gets value from external source
    {
        gameSettings.antiAliasing = _value;
        QualitySettings.antiAliasing = (int)Mathf.Pow(2, _value);
        return QualitySettings.antiAliasing;
    }

    public int SetVSync(GameSettings _gameSettings) //Gets value from Game Settings
    {
        QualitySettings.vSyncCount = _gameSettings.vSync;
        return QualitySettings.vSyncCount;
    }

    public int SetVSync(int _value) //Gets value from external source
    {
        QualitySettings.vSyncCount = gameSettings.vSync = _value;
        return QualitySettings.vSyncCount;
    }
    #endregion

    #region Audio Settings Methods

    void GetSceneMusic()
    {
        sceneMusic = SceneMusic.instance;
    }

    void InitSceneSoundtrack()
    {
        musicSource.clip = sceneMusic.soundtrack;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void InitAudioSettings(GameSettings _gameSettings)
    {
        SetMasterVolume(_gameSettings);
        SetMusicVolume(_gameSettings);
        SetFXVolume(_gameSettings);
    }

    /// <summary>
    /// Gets value from Game Settings
    /// </summary>
    /// <param name="_gameSettings"></param>
    /// <returns></returns>
    public float SetMasterVolume(GameSettings _gameSettings)
    {
        return SetVolume(AudioChannels.MasterVol, _gameSettings.masterVolume);
    }

    /// <summary>
    /// Sets Game Settings value
    /// </summary>
    /// <param name="_value"></param>
    /// <returns></returns>
    public float SetMasterVolume(float _value)
    {
        gameSettings.masterVolume = _value;
        return SetVolume(AudioChannels.MasterVol, _value);
    }

    /// <summary>
    /// Gets value from Game Settings
    /// </summary>
    /// <param name="_gameSettings"></param>
    /// <returns></returns>
    public float SetMusicVolume(GameSettings _gameSettings)
    {
        return SetVolume(AudioChannels.MusicVol, _gameSettings.musicVolume);
    }

    /// <summary>
    /// Sets Game Settings value
    /// </summary>
    /// <param name="_value"></param>
    /// <returns></returns>
    public float SetMusicVolume(float _value)
    {
        gameSettings.musicVolume = _value;
        return SetVolume(AudioChannels.MusicVol, _value);
    }

    /// <summary>
    /// Gets value from Game Seetings
    /// </summary>
    /// <param name="_gameSettings"></param>
    /// <returns></returns>
    public float SetFXVolume(GameSettings _gameSettings)
    {
        return SetVolume(AudioChannels.FX_Vol, _gameSettings.fxVolume);
    }

    /// <summary>
    /// Sets Game Settings value
    /// </summary>
    /// <param name="_value"></param>
    /// <returns></returns>
    public float SetFXVolume(float _value)
    {
        gameSettings.fxVolume = _value;
        return SetVolume(AudioChannels.FX_Vol, _value);
    }

    float SetVolume(AudioChannels _channel, float _value)
    {
        if (_value < 0.1)
        {
            audioMixer.SetFloat(_channel.ToString(), -80);
        }
        else
        {
            float dbVal = Mathf.Log10(_value);
            dbVal = (100f * dbVal) - 80f;
            audioMixer.SetFloat(_channel.ToString(), dbVal);
        }
        return _value;
    }
    #endregion

    #region Game Pause
    public void PauseGameToggle()
    {
        gamePaused = !gamePaused;
        if (gamePaused)
        {
            Time.timeScale = 0;
            gamePausedDelegate?.Invoke();
        }
        else
        {
            Time.timeScale = 1;
            gameResumedDelegate?.Invoke();
        }
    }
    #endregion
}
