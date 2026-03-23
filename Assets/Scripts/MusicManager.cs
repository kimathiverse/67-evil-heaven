using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip normalMusic;
    public AudioClip lowKarmaMusic;

    KarmaSystem karmaSystem;

    private AudioClip currentClip;

    void Start()
    {
        karmaSystem = GameObject.FindGameObjectWithTag("Karma").GetComponent<KarmaSystem>();
        UpdateMusic();
    }

    void Update()
    {
        UpdateMusic();
    }

    void UpdateMusic()
    {
        AudioClip targetClip;

        if (karmaSystem.karma <= 50f)
        {
            targetClip = lowKarmaMusic;
        }
        else
        {
            targetClip = normalMusic;
        }

        // Only switch if different (prevents restarting every frame)
        if (currentClip != targetClip)
        {
            currentClip = targetClip;
            audioSource.clip = currentClip;
            audioSource.Play();
        }
    }
}
