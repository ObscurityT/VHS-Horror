using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private Camera playerCamera;

    public float walkSpeed = 2.5f;
    public float runSpeed = 3.3f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;

    private float melodySanityTimer = 0f;
    public MelodyController melodyController;

    private PlayerStatus status;

    private Rigidbody rb;
    public float cameraPitch = 0f;

    public bool canLook = true;
    private bool isRunning;

    private float footstepTimer;

    public AudioSource Passos;
    public AudioClip woodClip;
    public AudioClip rockClip;
    public AudioClip gramClip;
    public float pitchMin = 0.85f;
    public float pitchMax = 1.05f;
    public float resetDelay = 1f;
    public string groundTag;

    private string currentSurface = "";
    private float timeSinceStopped = 0f;
    private bool pitchReset = true;

    private void Start()
    {
        Debug.Log("[DEBUG] Posição atual do jogador no Start(): " + transform.position);
        Debug.DrawRay(transform.position, Vector3.down * 10f, Color.red, 10f);
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
        Cursor.lockState = CursorLockMode.Locked;
        rb.freezeRotation = true;

    }

    private void Update()
    {


        if (canLook)
        {
            Debug.Log("Mouse: " + Input.GetAxis("Mouse X"));
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

                if (hit.collider != null)
                {
                    Debug.Log("Atingiu objeto: " + hit.collider.name);

                    var interObj = hit.collider.GetComponent<IInteractable>();
                    if (interObj == null)
                    {
                        Debug.LogWarning("Nenhum IInteractable encontrado no objeto " + hit.collider.name);
                    }
                    else
                    {
                        Debug.Log("IInteractable encontrado, chamando Interact");
                        interObj.Interact(gameObject);
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");


        bool isMoving = Mathf.Abs(moveX) > 0.01f || Mathf.Abs(moveZ) > 0.01f;

        if (melodyController != null && melodyController.IsMelodyActive())
        {
            if (isMoving)
            {
                melodySanityTimer += Time.fixedDeltaTime;
                if (melodySanityTimer >= 1f)
                {
                    status.DecreaseSanity(1);
                    melodySanityTimer = 0f;
                }
            }
            else
            {
                melodySanityTimer = 0f; 
            }
        }


        isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);

        HandleFootsteps(move);
    }

    void HandleFootsteps(Vector3 move)
    {
        bool isMoving = move.magnitude > 0.6f && IsGrounded(out groundTag);

        if (isMoving)
        {
            AudioClip desiredClip = null;

            if (groundTag == "Madeira")
                desiredClip = woodClip;
            else if (groundTag == "Pedra")
                desiredClip = rockClip;
            else if (groundTag == "Grama") // <-- ADICIONADO
                desiredClip = gramClip;

            if (desiredClip != null)
            {
                float previousTime = Passos.time;

                if (pitchReset)
                {
                    Passos.pitch = Random.Range(pitchMin, pitchMax);
                    pitchReset = false;
                }

                if (Passos.clip != desiredClip)
                {
                    Passos.clip = desiredClip;
                    Passos.time = previousTime;
                    Passos.Play();
                }
                else if (!Passos.isPlaying)
                {
                    Passos.Play();
                }
            }

            timeSinceStopped = 0f;
        }
        else
        {
            if (Passos.isPlaying)
                Passos.Stop();

            timeSinceStopped += Time.deltaTime;
            if (timeSinceStopped >= resetDelay)
                pitchReset = true;
        }
    }

    bool IsGrounded(out string tag)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f))
        {
            tag = hit.collider.tag;
            return true;
        }
        tag = null;
        return false;
    }
}
