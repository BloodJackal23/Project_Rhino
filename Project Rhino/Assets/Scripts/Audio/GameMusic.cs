using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameMusic : Singleton<GameMusic>
{
    AudioSource audioSource;
    #region Chapters Music
    [SerializeField] AudioClip[] music;
    [SerializeField] int musicIndex = 0;
    #endregion

    protected override void Awake()
    {
        dontDestroyOnLoad = true;
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        PlayMusic(musicIndex);
    }

    void PlayMusic(int _index)
    {
        audioSource.clip = music[_index];
        audioSource.Play();
    }
}
