using UnityEngine;

public class ProcessDeath : MonoBehaviour
{
    public static void ProcessCharacterDeath(Collision2D other, PlayableCharacterManager playableCharacterManager) // For Collision
    {
        if (other != null && other.gameObject.CompareTag("Clone"))
        {
            Debug.Log("Clone detected");
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            Debug.Log("playerController: " + playerController);
            if (playerController != null)
            {
                Destroy(other.gameObject);
                playableCharacterManager.RemoveCloneFromList(playerController);
            }
            else
            {
                Debug.LogError("PlayerController is null.");
            }
        }
        else if (other != null && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.PlayerDeath();
            Debug.Log("Player death initiated");
        }
    }

    public static void ProcessCharacterDeath(Collider2D other, PlayableCharacterManager playableCharacterManager) // For Triggers
    {
        if (other != null && other.gameObject.CompareTag("Clone"))
        {
            Debug.Log("Clone detected");
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            Debug.Log("playerController: " + playerController);
            if (playerController != null)
            {
                Destroy(other.gameObject);
                playableCharacterManager.RemoveCloneFromList(playerController);
            }
            else
            {
                Debug.LogError("PlayerController is null.");
            }
        }
        else if (other != null && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.PlayerDeath();
            Debug.Log("Player death initiated");
        }
    }
}
