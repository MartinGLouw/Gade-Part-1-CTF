using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float sprintSpeed = 20.0f;
    public float jumpHeight = 2.0f;
    private Vector3 direction;
    private CharacterController controller;

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
    }
}