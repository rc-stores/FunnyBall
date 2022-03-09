using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameIsActive { get; private set; }

    [SerializeField] private GameObject pauseMenuGO;
    [SerializeField] private GameObject gameOverMenuGO;

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
        pauseMenuGO.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        gameOverMenuGO.SetActive(false);
        gameIsActive = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenuGO.SetActive(false);
        gameIsActive = true;
    }

    public void EndGame()
    {
        gameIsActive = false;
        Time.timeScale = 0;
        gameOverMenuGO.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
