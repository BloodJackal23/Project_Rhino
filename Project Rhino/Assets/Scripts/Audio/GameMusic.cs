using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameMusic : Singleton<GameMusic>
{
    AudioSource audioSource;
    #region Chapters Music
    [SerializeField] AudioClip mainTheme;
    #endregion

    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        PlayMainTheme();
    }

    public void PlayMainTheme()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.clip = mainTheme;
            audioSource.Play();
        }
    }
}
