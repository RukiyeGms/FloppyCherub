using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    int score = 0;

    [SerializeField]  GameObject pipePrefab;
    [SerializeField] int pipeCount = 4;
    [SerializeField] float spawnTime = 1f;

    private void Awake()
    {
        if (instance == null) { 
            instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
        
        

    }

    void Start()
    {
        StartCoroutine(CreatePipes(pipeCount));
        
    }

    public static void ScoreUP()
    {
        instance.score++;
    }

    public static int CheckScore()
    {
        return instance.score;
    }

    public  void RestartGame()
    {
        score = 0;
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        StartCoroutine(CreatePipes(pipeCount));
    }

    IEnumerator  CreatePipes(int pipecount)
    {
        for (int i = 0; i < pipeCount; i++) 
        {
            Instantiate(pipePrefab);
            yield return new WaitForSeconds(spawnTime);
        }
    }


}
