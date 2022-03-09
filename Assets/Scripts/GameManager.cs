using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool GameIsActive { get; private set; }

    [SerializeField] private GameObject _pauseMenuGO;
    [SerializeField] private GameObject _gameOverMenuGO;

    private const string GAME_SCENE_NAME = "Game";

    // workaround for restarting the level
    private void Awake()
    {
        Time.timeScale = 1;  
        GameIsActive = true;
    }

    public void PauseGame()
    {
        GameIsActive = false;
        Time.timeScale = 0;
        _pauseMenuGO.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(GAME_SCENE_NAME);
        _gameOverMenuGO.SetActive(false);
        GameIsActive = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _pauseMenuGO.SetActive(false);
        GameIsActive = true;
    }

    public void EndGame()
    {
        GameIsActive = false;
        Time.timeScale = 0;
        _gameOverMenuGO.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
