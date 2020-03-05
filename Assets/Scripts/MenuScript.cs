using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        // "Demo" is the stage for the prototype
        SceneManager.LoadScene("Stage_1");
	}

    public void ExitToMenu()
    {
        // Reload the level
        Debug.Log("Menu Button clicked");
        Application.LoadLevel("MainMenu");
    }

    public void QuitGame()
    {
        // Reload the level
        Debug.Log("Quit Button clicked");
        Application.Quit();
    }
}

