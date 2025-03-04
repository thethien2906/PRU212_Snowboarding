using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    [SerializeField] private float pausedVolume = 0.7f;
    [SerializeField] private float lowPassFrequency = 1000f;

    private AudioLowPassFilter[] bgmLowPassFilters;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            bgmLowPassFilters = new AudioLowPassFilter[bgm.Length];
            for (int i = 0; i < bgm.Length; i++)
            {
                bgmLowPassFilters[i] = bgm[i].gameObject.GetComponent<AudioLowPassFilter>();
                if (bgmLowPassFilters[i] == null)
                {
                    bgmLowPassFilters[i] = bgm[i].gameObject.AddComponent<AudioLowPassFilter>();
                }
                bgmLowPassFilters[i].enabled = false;
            }

            SceneManager.sceneLoaded += OnSceneLoaded; // Listen for scene changes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBGM(0); // Play BGM 0 at game start (main menu)
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0) // Main Menu
        {
            PlayBGM(0);
        }
        else // Gameplay scenes
        {
            PlayBGM(1);
        }
    }

    public void PlaySFX(int sfxToPlay, bool randomPitch = true)
    {
        if (sfxToPlay >= sfx.Length)
            return;
        if (randomPitch)
        {
            sfx[sfxToPlay].pitch = Random.Range(.9f, 1.1f);
        }
        sfx[sfxToPlay].Play();
    }

    public void PlayBGM(int bgmToPlay)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
        if (bgmToPlay >= bgm.Length)
            return;
        bgm[bgmToPlay].Play();
    }

    public void AdjustBGMForPause(bool isPaused)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (isPaused)
            {
                bgm[i].volume *= pausedVolume;
                bgmLowPassFilters[i].enabled = true;
                bgmLowPassFilters[i].cutoffFrequency = lowPassFrequency;
            }
            else
            {
                bgm[i].volume = 1f;
                bgmLowPassFilters[i].enabled = false;
            }
        }
    }

    public void StopSFX(int sfxToStop) => sfx[sfxToStop].Stop();
}
