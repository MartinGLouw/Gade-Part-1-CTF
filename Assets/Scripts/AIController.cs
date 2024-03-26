using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class AIController : MonoBehaviour
{
    public Transform playerBase;
    public Transform aiBase;
    public Transform midBase;
    public bool PlayerCollided = false;
    public Flag playerFlag;
    public Flag aiFlag;
    private NavMeshAgent agent;

    public enum State { ChaseFlag, ChasePlayer, ReturnFlag }
    public State state;
    public PlayerController playerC;
    public Flag redFlag;
    public int AiScore = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = State.ChaseFlag;
        
    }

    void Update()
    {
        //Debug.Log("destination: " + agent.destination);
       Debug.Log("AI State: " + state);
        //Debug.Log("aiflagcarriedbyai: " + aiFlag.isCarriedByAI);
        //Debug.Log("playerflagcarriedbyplayer: " + playerFlag.isCarriedByPlayer);
        switch (state)
        {
            case State.ChaseFlag:
                ChaseFlag();
                if (playerFlag.IsCarriedByPlayer()) 
                {
                    state = State.ChasePlayer;
                }
                else if (aiFlag.IsCarriedByAI()) 
                {
                    
                    state = State.ReturnFlag;
                }
                break;
            case State.ChasePlayer:
                ChasePlayer();
                break;
            case State.ReturnFlag:
                ReturnFlag();
                break;
        }
        
    }
    
    void ChaseFlag()
    {
        if (state == State.ChaseFlag)
        {
            if (PlayerCollided == true)
            {
                agent.SetDestination(midBase.position);
            }
            else
            {
                agent.SetDestination(playerBase.position);
            }
            
        }
    }
    void ChasePlayer()
    {
        Debug.Log("Chasing player!");
        // Assuming the player object is tagged "Player" in Unity
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            agent.SetDestination(player.transform.position);
        }
    }
    void ReturnFlag()
    {
        if (state == State.ReturnFlag)
        {
            agent.SetDestination(aiBase.position);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (aiFlag.IsCarriedByAI() && other.gameObject.CompareTag("Player")) 
        {
            
            Debug.Log("AI has collided with player!");
            
            redFlag.DropFlag();
            state = State.ChaseFlag;
            aiFlag.transform.parent = null;
            aiFlag.transform.position = aiFlag.MiddleRed.transform.position;
            aiFlag.isCarriedByAI = false;
            PlayerCollided = true;
            
        }
        Debug.Log("AI has collided with something!" + aiFlag.isCarriedByAI);
        // Check if the AI has reached the player's flag and is not already carrying it
        if (other.gameObject == aiFlag.gameObject && !aiFlag.isCarriedByAI)
        {
            aiFlag.isCarriedByAI = true; 
            state = State.ReturnFlag;
        }
        // Check if the AI has reached its base with the flag
        else if (other.gameObject == aiBase.gameObject && aiFlag.isCarriedByAI)
        {
            aiFlag.isCarriedByAI = false;
            aiFlag.ResetFlag();
            aiFlag.DropFlag();
            ScoreManager.Instance.IncrementAIScore(); // Increment AI score
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
        
    }



    public void FlagPickedUp(Flag flag) 
    {
        if (flag == playerFlag)
        {
            playerFlag = flag;
        }
        else
        {
            aiFlag = flag;
        }
    }
    
}
