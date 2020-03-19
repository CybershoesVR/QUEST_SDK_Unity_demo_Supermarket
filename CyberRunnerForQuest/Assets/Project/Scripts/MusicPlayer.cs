using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    private AudioClip defaultTrack;
    private float defaultVolume;
    private AudioSource source;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            source = GetComponent<AudioSource>();
            defaultTrack = source.clip;
            defaultVolume = source.volume;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeTrack(AudioClip newTrack,float volume)
    {
        source.Stop();
        source.clip = newTrack;
        source.volume = volume;
        source.Play();
    }

    public void ResetTrack()
    {
        source.Stop();
        source.clip = defaultTrack;
        source.volume = defaultVolume;
        source.Play();
    }
}
