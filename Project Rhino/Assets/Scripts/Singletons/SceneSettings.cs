using UnityEngine;

public class SceneSettings : Singleton<SceneSettings>
{
    [Header("Gameplay")]
    [SerializeField] bool canPause = true;
    [SerializeField] CursorLockMode cursorLockMode = CursorLockMode.None;
    public bool CanPause { get { return canPause; } }
    public CursorLockMode CursorLockMode { get { return cursorLockMode; } }
    [Space]
    #region Scene Music
    [Header("Music")]
    public bool playNewTrack = true;
    public AudioClip soundtrack;
    #endregion

    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
    }
}
