using UnityEngine;

public class HammerInteraction : MonoBehaviour
{
    private Animator animator;
    private PlayableCharacterManager playableCharacterManager;

    private void Awake() 
    {
        playableCharacterManager = FindObjectOfType<PlayableCharacterManager>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetTrigger("isActive");
    }

    private void OnCollisionEnter2D(Collision2D other) // Processing player/clone death 
    {
        ProcessDeath.ProcessCharacterDeath(other, playableCharacterManager);
    }
}
