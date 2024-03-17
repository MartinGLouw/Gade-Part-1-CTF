using UnityEngine;

public class Flag : MonoBehaviour
{
    public GameObject blueBase;
    public GameObject redBase;
    public bool isBlueFlag;
    public bool isCarried = false;
    public PlayerController playerController; // Add this line

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
                playerController.FlagPickedUp(this); // Notify the PlayerController
            }
            else
            {
                ResetFlag();
            }
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