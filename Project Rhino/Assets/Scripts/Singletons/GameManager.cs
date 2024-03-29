﻿using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Game Data
    public GameSettings gameSettings { get; private set; }
    [SerializeField] private LevelManager levelManager;
    #endregion

    #region Audio Variables
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource musicSource;
    public enum AudioChannels { MasterVol, MusicVol, FX_Vol }
    [Space]
    #endregion

    private SceneSettings sceneSettings;

    #region Loading System
    [Header("Loading System")]
    [SerializeField] private GameObject loadingScreenCanvas;
    [SerializeField] private LoadingBar loadingBar;
    #endregion

    #region Pause System
    public delegate void OnPauseCommand();
    public OnGamePaused onPauseCommand;

    public delegate void OnGamePaused();
    public OnGamePaused onGamePaused;

    public delegate void OnGameResumed();
    public OnGamePaused onGameResumed;
    public bool gamePaused { get; private set; }
    #endregion

    protected override void Awake()
    {
        dontDestroyOnLoad = true;
        gamePaused = false;
        base.Awake();
        SaveSystem.CreateSaveFolder();
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable Called!");
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneSystem.onLoadStart += ActivateLoadingScreen;
        SceneSystem.onLoadStart += loadingBar.ResetBar;
        SceneSystem.whileLoading += loadingBar.UpdateBar;        
    }

    private void Start()
    {
        Debug.Log("Start Called!");
        InitSceneSettings();
        InitGameSettings();
        levelManager.GetUnlockedLevelsFromSaveFile();
        SaveSystem.onSaveFileDeleted += levelManager.GetUnlockedLevelsFromSaveFile;
    }

    private void Update()
    {
        onPauseCommand?.Invoke();
    }

    private void OnDestroy()
    {
        onPauseCommand = null;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneSystem.onLoadStart -= ActivateLoadingScreen;
        SceneSystem.onLoadStart -= loadingBar.ResetBar;
        SceneSystem.whileLoading -= loadingBar.UpdateBar;

        SaveSystem.onSaveFileDeleted -= levelManager.GetUnlockedLevelsFromSaveFile;
    }

    void ActivateLoadingScreen()
    {
        loadingScreenCanvas.SetActive(true);
    }

    void DeactivateLoadingScreen()
    {
        loadingScreenCanvas.SetActive(false);
    }

    #region Scene Management
    public void LoadScene(SceneSystem.GameScene _scene)
    {      
        AsyncOperation operation = SceneManager.LoadSceneAsync(_scene.ToString());
        StartCoroutine(SceneSystem.LoadNextScene(operation));
    }

    public void LoadScene(string _scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(_scene);
        StartCoroutine(SceneSystem.LoadNextScene(operation));
    }

    public void LoadMainMenuScene()
    {
        LoadScene(SceneSystem.GameScene.MainMenu);
    }

    void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        Debug.Log("New scene loaded");
        DeactivateLoadingScreen();
        InitSceneSettings();
        if (sceneSettings.playNewTrack)
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

    public void SetToDefaultSettings()
    {
        gameSettings = GameSettings.DefaultSettings;
        InitVideoSettings(gameSettings);
        InitAudioSettings(gameSettings);
        SaveGameSettings();
    }

    public void SaveGameSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/GameSettings.json", jsonData);
        Debug.Log("Game Settings saved at " + Application.persistentDataPath + "/GameSettings.json");
        Debug.Log("Game Settings are set at FScrn = " + gameSettings.fullScreen + "/TexQ = " + gameSettings.textureQuality
            + "/VSync = " + gameSettings.vSync + "/ResInd = " + gameSettings.resolutionIndex + "/MasVol = " + gameSettings.masterVolume
            + "/MusVol = " + gameSettings.musicVolume + "/FxVol = " + gameSettings.fxVolume);
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
            SetToDefaultSettings();
        }
    }
    #endregion

    #region Video Settings Methods
    public void InitVideoSettings(GameSettings _gameSettings)
    {
        SetFullScreen(GetFullScreen(_gameSettings));
        SetResolution(GetResolution(_gameSettings));
        SetTextureQuality(GetTextureQuality(_gameSettings));
        SetVSync(GetVSync(_gameSettings));
    }

    public bool GetFullScreen(GameSettings _gameSettings) //Gets value from Game Settings
    {
        return _gameSettings.fullScreen;
    }

    public bool SetFullScreen(bool _value) //Gets value from external source
    {
        Screen.fullScreen = gameSettings.fullScreen = _value;
        return Screen.fullScreen;
    }

    public int GetResolution(GameSettings _gameSettings) //Gets value from Game Settings
    {
        return _gameSettings.resolutionIndex;
    }

    public int SetResolution(int _index) //Gets value from external source
    {
        Resolution[] resolutions = Screen.resolutions;
        gameSettings.resolutionIndex = _index;
        Screen.SetResolution(resolutions[_index].width, resolutions[_index].height, Screen.fullScreen);
        return _index;
    }

    public int GetTextureQuality(GameSettings _gameSettings) //Gets value from Game Settings
    {
        return _gameSettings.textureQuality;
    }

    public int SetTextureQuality(int _value) //Gets value from external source
    {
        QualitySettings.masterTextureLimit = gameSettings.textureQuality = _value;
        return QualitySettings.masterTextureLimit;
    }

    public int GetVSync(GameSettings _gameSettings) //Gets value from Game Settings
    {
        return _gameSettings.vSync;
    }

    public int SetVSync(int _value) //Gets value from external source
    {
        QualitySettings.vSyncCount = gameSettings.vSync = _value;
        return QualitySettings.vSyncCount;
    }
    #endregion

    #region Audio Settings Methods
    void InitSceneSettings()
    {
        sceneSettings = SceneSettings.instance;
        onPauseCommand = null;
        if (sceneSettings.CanPause)
        {
            onPauseCommand += PauseCommand;
        }
        Cursor.lockState = sceneSettings.CursorLockMode;
    }

    void InitSceneSoundtrack()
    {
        musicSource.clip = sceneSettings.soundtrack;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void InitAudioSettings(GameSettings _gameSettings)
    {
        SetMasterVolume(GetMasterVolume(_gameSettings));
        SetMusicVolume(GetMusicVolume(_gameSettings));
        SetFXVolume(GetFXVolume(_gameSettings));
    }

    /// <summary>
    /// Gets value from Game Settings
    /// </summary>
    /// <param name="_gameSettings"></param>
    /// <returns></returns>
    public float GetMasterVolume(GameSettings _gameSettings)
    {
        return _gameSettings.masterVolume;
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
    public float GetMusicVolume(GameSettings _gameSettings)
    {
        return _gameSettings.musicVolume;
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
    public float GetFXVolume(GameSettings _gameSettings)
    {
        return _gameSettings.fxVolume;
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


    /// <summary>
    /// Sets a volume mixer channel's value. Make sure that the corresponding slider is clamped between 0.0001 and 1
    /// </summary>
    /// <param name="_channel"></param>
    /// <param name="_value"></param>
    /// <returns></returns>
    float SetVolume(AudioChannels _channel, float _value)
    {
        audioMixer.SetFloat(_channel.ToString(), Mathf.Log10(_value) * 20);
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
            Debug.Log("Game Paused!");
            Cursor.lockState = CursorLockMode.None;
            onGamePaused?.Invoke();
        }
        else
        {
            Time.timeScale = 1;
            Debug.Log("Game Resumed!");
            Cursor.lockState = sceneSettings.CursorLockMode;
            onGameResumed?.Invoke();
        }
    }

    void PauseCommand()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseGameToggle();
        }
    }
    #endregion
}
