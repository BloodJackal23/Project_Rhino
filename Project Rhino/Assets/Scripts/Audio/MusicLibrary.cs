using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Music Library", menuName = "Libraries/Audio Libraries")]
public class MusicLibrary : ScriptableObject
{
    [Serializable]
    public class Soundtrack
    {
        public string name;
        public AudioClip clip;
    }

    public Soundtrack[] soundtracks;

    public void PlaySoundtrack(AudioSource _source, int _index, bool _loop)
    {
        _source.clip = soundtracks[_index].clip;
        _source.loop = _loop;
        _source.Play();
    }

    public void PlaySoundtrack(AudioSource _source, string _name, bool _loop)
    {
        AudioClip newClip;

        foreach(Soundtrack soundtrack in soundtracks)
        {
            if(soundtrack.name == _name)
            {
                newClip = soundtrack.clip;
                _source.clip = newClip;
                break;
            }
        }
        _source.loop = _loop;
        _source.Play();
    }
}
