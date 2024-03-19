using UnityEngine;

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

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
            direction = Camera.main.transform.TransformDirection(direction);
            direction.y = 0;
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

        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                direction.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            }
        }

        direction.y += Physics.gravity.y * Time.deltaTime;

        controller.Move(direction * Time.deltaTime);
        if (TouchedByAI())
        {
            blueFlag.DropFlag();
            blueFlag.ResetFlag();
            redFlag.ResetFlag();
        }
    }
    bool TouchedByAI()
    {
        // Implement the logic to check if the player is shot or touched by the AI
        // Return true if the player is shot or touched by the AI, false otherwise
        return false;
    }
    

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player has collided with something!" + blueFlag.IsCarriedByPlayer()); // Modify this line
        // Check if the player has reached their base with the flag
        if (blueFlag.IsCarriedByPlayer() && other.gameObject.CompareTag("PlayerBase")) // Modify this line
        {
            Debug.Log("Player has scored!");
            score++;
            Debug.Log("Score: " + score);
            blueFlag.DropFlag();
            blueFlag.ResetFlag();
            redFlag.ResetFlag();
            FS.RespawnFlags();
        }
    }

    public void FlagPickedUp(Flag flag) // Add this method
    {
        if (flag.isBlueFlag)
        {
            blueFlag = flag;
            blueFlag.isCarriedByPlayer = true; // Add this line
        }
        else
        {
            redFlag = flag;
            redFlag.isCarriedByPlayer = true; // Add this line
        }
    }
}
