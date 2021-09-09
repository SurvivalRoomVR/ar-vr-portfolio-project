using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopPlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 3.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private Transform cameraTransform;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerSpeed *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed /= 2;
        }
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
