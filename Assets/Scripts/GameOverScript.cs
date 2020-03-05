using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    private Button[] buttons;

    void Awake()
    {
        // Get the buttons
        buttons = GetComponentsInChildren<Button>();

        // Disable them
        HideButtons();
    }

    public void HideButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(false);
        }
    }

    public void ShowButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(true);
        }
    }

    public void ExitToMenu()
    {
        // Reload the level
        Debug.Log("Menu Button clicked");
        Application.LoadLevel("MainMenu");
    }

    public void RestartGame()
    {
        // Reload the level
        Debug.Log("Reset Button clicked");
        Application.LoadLevel("Stage_1");
    }

    public void RetryLevel()
    {
        // Reload the level
        Debug.Log("Retry Button clicked");
        int scene = SceneManager.GetActiveScene().buildIndex;
        Application.LoadLevel(scene);
    }

    public void QuitGame()
    {
        // Reload the level
        Debug.Log("Quit Button clicked");
        Application.Quit();
    }
}
