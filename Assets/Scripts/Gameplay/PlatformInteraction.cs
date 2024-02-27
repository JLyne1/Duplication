using System.Collections;
using UnityEngine;

public class PlatformInteraction : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private SwitchInteraction switchInteraction;
       
    [Header("Mechanics")]
    [SerializeField] private GameObject linkedSwitch;
    [SerializeField] private float switchToPlatformDelay = 0f;

    private bool hasDropped = false;
    
    private void Awake() 
    {
        if (linkedSwitch == null)
        {
            Debug.LogWarning(name + " has no linked switch!");
        }
        else
        {
            return;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        switchInteraction = linkedSwitch.GetComponent<SwitchInteraction>();
    }

    private void Update()
    {
        PlatformMechanics();
    }

    private void PlatformMechanics()
    {
        if (!switchInteraction.GetIsActive() && !hasDropped)
        {
            StartCoroutine(PlatformDown());
        }
        else if (switchInteraction.GetIsActive() && hasDropped)
        {
            StartCoroutine(PlatformUp());
        }
    }

    private IEnumerator PlatformDown()
    {
        yield return new WaitForSeconds(switchToPlatformDelay);

        if (!hasDropped)
        {
            animator.SetBool("isRaised", false);
            boxCollider2D.enabled = false;
            hasDropped = true;
        }
    }

    private IEnumerator PlatformUp()
    {
        yield return new WaitForSeconds(switchToPlatformDelay);

        if (hasDropped)
        {
            animator.SetBool("isRaised", true);
            boxCollider2D.enabled = true;
            hasDropped = false;
        }
    }
}