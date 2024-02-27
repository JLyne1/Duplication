using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private PlayableCharacterManager playableCharacterManager;
    private SFXAudioManager sFXAudioManager;
    private CameraMapView cameraMapView;
    
    #region BOOLS
    private bool isAlive = true;
    private bool isCloning = false;
    private bool canMove = true;
    private bool canJump = true;
    private bool canClone = true;
    private bool canChangeCharacter = true;
    private bool isCameraZoomedOut = false;
    #endregion

    [SerializeField] private float levelResetDelay = 1f;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpSpeed = 10f;

    [Header("Cloning")]
    [SerializeField] private AnimationClip cloningClip;
    [SerializeField] private GameObject clonePlayerPrefab;
    [SerializeField] private Vector3 cloneSpawnPosition;

    [Header("Changing Character")]
    [SerializeField] private float characterChangeDelay = 1f;
    [SerializeField] private new ParticleSystem particleSystem;
    private bool isChangingToNextCharacter = false;
    private bool isChangingToPreviousCharacter = false;

    private void Awake()
    {
        if (gameObject.CompareTag("Player") && clonePlayerPrefab == null)
        {
            Debug.LogWarning("Clone player prefab not serialized!");
        }

        playableCharacterManager = FindObjectOfType<PlayableCharacterManager>();
        sFXAudioManager = FindObjectOfType<SFXAudioManager>();
        cameraMapView = FindObjectOfType<CameraMapView>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate() 
    {
        if (!isAlive) { return; }

        PlayerRun();
        PlayerJump();
        FlipSprite();
        animator.SetFloat("y_Velocity", rb.velocity.y);
    }

    private void Update()
    {
        if (!isAlive) { return; }

        CheckCloningConditions();
        ChangePlayerControl();
        CameraZoom();
    }

    private void ChangePlayerControl()
    {
        if (!isAlive || isCloning || 
            !canChangeCharacter ||
            isChangingToNextCharacter || 
            isChangingToPreviousCharacter || 
            !boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (Input.GetButtonDown("CharacterChange") &&
            rb.velocity.x == 0f  && rb.velocity.y == 0f &&
            playableCharacterManager.GetPlayableCharactersList().Count != 1)
        {
            sFXAudioManager.PlayChangeCharacterClip();
            isChangingToNextCharacter = Input.GetKeyDown(KeyCode.E);
            isChangingToPreviousCharacter = Input.GetKeyDown(KeyCode.Q);
            StartCoroutine(ChangeCharacterCoroutine());
        }
        else
        {
            return;
        }
    }

    private IEnumerator ChangeCharacterCoroutine()
    {
        yield return new WaitForSeconds(characterChangeDelay);

        if (playableCharacterManager.GetActivePlayer() == null) { yield break; }

        playableCharacterManager.SetControlsEnabling(playableCharacterManager.GetActivePlayer(), false);

        playableCharacterManager.SetActivePlayerIndex((playableCharacterManager.GetActivePlayerIndex() + 
        (isChangingToNextCharacter ? 1 : -1) + playableCharacterManager.GetPlayableCharactersList().Count) % 
        playableCharacterManager.GetPlayableCharactersList().Count);

        isChangingToNextCharacter = false;
        isChangingToPreviousCharacter = false;

        Debug.Log("Current player index = " + playableCharacterManager.GetActivePlayerIndex());

        playableCharacterManager.SetActivePlayer(playableCharacterManager.GetPlayableCharactersList()[playableCharacterManager.GetActivePlayerIndex()]);
        playableCharacterManager.SetControlsEnabling(playableCharacterManager.GetActivePlayer(), true);
        playableCharacterManager.ChangeCameraTarget();
        playableCharacterManager.GetActivePlayer().particleSystem.Play();
    }


    private void CameraZoom()
    {
        if (!isAlive || isCloning || 
            isChangingToNextCharacter || 
            isChangingToPreviousCharacter) { return; }

        if (playableCharacterManager.GetActivePlayer() == this &&
            Input.GetButtonDown("CameraZoom") &&
            rb.velocity.x == 0f && rb.velocity.y == 0f)
        {
            isCameraZoomedOut = !isCameraZoomedOut;
            cameraMapView.SetZoomState(isCameraZoomedOut);
        }
    }

#region CLONING
    private void CheckCloningConditions()
    {
        if (!isAlive || 
            !canClone ||
            isCloning || 
            isChangingToNextCharacter || 
            isChangingToPreviousCharacter ||
            playableCharacterManager.GetPlayableCharactersList().Count == playableCharacterManager.GetPlayableCharactersList().Capacity || 
            !boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (gameObject.CompareTag("Player") && 
            Mathf.Approximately(rb.velocity.x, 0f) && Mathf.Approximately(rb.velocity.y, 0f) &&
            Input.GetButtonDown("CloneAbility"))
            {
                isCloning = true;
                StartCoroutine(CloningCoroutine());
            }
    }

    private IEnumerator CloningCoroutine()
    {
        if (cloningClip == null)
        {
            Debug.Log("No cloning SFX clip found!");
            yield break;
        }

        sFXAudioManager.PlayCloningAbilityClip();
        animator.SetBool("isCloning", true);
        yield return new WaitForSeconds(cloningClip.length);
        animator.SetBool("isCloning", false);
        GameObject newCloneObj = Instantiate(clonePlayerPrefab, transform.position + cloneSpawnPosition, transform.rotation);
        isCloning = false;

        if (playableCharacterManager != null)
        {
            PlayerController newClonePlayer = newCloneObj.GetComponent<PlayerController>();
            playableCharacterManager.GetPlayableCharactersList().Add(newClonePlayer);
            playableCharacterManager.SetControlsEnabling(playableCharacterManager.GetActivePlayer(), false);
            playableCharacterManager.SetActivePlayerIndex(playableCharacterManager.GetPlayableCharactersList().Count - 1);
            playableCharacterManager.SetActivePlayer(newClonePlayer);
            playableCharacterManager.SetControlsEnabling(playableCharacterManager.GetActivePlayer(), true);
            playableCharacterManager.ChangeCameraTarget();
            playableCharacterManager.GetActivePlayer().particleSystem.Play();

            Debug.Log("New player index = " + playableCharacterManager.GetActivePlayerIndex());
            Debug.Log("Number of players = " + playableCharacterManager.GetPlayableCharactersList().Count);
        }
        else
        {
            Debug.LogWarning("PlayableCharacterManager is null!");
        }
    }
#endregion
    
#region MOVEMENT
    private void PlayerRun()
    {
        if (!isAlive || !canMove || isCloning || 
            isChangingToNextCharacter || 
            isChangingToPreviousCharacter || isCameraZoomedOut) { return; }

        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        Vector2 velocity = new(moveX * Time.deltaTime, rb.velocity.y);
        rb.velocity = velocity;

        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
            animator.SetBool("isRunning", playerHasHorizontalSpeed);
        }
    }

    private void PlayerJump()
    {
        if (!isAlive || 
            !canJump ||
            isCloning ||
            isChangingToNextCharacter || 
            isChangingToPreviousCharacter || 
            isCameraZoomedOut ||
            !boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (Input.GetButton("Jump") && !animator.GetBool("isJumping"))
        {
            sFXAudioManager.PlayJumpAudioClip();
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * Time.deltaTime);
            animator.SetBool("isJumping", true);
        }

        if (!Input.GetButton("Jump"))
        {
            animator.SetBool("isJumping", false);
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    private void PlayFootstepSound() 
    {
        sFXAudioManager.PlayFootstepClip();
    }
#endregion

#region DEATH
    public void PlayerDeath()
    {
        if (isAlive)
        {
            isAlive = false;
            sFXAudioManager.PlayPlayerDeathClip();
            animator.SetTrigger("isDying");
            playableCharacterManager.DeathCamera();
        }
        
        for (int i = 0; i < playableCharacterManager.GetPlayableCharactersList().Count; i++)
        {
            playableCharacterManager.SetControlsEnabling(playableCharacterManager.GetPlayableCharactersList()[i], false);
        }

        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(levelResetDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
#endregion

#region GETTERS AND SETTERS

    public bool GetCanMove()
    {
        return canMove;
    }

    public bool GetCanJump()
    {
        return canJump;
    }

    public bool GetCanClone()
    {
        return canClone;
    }

    public bool GetCanChangeCharacter()
    {
        return canChangeCharacter;
    }

    public void SetCharacterFunctions(bool move, bool jump, bool clone, bool changeCharacter)
    {
        canMove = move;
        canJump = jump;
        canClone = clone;
        canChangeCharacter = changeCharacter;
    }

    public Animator GetAnimator()
    {
        return animator;
    }

#endregion
}