using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource, sfxSource;

    [Header("Music Volumes")]
    [SerializeField] private float musicVolume = 1f;
    [SerializeField] private float musicVolumePaused = 0.5f;

    private static AudioManager instance;

    private void Awake() 
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update() 
    {       
        if (PauseMenu.isPaused)
        {
            musicSource.volume = musicVolumePaused;
        }
        else
        {
            musicSource.volume = musicVolume; 
        }
    }

    public AudioSource GetMusicSource()
    {
        return musicSource;
    }
}
