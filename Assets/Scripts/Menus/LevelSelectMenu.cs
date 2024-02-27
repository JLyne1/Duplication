using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int sceneBuildIndex = i + 1;
            levelButtons[i].onClick.AddListener(delegate { ButtonClickHandler(sceneBuildIndex); });
        }
    }

    private void ButtonClickHandler(int sceneIndex)
    {
        LoadLevel(sceneIndex);
    }

    private void LoadLevel(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
