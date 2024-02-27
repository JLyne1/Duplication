using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayableCharacterManager : MonoBehaviour
{
    private PlayerController originalPlayer;
    private SFXAudioManager sFXAudioManager;

    [Header("Playable Character Fields")]
    private PlayerController activePlayer;
    private int activePlayerIndex;
    [SerializeField] private List<PlayerController> playableCharacters = new();

    [Header("Current Player Limit")]
    [SerializeField] private int cloneLimit;

    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera[] followCameras;
    [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;


    private void Awake() 
    {
        sFXAudioManager = FindObjectOfType<SFXAudioManager>();
        originalPlayer = FindObjectOfType<PlayerController>();

        if (originalPlayer != null)
        {           
            playableCharacters.Add(originalPlayer);
        }
        else if (playableCharacters.Count == 0)
        {
            Debug.LogWarning("The list is empty!");
        }
        else
        {
            Debug.LogWarning("Original player not found!");
        }

        if (followCameras == null)
        {
            Debug.LogWarning("Follow Cameras not attached to Playable Character Manager!");
        }
        else
        {
            return;
        }
    }

    private void Start()
    {
        activePlayerIndex = 0;
        activePlayer = playableCharacters[activePlayerIndex];
    }

    private void Update()
    {
        SetTotalPlayerLimit();
    }

#region CHANGING CHARACTERS
    public void ChangeCameraTarget()
    {
        foreach (var camera in followCameras)
        {
            camera.Follow = activePlayer.transform;

            if (activePlayer.GetAnimator() != null)
                {
                    stateDrivenCamera.m_AnimatedTarget = activePlayer.GetAnimator();
                }
            else
            {
                Debug.LogWarning("Active player's animator not found!");
            }
        }
    }

    public void SetControlsEnabling(PlayerController player, bool enabled)
    {
        player.SetCharacterFunctions(enabled, enabled, enabled, enabled);
    }
#endregion    

#region REMOVING CHARACTERS
    public void RemoveCloneFromList(PlayerController clonePlayer)
    {
        Debug.Log("RemoveCloneFromList called");

        if (clonePlayer != null && playableCharacters.Contains(clonePlayer))
            {
                sFXAudioManager.PlayCloneDeathClip();
                int removedIndex = playableCharacters.IndexOf(clonePlayer);
                playableCharacters.Remove(clonePlayer);

                if (activePlayerIndex > removedIndex)
                {
                    activePlayerIndex--;
                }
                else if (activePlayerIndex == removedIndex)
                {
                    activePlayerIndex++;

                    if (activePlayerIndex >= playableCharacters.Count)
                    {
                        activePlayerIndex = 0;
                    }

                    activePlayer = playableCharacters[activePlayerIndex];
                    SetControlsEnabling(activePlayer, true);
                    ChangeCameraTarget();
                }
            }
        else
            {
                Debug.LogWarning("Clone player removed from list");
            }

        Debug.Log("Number of players remaining: " + playableCharacters.Count);
    }

    public void DeathCamera()
    {
        stateDrivenCamera.m_AnimatedTarget = originalPlayer.GetAnimator();
    }
#endregion

#region GETTERS AND SETTERS
    private int SetTotalPlayerLimit()
    {
        return playableCharacters.Capacity = cloneLimit + 1;
    }

    public List<PlayerController> GetPlayableCharactersList()
    {
        return playableCharacters;
    }

    public PlayerController GetActivePlayer()
    {
        return activePlayer;
    }

    public void SetActivePlayer(PlayerController player)
    {
        activePlayer = player;
    }

    public int GetActivePlayerIndex()
    {
        return activePlayerIndex;
    }

    public void SetActivePlayerIndex(int playerIndex)
    {
        activePlayerIndex = playerIndex;
    }
#endregion
}