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
                //Debug.Log("Player has picked up the flag!");
                isCarriedByPlayer = true; 
                Debug.Log(isCarriedByPlayer); 
                transform.parent = other.transform;
                playerController.FlagPickedUp(this);
            }
            else
            {
                ResetFlag();
            }
        }
        if (other.gameObject.CompareTag("AI"))
        {
            Debug.Log("AI has collided with the flag!");
            if ((other.gameObject.name == "Ai player" && !isBlueFlag) || (other.gameObject.name == "Player" && isBlueFlag))
            {
                Debug.Log("AI has picked up the flag!");
                isCarriedByAI = true; 
                Debug.Log(isCarriedByAI); 
                transform.parent = other.transform;
                aiController.FlagPickedUp(this);
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
    }

    public void ResetFlag()
    {
        transform.position = isBlueFlag ? blueBase.transform.position : redBase.transform.position;
    }
}
