using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Transform playerBase;
    public Transform aiBase;
    public Flag playerFlag;
    public Flag aiFlag;
    private NavMeshAgent agent;
    private enum State { ChaseFlag, ChasePlayer, ReturnFlag }
    private State state;
    public PlayerController playerC;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = State.ChaseFlag;
    }

    void Update()
    {
        switch (state)
        {
            case State.ChaseFlag:
                ChaseFlag();
                if (playerFlag.IsCarried())
                {
                    state = State.ChasePlayer;
                }
                else if (aiFlag.IsCarried())
                {
                    state = State.ReturnFlag;
                }
                break;
            case State.ChasePlayer:
                ChasePlayer();
                if (playerFlag.IsCarried())
                {
                    state = State.ChaseFlag;
                }
                else if (aiFlag.IsCarried())
                {
                    state = State.ReturnFlag;
                }
                break;
            case State.ReturnFlag:
                ReturnFlag();
                if (aiFlag.IsCarried())
                {
                    state = State.ChaseFlag;
                }
                break;
        }
        if (TouchedByPlayer())
        {
            playerFlag.DropFlag();
            playerFlag.ResetFlag();
            aiFlag.ResetFlag();
        }
    }
    bool TouchedByPlayer()
    {
        // Implement the logic to check if the AI is shot or touched by the player
        // Return true if the AI is shot or touched by the player, false otherwise
        return false;
    }

    void ChaseFlag()
    {
        agent.SetDestination(playerBase.position);
    }

    void ChasePlayer()
    {
        // Assuming the player object is tagged "Player" in Unity
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    void ReturnFlag()
    {
        agent.SetDestination(aiBase.position);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the AI has reached the player's flag
        if (other.gameObject == playerFlag.gameObject)
        {
            playerFlag.isCarried = true;
            state = State.ReturnFlag;
        }
        // Check if the AI has reached its base with the flag
        else if (other.gameObject == aiBase.gameObject && playerFlag.isCarried)
        {
            playerFlag.isCarried = false;
            playerFlag.ResetFlag();
            playerC.Aiscore++;
            Debug.Log("AI Score: " + playerC.Aiscore);
        }
    }
    public void FlagPickedUp(Flag flag) // Add this method
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
