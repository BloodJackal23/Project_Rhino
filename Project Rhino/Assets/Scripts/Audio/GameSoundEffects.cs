using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameSoundEffects : Singleton<GameSoundEffects>
{
    AudioSource audioSource;
    #region FX
    [SerializeField] AudioClip selectAudio;
    [SerializeField] AudioClip switchAudio;
    #endregion

    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySelectAudio()
    {
        audioSource.clip = selectAudio;
        audioSource.Play();
    }

    public void PlaySwitchAudio()
    {
        audioSource.clip = switchAudio;
        audioSource.Play();
    }
}
