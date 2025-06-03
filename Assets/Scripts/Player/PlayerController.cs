using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;

    private Rigidbody rb;
    public float cameraPitch = 0f;

    private void Start()
    {
       rb = GetComponent<Rigidbody>();
       Cursor.lockState = CursorLockMode.Locked;
       rb.freezeRotation = true;

    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        //Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
        
    }
}
