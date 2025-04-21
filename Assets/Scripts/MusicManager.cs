using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private MusicLibrary musicLibrary;
    [SerializeField] private AudioSource musicSource;

    [SerializeField, Range(0f, 1f)]
    private float targetVolume = 1f; // Set your preferred music volume here

    private Coroutine currentFadeRoutine;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayMusic(string trackName, float fadeDuration = 0.5f)
    {
        AudioClip clip = musicLibrary.GetClipFromName(trackName);
        if (clip != null)
        {
            if (currentFadeRoutine != null)
                StopCoroutine(currentFadeRoutine);

            currentFadeRoutine = StartCoroutine(AnimateMusicCrossfade(clip, fadeDuration));
        }
        else
        {
            Debug.LogWarning($"Music track \"{trackName}\" not found.");
        }
    }

    IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration = 0.5f)
    {
        float percent = 0;
        float initialVolume = musicSource.volume;

        // Fade out current music
        while (percent < 1)
        {
            percent += Time.deltaTime / fadeDuration;
            musicSource.volume = Mathf.Lerp(initialVolume, 0, percent);
            yield return null;
        }

        musicSource.clip = nextTrack;
        musicSource.Play();

        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime / fadeDuration;
            musicSource.volume = Mathf.Lerp(0, targetVolume, percent);
            yield return null;
        }

        musicSource.volume = targetVolume; // ensure volume is set to intended level
    }

    public void SetMusicVolume(float volume)
    {
        targetVolume = Mathf.Clamp01(volume);
        musicSource.volume = targetVolume;
    }

    public float GetMusicVolume()
    {
        return targetVolume;
    }
}
