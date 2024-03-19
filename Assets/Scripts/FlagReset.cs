using UnityEngine;

public class FlagReset : MonoBehaviour
{
    public GameObject blueBase;
    public GameObject redBase;
    public Flag blueFlag;
    public Flag redFlag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (blueFlag.IsCarriedByPlayer()) 
            {
                ResetBlueFlag(); 
            }
            if (redFlag.IsCarriedByPlayer()) 
            {
                ResetRedFlag(); 
            }
        }
        if (other.gameObject.CompareTag("AI"))
        {
            if (blueFlag.IsCarriedByAI()) 
            {
                ResetBlueFlag(); 
            }
            if (redFlag.IsCarriedByAI()) 
            {
                ResetRedFlag(); 
            }
        }
    }

    public void ResetBlueFlag() 
    {
        blueFlag.isCarriedByPlayer = false; 
        blueFlag.isCarriedByAI = false; 
        blueFlag.transform.position = blueBase.transform.position;
    }

    public void ResetRedFlag() 
    {
        redFlag.isCarriedByPlayer = false; 
        redFlag.isCarriedByAI = false; 
        redFlag.transform.position = redBase.transform.position;
    }
}