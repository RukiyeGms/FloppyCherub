using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool flying;
    int score = 0;

    [SerializeField]  GameObject pipePrefab;
    [SerializeField] int pipeCount = 4;
    [SerializeField] float spawnTime = 1f;

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject pauseButton;

    [SerializeField] TextMeshProUGUI scoreTextGameScreen;
    [SerializeField] GameObject scoreObject;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI gameOverScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    [SerializeField] GameObject startGamePanel;
    [SerializeField] GameObject newHighScoreText;
    string playerPrefsHighScoreKeyName = "HighScore";
    int currentHighScore;
    bool gotNewHighScore;


    void Start()
    {
        currentHighScore = CheckHighScore();
        newHighScoreText.SetActive(false);

        pausePanel.SetActive(false);
        UpdateScoreText();
        gameOverPanel.SetActive(false);
        scoreObject.SetActive(true);
        pauseButton.SetActive(true);
        flying = false;
        startGamePanel.SetActive(true);
        Time.timeScale = 0;
        gotNewHighScore = false;
        
        
    }

    public void ScoreUP()
    {
        score++;
        UpdateScoreText();
        if (currentHighScore < score)
        {
            if (!gotNewHighScore) 
            {
                gotNewHighScore=true;
                StartCoroutine(ShowNewHighScoreText());
                SoundEffectManager.Play("HighScore");
                return;
            }

        }

        SoundEffectManager.Play("Score");
    }

    public  int CheckScore()
    {
        return score;
    }

    public  void RestartGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene);
    }

    IEnumerator  CreatePipes(int pipecount)
    {
        for (int i = 0; i < pipeCount; i++) 
        {
            Instantiate(pipePrefab);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void OnPause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
    }

    public void OnContinue()
    {
        
        pausePanel.SetActive(false );
        pauseButton.SetActive(true);
        if (CheckFlying()) { Time.timeScale = 1f; }

    }

    void UpdateScoreText()
    {
        scoreTextGameScreen.text = $"Score: {score}";
    }

    public void OnExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        if (score > currentHighScore) 
        { UpdateHighScore(score);
          currentHighScore = score;
        }
        newHighScoreText.SetActive(false);
        highScoreText.text = $"High Score: {currentHighScore}";
        gameOverScoreText.text = $"Score: {score}";
        scoreObject.SetActive(false);
        pauseButton.SetActive(false);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        
        
    }

    public bool CheckFlying()
    {
        return flying;
    }

    public void StartFlying()
    {
        Time.timeScale = 1;
        flying = true;
        StartCoroutine(CreatePipes(pipeCount));
        startGamePanel.SetActive(false);
    }

    int CheckHighScore()
    {
        if (PlayerPrefs.HasKey(playerPrefsHighScoreKeyName))
        {
            return PlayerPrefs.GetInt(playerPrefsHighScoreKeyName);
        }
        else 
        {
            PlayerPrefs.SetInt(playerPrefsHighScoreKeyName, 0);
            return 0; 
        }
    
    }

    void UpdateHighScore(int highscore)
    {
        PlayerPrefs.SetInt(playerPrefsHighScoreKeyName , highscore);
    }

    IEnumerator ShowNewHighScoreText()
    {
        newHighScoreText.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        newHighScoreText.SetActive(false);

    }
}
