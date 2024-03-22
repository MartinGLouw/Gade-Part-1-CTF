using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Singleton instance
    private static ScoreManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // This will make the ScoreManager GameObject persist across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

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

    // Score variables
    public int playerScore = 0;
    public int aiScore = 0;

    // Method to increment player score
    public void IncrementPlayerScore()
    {
        playerScore++;
        Debug.Log("Player score incremented: " + playerScore);
    }

    public void IncrementAIScore()
    {
        aiScore++;
        Debug.Log("AI score incremented: " + aiScore);
    }

    // Method to reset scores
    public void ResetScores()
    {
        playerScore = 0;
        aiScore = 0;
    }
}