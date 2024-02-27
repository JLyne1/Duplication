using UnityEngine;
using TMPro;

public class CloneTracker : MonoBehaviour
{
    private PlayableCharacterManager playableCharacterManager;
    [SerializeField] private TextMeshProUGUI clonesRemainingText;

    private void Awake() 
    {
        playableCharacterManager = FindObjectOfType<PlayableCharacterManager>();
    }
    
    private void Update()
    {
        if (clonesRemainingText == null)
        {
            Debug.LogWarning("UI component is missing!");
        }
        else
        {
            clonesRemainingText.text = "CLONES REMAINING: " + (playableCharacterManager.GetPlayableCharactersList().Capacity -
                                                                playableCharacterManager.GetPlayableCharactersList().Count).ToString();
        }
    }
}
