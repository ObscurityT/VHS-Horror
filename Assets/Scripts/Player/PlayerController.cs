using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private Camera playerCamera;

    public float walkSpeed = 2.5f;
    public float runSpeed = 3.3f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;

    private Rigidbody rb;
    public float cameraPitch = 0f;

    public bool canLook = true;
    private bool isRunning;


    private void Start()
    {
       rb = GetComponent<Rigidbody>();
       Cursor.lockState = CursorLockMode.Locked;
       rb.freezeRotation = true;

    }

    private void Update()
    {
        if (canLook)
        {
            //Mouse look
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            cameraPitch -= mouseY;
            cameraPitch = Mathf.Clamp(cameraPitch, -85f, 85f);

            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
        HandleInteraction();

    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionDistance, Color.green, 1f);
            Debug.Log("Interact?");
            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
            {
                Debug.Log("Acertou algo: " + hit.collider.gameObject.name);

                if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactable.Interact(gameObject);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);

    }
}
