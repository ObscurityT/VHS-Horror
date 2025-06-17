using System.Text;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Velocity")]
    public float walkSpeed = 2.5f;
    public float runSpeed =  4.0f;
    public float gravity = -9.81f;

    [Header("Components")]
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;
    public bool isRunning;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if (cameraTransform != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //Camera direction movement
        Vector3 moveDirection = cameraTransform.forward * moveZ + cameraTransform.right * moveX;
        moveDirection.y = 0;
        moveDirection.Normalize();

        //Shift between Walk and Run
        isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        controller.Move(moveDirection *  currentSpeed * Time.deltaTime);

        if(controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
