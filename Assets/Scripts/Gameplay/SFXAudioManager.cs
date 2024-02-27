using UnityEngine;

public class SFXAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Header("Player")]
    [SerializeField] private AudioClip jumpAudioClip;
    [SerializeField] [Range(0f, 1f)] private float jumpVolume = 1f;
    [Space(10)]
    [SerializeField] private AudioClip footstepClip;
    [SerializeField] [Range(0f, 1f)] private float footstepVolume = 1f;
    [Space(10)]
    [SerializeField] private AudioClip cloningAbilityClip;
    [SerializeField] [Range(0f, 1f)] private float cloningAbilityVolume = 1f;
    [Space(10)]
    [SerializeField] private AudioClip changeCharacterClip;
    [SerializeField] [Range(0f, 1f)] private float changeCharacterVolume = 1f;
    [Space(10)]
    [SerializeField] private AudioClip playerDeathClip;
    [SerializeField] [Range(0f, 1f)] private float playerDeathVolume = 1f;
    [Space(10)]
    [SerializeField] private AudioClip cloneDeathClip;
    [SerializeField] [Range(0f, 1f)] private float cloneDeathVolume = 1f;
    

    [Header("Switch")]
    [SerializeField] private AudioClip switchActiveClip;
    [SerializeField] [Range(0f, 1f)] private float switchActiveVolume = 1f;
    [Space(10)]
    [SerializeField] private AudioClip switchInactiveClip;
    [SerializeField] [Range(0f, 1f)] private float switchInactiveVolume = 1f;


    [Header("Exit")]
    [SerializeField] private AudioClip levelExitClip;
    [SerializeField] [Range(0f, 1f)] private float levelExitVolume = 1f;


    [Header("UI")]
    [SerializeField] private AudioClip buttonClickClip;
    [SerializeField] [Range(0f, 1f)] private float buttonClipVolume = 1f;
    

    private void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
    }

#region PLAYER
    public void PlayJumpAudioClip()
    {
        if(jumpAudioClip != null)
        {
            audioSource.PlayOneShot(jumpAudioClip, jumpVolume);
        }
    }

    public void PlayFootstepClip()
    {
        if(footstepClip != null)
        {
            audioSource.PlayOneShot(footstepClip, footstepVolume);
        }
    }

    public void PlayCloningAbilityClip()
    {
        if(cloningAbilityClip != null)
        {
            audioSource.PlayOneShot(cloningAbilityClip, cloningAbilityVolume);
        }
    }

    public void PlayChangeCharacterClip()
    {
        if(changeCharacterClip != null)
        {
            audioSource.PlayOneShot(changeCharacterClip, changeCharacterVolume);
        }
    }

    public void PlayPlayerDeathClip()
    {
        if(playerDeathClip != null)
        {
            audioSource.PlayOneShot(playerDeathClip, playerDeathVolume);
        }
    }

    public void PlayCloneDeathClip()
    {
        if(cloneDeathClip != null)
        {
            audioSource.PlayOneShot(cloneDeathClip, cloneDeathVolume);
        }
    }
#endregion

#region SWITCH
    public void PlaySwitchActiveClip()
    {
        if(switchActiveClip != null)
        {
            audioSource.PlayOneShot(switchActiveClip, switchActiveVolume);
        }
    }

    public void PlaySwitchInactiveClip()
    {
        if(switchInactiveClip != null)
        {
            audioSource.PlayOneShot(switchInactiveClip, switchInactiveVolume);
        }
    }
#endregion

#region EXIT
    public void PlayLevelExitClip()
    {
        if(levelExitClip != null)
        {
            audioSource.PlayOneShot(levelExitClip, levelExitVolume);
        }
    }
#endregion

#region UI
    public void PlayButtonClickClip()
    {
        if(buttonClickClip != null)
        {
            audioSource.PlayOneShot(buttonClickClip, buttonClipVolume);
        }
    }
#endregion

#region GETTERS AND SETTERS
    public AudioSource GetAudioSource()
    {
        return audioSource;
    }
#endregion
}
