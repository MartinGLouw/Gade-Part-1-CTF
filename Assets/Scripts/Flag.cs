using UnityEngine;

public class Flag : MonoBehaviour
{
    public GameObject blueBase;
    public GameObject redBase;
    public bool isBlueFlag;
    public bool isCarriedByPlayer = false; 
    public bool isCarriedByAI = false; 
    public PlayerController playerController; 
    public AIController aiController;
    public FlagSpawner FS;
    public GameObject MiddleBlue;
    public GameObject MiddleRed;

    void Start()
    {
        isBlueFlag = transform.position == blueBase.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if ((other.gameObject.name == "Player" && isBlueFlag) || (other.gameObject.name == "AI" && !isBlueFlag))
            {
                isCarriedByPlayer = true; 
                transform.parent = other.transform;
                playerController.FlagPickedUp(this);
                aiController.state = AIController.State.ChasePlayer;
            }
            else
            {
                ResetFlag();
            }
        }
        if (other.gameObject.CompareTag("AI"))
        {
            if ((other.gameObject.name == "Ai player" && !isBlueFlag) || (other.gameObject.name == "Player" && isBlueFlag))
            {
                // Add an additional check to ensure that the AI only picks up the red flag
                if (!isBlueFlag)
                {
                    isCarriedByAI = true; 
                    transform.parent = other.transform;
                    aiController.FlagPickedUp(this);
                }
            }
            else
            {
                ResetFlag();
            }
        }
    }



    public bool IsCarriedByPlayer() 
    {
        return isCarriedByPlayer;
    }

    public bool IsCarriedByAI() 
    {
        return isCarriedByAI;
    }

    public void DropFlag()
    {
        isCarriedByPlayer = false; 
        isCarriedByAI = false; 
        transform.parent = null; 
        aiController.state = AIController.State.ChaseFlag;
    }

    public void ResetFlag()
    {
        // Move blue flag to red base
        GameObject.FindWithTag("BlueFlag").transform.position = redBase.transform.position;

        // Move red flag to blue base
        GameObject.FindWithTag("RedFlag").transform.position = blueBase.transform.position;
        isCarriedByPlayer = false; 
        isCarriedByAI = false; 
        transform.parent = null; 

        

        //transform.position = isBlueFlag ? blueBase.transform.position : redBase.transform.position;
        
    }

}
