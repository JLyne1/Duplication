using UnityEngine;

public class SwitchInteraction : MonoBehaviour
{
    private Animator animator;
    private SFXAudioManager sFXAudioManager;

    private int playersInTrigger = 0;
    private bool isActive = false;

    private void Awake() 
    {
        sFXAudioManager = FindObjectOfType<SFXAudioManager>();

        if (sFXAudioManager == null)
        {
            Debug.LogWarning("SFX for Switches not found!");        
        }
        else
        {
            return;
        }
    }

    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player") || other.CompareTag("Clone"))
        {
            playersInTrigger++;
            
            if (playersInTrigger == 1)
            {
                animator.SetBool("isActive", true);
                isActive = true;
                sFXAudioManager.PlaySwitchActiveClip();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player") || other.CompareTag("Clone"))
        {
            playersInTrigger--;
            
            if (playersInTrigger == 0)
            {
                animator.SetBool("isActive", false);
                isActive = false;
                sFXAudioManager.PlaySwitchInactiveClip();
            }
        }
    }

    private void OnDisable() 
    {
        if (sFXAudioManager != null)
        {
            sFXAudioManager.GetAudioSource().Stop();
            Debug.Log("OnDisable: SFX Audio Source was disabled");
        }
    }

    public bool GetIsActive()
    {
        return isActive;
    }
}
