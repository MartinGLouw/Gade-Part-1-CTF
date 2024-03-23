using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Singleton instance
    private static ScoreManager instance;

    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject();
                    singleton.name = "ScoreManager";
                    instance = singleton.AddComponent<ScoreManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }

    public int playerScore = 0;
    public int aiScore = 0;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI aiScoreText;
    public TextMeshProUGUI winnerText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerScoreText = GameObject.Find("PlayerScore").GetComponent<TextMeshProUGUI>();
        aiScoreText = GameObject.Find("AiScore").GetComponent<TextMeshProUGUI>();
        if(SceneManager.GetActiveScene().name == "EndScene")
        {
            winnerText = GameObject.Find("WinnerText").GetComponent<TextMeshProUGUI>();
        }
        else
        {
            winnerText = null;
        }
        
        UpdateScoreTexts();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScores();
        }
        if (aiScore == 5 || playerScore == 5)
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    // Method to increment player score
    public void IncrementPlayerScore()
    {
        playerScore++;
        UpdateScoreTexts();
        Debug.Log("Player score incremented: " + playerScore);
    }

    public void IncrementAIScore()
    {
        aiScore++;
        UpdateScoreTexts();
        Debug.Log("AI score incremented: " + aiScore);
    }

    // Method to reset scores
    public void ResetScores()
    {
        playerScore = 0;
        aiScore = 0;
        UpdateScoreTexts();
    }

    private void UpdateScoreTexts()
    {
        playerScoreText.text = "Player Score: " + playerScore.ToString();
        aiScoreText.text = "AI Score: " + aiScore.ToString();
        if (SceneManager.GetActiveScene().name == "EndScene")
        {
            if(playerScore > aiScore)
            {
                winnerText.text = "Player Wins!";
            }
            else if(aiScore > playerScore)
            {
                winnerText.text = "AI Wins!";
            }
            else
            {
                winnerText.text = "It's a tie!";
            } 
        }
        
    }
    
}
