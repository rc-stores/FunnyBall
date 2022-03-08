using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Game");
        //Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
