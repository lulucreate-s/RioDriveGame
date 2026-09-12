using UnityEngine;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private float musicVolume = 0.7f;
    [SerializeField] private float sfxVolume = 0.8f;

    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();
        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        LoadAudioClips();
        UpdateVolumes();
    }

    private void LoadAudioClips()
    {
        // Load all audio clips from Resources/Audio
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio");
        foreach (var clip in clips)
        {
            audioClips[clip.name] = clip;
        }

        Debug.Log($"[AudioManager] Loaded {audioClips.Count} audio clips");
    }

    public void PlayMusic(string clipName, bool loop = true)
    {
        if (!audioClips.ContainsKey(clipName))
        {
            Debug.LogWarning($"[AudioManager] Music not found: {clipName}");
            return;
        }

        if (musicSource.isPlaying)
            musicSource.Stop();

        musicSource.clip = audioClips[clipName];
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PlaySFX(string clipName, float volumeOverride = 1f)
    {
        if (!audioClips.ContainsKey(clipName))
        {
            Debug.LogWarning($"[AudioManager] SFX not found: {clipName}");
            return;
        }

        sfxSource.PlayOneShot(audioClips[clipName], volumeOverride * sfxVolume * masterVolume);
    }

    public void StopMusic(float fadeTime = 0.5f)
    {
        if (fadeTime <= 0)
        {
            musicSource.Stop();
            return;
        }

        StartCoroutine(FadeOut(musicSource, fadeTime));
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    private void UpdateVolumes()
    {
        musicSource.volume = musicVolume * masterVolume;
        sfxSource.volume = sfxVolume * masterVolume;
    }

    private System.Collections.IEnumerator FadeOut(AudioSource source, float duration)
    {
        float startVolume = source.volume;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            yield return null;
        }

        source.Stop();
        source.volume = startVolume;
    }
}
