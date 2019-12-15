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
        SceneManager.LoadScene("DemoStage");
	}
}

