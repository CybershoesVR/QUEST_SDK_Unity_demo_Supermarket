using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    private AudioClip defaultTrack;
    private float defaultVolume;
    private AudioSource source;
    private AudioSource subSource;


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

    private void Start()
    {
        subSource = transform.GetChild(0).GetComponent<AudioSource>();
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

    public void StopTrack()
    {
        source.Stop();
    }

    public void StartSubTrack(AudioClip track, float volume=1)
    {
        subSource.Stop();
        subSource.clip = track;
        subSource.volume = volume;
        subSource.Play();
    }

    public void StopSubTrack()
    {
        subSource.Stop();
    }
}
