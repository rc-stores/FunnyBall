using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText, menuScoreText, highScoreText;

    private const string PREFS_HIGH_SCORE = "HighScore";

    private int score = 0, highScore;
    private float time = 0;
    [SerializeField] private float scoreStep = 1; // increment the score every second

    void Start()
    {
        highScore = PlayerPrefs.GetInt(PREFS_HIGH_SCORE, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float oldModulus = time % scoreStep;
        time += Time.deltaTime;
        if (time % scoreStep < oldModulus)
        {
            if (++score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt(PREFS_HIGH_SCORE, highScore);
            }
        }
        scoreText.text = score.ToString();
        if (!GameManager.gameIsActive)
        {
            menuScoreText.text = score.ToString();
            highScoreText.text = highScore.ToString();
        }
    }
}
