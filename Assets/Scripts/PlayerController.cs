using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float sprintSpeed = 20.0f;
    public float jumpHeight = 2.0f;
    public Flag blueFlag;
    public Flag redFlag;
    private Vector3 direction;
    private CharacterController controller;
    public int score = 0;
    public int Aiscore = 0;
    public FlagSpawner FS;
    public AIController AI;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.height = 2.0f; 
        blueFlag.isCarriedByPlayer = false;
    }

    

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
            direction = Camera.main.transform.TransformDirection(direction);
            direction.y = 0; // Ignore vertical movement
            direction = direction.normalized;
            this.transform.forward = direction;
        }
        else
        {
            direction = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(direction * sprintSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(direction * speed * Time.deltaTime);
        }

        

        controller.Move(direction * Time.deltaTime);
        
    }
    
    

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player has collided with something!" + blueFlag.IsCarriedByPlayer()); 
        // Check if the player has reached their base with the flag
        if (blueFlag.IsCarriedByPlayer() && other.gameObject.CompareTag("PlayerBase"))
        {
            blueFlag.DropFlag();
            blueFlag.ResetFlag();
            redFlag.ResetFlag();
            ScoreManager.Instance.IncrementPlayerScore(); 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (blueFlag.IsCarriedByPlayer() && other.gameObject.CompareTag("AI"))
        {
            Debug.Log("Player has collided with AI! WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW");
            AI.state = AIController.State.ChaseFlag;
            blueFlag.DropFlag();

            blueFlag.ResetFlag();
        }
        
        if (transform.position.y > 1.0f) 
           {
               Vector3 resetPosition = new Vector3(transform.position.x, 1.0f, transform.position.z); 
               transform.position = resetPosition;
           }
    }

    public void FlagPickedUp(Flag flag)
    {
        if (flag.isBlueFlag)
        {
            blueFlag = flag;
            blueFlag.isCarriedByPlayer = true; 
        }
        else
        {
            redFlag = flag;
            redFlag.isCarriedByPlayer = true; 
        }
    }
}
