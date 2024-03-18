using UnityEngine;

public class Flag : MonoBehaviour
{
    public GameObject blueBase;
    public GameObject redBase;
    public bool isBlueFlag;
    public bool isCarried = false;
    public PlayerController playerController; 
    public AIController aiController; // Add this line

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
                Debug.Log("Player has picked up the flag!");
                isCarried = true;
                Debug.Log(isCarried);
                transform.parent = other.transform;
                playerController.FlagPickedUp(this);
            }
            else
            {
                ResetFlag();
            }
        }
        // Add this block to handle the AI picking up the flag
        if (other.gameObject.CompareTag("AI"))
        {
            if ((other.gameObject.name == "AI" && !isBlueFlag) || (other.gameObject.name == "Player" && isBlueFlag))
            {
                Debug.Log("AI has picked up the flag!");
                isCarried = true;
                Debug.Log(isCarried);
                transform.parent = other.transform;
                aiController.FlagPickedUp(this); // Notify the AIController
            }
            else
            {
                ResetFlag();
            }
        }
        if (other.gameObject.CompareTag("Player") && !isBlueFlag)
        {
            Debug.Log("Player has picked up the opponent's flag!");
            transform.position = blueBase.transform.position;
        }
        else if (other.gameObject.CompareTag("AI") && isBlueFlag)
        {
            Debug.Log("AI has picked up the opponent's flag!");
            transform.position = redBase.transform.position;
        }
    }
    public bool IsCarried()
    {
        return isCarried;
    }

    public void DropFlag()
    {
        isCarried = false;
        transform.parent = null; 
    }

    public void ResetFlag()
    {
        transform.position = isBlueFlag ? blueBase.transform.position : redBase.transform.position;
    }
}