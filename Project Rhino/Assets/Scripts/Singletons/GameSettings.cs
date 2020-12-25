
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
}
