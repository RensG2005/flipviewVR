using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Replace "GameScene" with your actual gameplay scene name
    }

    public void QuitGame()
    {
        Application.Quit(); // This will only work in a built game
    }
}
