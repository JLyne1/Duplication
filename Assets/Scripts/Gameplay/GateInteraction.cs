using System.Collections;
using UnityEngine;

public class GateInteraction : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private CapsuleCollider2D capCollider2D;
    private SwitchInteraction switchInteraction;
    private PlayableCharacterManager playableCharacterManager;

    [Header("Mechanics")]
    [SerializeField] private GameObject linkedSwitch;
    [SerializeField] private float switchToGateDelay;

    private bool isOpen = false;

    private void Awake() 
    {
        playableCharacterManager = FindObjectOfType<PlayableCharacterManager>();

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
        capCollider2D = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        switchInteraction = linkedSwitch.GetComponent<SwitchInteraction>();
    }

    private void Update() 
    {
        GateMechanics();
    }

#region OPEN AND CLOSE
    private void GateMechanics()
    {
        if (switchInteraction.GetIsActive() && !isOpen)
        {
            StartCoroutine(GateOpen());
        }
        else if (!switchInteraction.GetIsActive() && isOpen)
        {
            StartCoroutine(GateClose());
        }
    }

    private IEnumerator GateOpen()
    {
        yield return new WaitForSeconds(switchToGateDelay);

        if (!isOpen)
        {
            animator.SetBool("isOpen", true);
            boxCollider2D.enabled = false;
            capCollider2D.enabled = false;
            isOpen = true;
        }
    }

    private IEnumerator GateClose()
    {
        yield return new WaitForSeconds(switchToGateDelay);

        if (isOpen)
        {
            animator.SetBool("isOpen", false);
            boxCollider2D.enabled = true;
            capCollider2D.enabled = true;
            isOpen = false;
        }
    }
#endregion

#region TRIGGERS
    private void OnTriggerEnter2D(Collider2D other) // Processing player/clone death
    {
        ProcessDeath.ProcessCharacterDeath(other, playableCharacterManager);
    }
#endregion
}
