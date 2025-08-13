using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;
    public float gravity = -9.81f;
    private Rigidbody rb;
    private Vector3 velocity;

    public Transform cameraTransform; // 👉 Referência à câmara

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude < 0.01f)
            rb.linearVelocity = Vector3.zero;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 🧭 Direção com base na câmara (com rotação de 180º em conta)
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (right * horizontal + forward * vertical).normalized;

        if (moveDirection.magnitude > 0f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        // Movimento com base na direção ajustada
        Vector3 velocity = moveDirection * moveSpeed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

        // Gravidade extra (opcional)
        if (!Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);
        }
    }
}

