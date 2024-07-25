using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    private float x;
    private float z;

    public float speed = 12f;
    public float gravity = -12f;
    public float jumpHeight = 3f;

    private float jumpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    { 
        playerInput();
        groundMovement();
    }

    private void playerInput()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
    }

    private void groundMovement()
    {
        Vector3 move = new Vector3(x, 0, z);
        move = transform.TransformDirection(move);

        move *= speed;

        move.y = Gravity();

        controller.Move(move * Time.deltaTime);
    }

    private float Gravity()
    {
        if (controller.isGrounded)
        {
            jumpSpeed = -1f;

            if (Input.GetButtonDown("Jump"))
            {
                jumpSpeed = Mathf.Sqrt(jumpHeight * gravity * -2);
            }
        }
        else
        {
            jumpSpeed += gravity * Time.deltaTime;
        }

        return jumpSpeed;
    }
}
