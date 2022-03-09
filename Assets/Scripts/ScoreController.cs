using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText, _menuScoreText, _highScoreText;

    private const string PREFS_HIGH_SCORE = "HighScore";

    private int _score = 0, _highScore;
    private float _time = 0;
    [SerializeField] private float _scoreStep = 1; // increment the score every second

    void Start()
    {
        _highScore = PlayerPrefs.GetInt(PREFS_HIGH_SCORE, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float oldModulus = _time % _scoreStep;
        _time += Time.deltaTime;
        if (_time % _scoreStep < oldModulus)
        {
            if (++_score > _highScore)
            {
                _highScore = _score;
                PlayerPrefs.SetInt(PREFS_HIGH_SCORE, _highScore);
            }
        }
        _scoreText.text = _score.ToString();
        if (!GameManager.GameIsActive)
        {
            _menuScoreText.text = _score.ToString();
            _highScoreText.text = _highScore.ToString();
        }
    }
}
