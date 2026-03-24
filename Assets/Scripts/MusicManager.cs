using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip normalMusic;
    public AudioClip lowKarmaMusic;
    public float fadeSpeed = 0.5f;

    private KarmaSystem karmaSystem;
    private AudioClip targetClip;
    private bool isSwitching = false;

    void Start()
    {
        karmaSystem = GameObject.FindGameObjectWithTag("Karma").GetComponent<KarmaSystem>();
        
        // Start a loop that checks every second instead of every frame
        InvokeRepeating(nameof(CheckMusic), 0f, 1f);
    }

    void CheckMusic()
    {
        if (isSwitching) return;

        AudioClip nextClip = (karmaSystem.karma <= 50f) ? lowKarmaMusic : normalMusic;

        if (audioSource.clip != nextClip)
        {
            StartCoroutine(CrossfadeMusic(nextClip));
        }
    }

    IEnumerator CrossfadeMusic(AudioClip newClip)
    {
        isSwitching = true;
        float startVolume = audioSource.volume;

        // Fade Out
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeSpeed;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade In
        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeSpeed;
            yield return null;
        }

        isSwitching = false;
    }
}
