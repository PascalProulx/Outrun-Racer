using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that handle the audio of the game
/// </summary>
public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        // Check if the object is already in the scene, if not, then it will create a new one
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("Spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    /// <summary>
    /// Fields
    /// </summary>
    private AudioSource _musicSource;
    private AudioSource _musicSource2;
    private AudioSource _sfxSource;
    private AudioSource _sfxSource2;
    private bool _firstMsourceIsPlaying;

    // Start is called before the first frame update
    void Awake()
    {
        // Make sure that we don't destroy the instance
        DontDestroyOnLoad(this.gameObject);

        // Create the audiosources, and save them as references
        _musicSource = this.gameObject.AddComponent<AudioSource>();
        _musicSource2 = this.gameObject.AddComponent<AudioSource>();
        _sfxSource = this.gameObject.AddComponent<AudioSource>();

        // Loop the music tracks
        _musicSource.loop = true;
        _musicSource2.loop = true;
    }

    // Method that plays the music
    public void PlayMusic(AudioClip musicClip)
    {
        // Determine wich source is active (terniary operation)
        AudioSource activeSource = (_firstMsourceIsPlaying) ? _musicSource : _musicSource2;

        activeSource.clip = musicClip;
        activeSource.volume = 1f;
        activeSource.Play();
    }

    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        // Determine wich source is active (terniary operation)
        AudioSource activeSource = (_firstMsourceIsPlaying) ? _musicSource : _musicSource2;

        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }

    public void PlaySFXWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        // Determine wich source is active (terniary operation)
        AudioSource activeSource = (_firstMsourceIsPlaying) ? _sfxSource : _sfxSource2;

        StartCoroutine(UpdateSFXWithFade(activeSource, newClip, transitionTime));
    }

    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        // Determine wich source is active (terniary operation)
        AudioSource activeSource = (_firstMsourceIsPlaying) ? _musicSource : _musicSource2;
        AudioSource newSource = (_firstMsourceIsPlaying) ? _musicSource2 : _musicSource;

        // Sawp the source
        _firstMsourceIsPlaying = !_firstMsourceIsPlaying;

        // Set the fields of the audio source, then strat the otoutine to crossfade
        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        // Make sure that the source is active and playing
        if (activeSource.isPlaying)
            activeSource.Play();
        float t = 0.0f;

        // Fade out
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (1 - (t / transitionTime));
            yield return null;
        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        // Fade in
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime);
            yield return null;
        }
    }

    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
        float t = 0.0f;

        // Fade out
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            original.volume = (1 - (t / transitionTime));
            newSource.volume = (t / transitionTime);
            yield return null;
        }

        original.Stop();
    }

    private IEnumerator UpdateSFXWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        // Make sure that the source is active and playing
        if (activeSource.isPlaying)
            activeSource.Play();
        float t = 0.0f;

        // Fade out
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (1 - (t / transitionTime));
            yield return null;
        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        // Fade in
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime);
            yield return null;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        _sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayLoopSFX(AudioClip clip)
    {
        _sfxSource.loop = true;
        _sfxSource.clip = clip;
        _sfxSource.Play();
    }

    public void PlayLoopSFX(AudioClip clip, float volume)
    {
        _sfxSource.loop = true;
        _sfxSource.clip = clip;
        _sfxSource.volume = volume;
        _sfxSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        _musicSource.volume = volume;
        _musicSource2.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        _sfxSource.volume = volume;
    }

    public void StopSFX()
    {
        _sfxSource.Stop();
    }
}
