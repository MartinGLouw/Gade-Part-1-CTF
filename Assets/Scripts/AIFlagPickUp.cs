using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlagPickUp : MonoBehaviour
{
    public AIController aiController;
    public bool isCarriedByAI = false;
    public GameObject PlayerFlag;
    public GameObject AIFlag;
        
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("AI has collided with the flag!");
        if (other.gameObject.CompareTag("AI"))
        {
      
            if (other.gameObject.name == "AI")
            {
                Debug.Log("AI has picked up the flag!");
                isCarriedByAI = true; 
                Debug.Log(isCarriedByAI); 
                transform.parent = other.transform;
                
            }
            
        }
        {
            
        }
    }
}
