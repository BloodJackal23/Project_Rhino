
public class GameSettings
{
    #region Video Settings
    public bool fullScreen = false;
    public int textureQuality;
    public int vSync;
    public int resolutionIndex;
    #endregion

    #region Audio Settings
    public float masterVolume = 6;
    public float musicVolume = 4;
    public float fxVolume = 4;
    #endregion

    public GameSettings(bool _fullScreen, int _textureQuality, int _vSync, int _resIndex, float _masterVol, float _musicVol, float _fxVol)
    {
        fullScreen = _fullScreen;
        textureQuality = _textureQuality;
        vSync = _vSync;
        resolutionIndex = _resIndex;
        masterVolume = _masterVol;
        musicVolume = _musicVol;
        fxVolume = _fxVol;
    }

    /// <summary>
    /// Default Audio settings
    /// </summary>
    /// <param name="_masterVol"></param>
    /// <param name="_musicVol"></param>
    /// <param name="_fxVol"></param>
    public GameSettings(float _masterVol, float _musicVol, float _fxVol)
    {
        masterVolume = _masterVol;
        musicVolume = _musicVol;
        fxVolume = _fxVol;
    }

    /// <summary>
    /// Default Video settings
    /// </summary>
    /// <param name="_fullScreen"></param>
    /// <param name="_textureQuality"></param>
    /// <param name="_vSync"></param>
    /// <param name="_resIndex"></param>
    public GameSettings(bool _fullScreen, int _textureQuality, int _vSync, int _resIndex)
    {
        fullScreen = _fullScreen;
        textureQuality = _textureQuality;
        vSync = _vSync;
        resolutionIndex = _resIndex;
    }

    public static GameSettings DefaultSettings
    {
        get
        {
            return new GameSettings(false, 1, 0, 0, 6f, 6f, 6f);
        }
    }

    public static GameSettings DefaultAudioSettings
    {
        get
        {
            return new GameSettings(6f, 6f, 6f);
        }
    }

    public static GameSettings DefaultVideoSettings
    {
        get
        {
            return new GameSettings(false, 1, 0, 0);
        }
    }
}
