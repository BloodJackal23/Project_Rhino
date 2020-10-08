using UnityEngine;

public class SceneMusic : Singleton<SceneMusic>
{
    #region Chapters Music
    public bool playNewTrack = true;
    public AudioClip soundtrack;
    #endregion

    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
    }
}
