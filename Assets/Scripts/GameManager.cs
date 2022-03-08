using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;

    public static bool gameIsActive { get; private set; }

    // workaround for restarting the level
    private void Awake()
    {
        Time.timeScale = 1;  
        gameIsActive = true;
    }

    public void PauseGame()
    {
        gameIsActive = false;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        gameOverMenu.SetActive(false);
        gameIsActive = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        gameIsActive = true;
    }

    public void EndGame()
    {
        gameIsActive = false;
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
