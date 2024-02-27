using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [Header("Menu Load Time")]
    [SerializeField] private float menuLoadDelay = 1f;

    [Header("Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private SFXAudioManager sFXAudioManager;

    private readonly int mainMenuSceneIndex = 0;
    private PauseMenu pauseMenu;
    private PlayableCharacterManager playableCharacterManager;
    
    private void Awake() 
    {
        audioManager = FindObjectOfType<AudioManager>();
        sFXAudioManager = FindObjectOfType<SFXAudioManager>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        playableCharacterManager = FindObjectOfType<PlayableCharacterManager>();
        playerController = FindObjectOfType<PlayerController>();
        
        if (playerController == null)
        {
            Debug.LogWarning("Main player for exit not found!");
        }
        else
        {
            return;
        }

        if (audioManager == null)
        {
            Debug.LogWarning("Background music for exit not found!");
        }
        else
        {
            return;
        }
    }
    
    private void Start() 
    {
        playerController.GetAnimator();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        pauseMenu.SetCanPause(false);
        playableCharacterManager.SetControlsEnabling(playableCharacterManager.GetActivePlayer(), false);
        playerController.GetAnimator().SetTrigger("hasCompleted");
        StartCoroutine(EndLevel());
    }

    private IEnumerator EndLevel()
    {
        audioManager.GetMusicSource().Stop();
        sFXAudioManager.PlayLevelExitClip();
        yield return new WaitForSeconds(menuLoadDelay);
        SceneManager.LoadScene(mainMenuSceneIndex);
        audioManager.GetMusicSource().Play();
    }
}
