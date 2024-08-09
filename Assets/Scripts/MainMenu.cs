
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        Debug.Log("Start Game");
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitGame() {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void Credits() {
        Debug.Log("Credits");
    }
}
