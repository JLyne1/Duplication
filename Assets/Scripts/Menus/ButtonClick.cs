using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    private Button button;
    private SFXAudioManager sFXAudioManager;

    private void Awake() 
    {
        sFXAudioManager = FindObjectOfType<SFXAudioManager>();
    }

    private void Start() 
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickSFX);
    }

    private void ClickSFX()
    {
        if (sFXAudioManager != null)
        {
            sFXAudioManager.PlayButtonClickClip();
        }
        else
        {
            Debug.LogWarning("SFX Audio Manager not found!");
        }
    }
}
