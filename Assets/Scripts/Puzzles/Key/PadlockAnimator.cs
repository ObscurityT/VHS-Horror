using System.Collections;
using UnityEngine;

public class PadlockAnimator : MonoBehaviour
{
    [Header("References")]
    public Transform arcTransform; // CadeadoCima

    [Header("Animation Settings")]
    public float liftHeight = 0.1f;
    public float liftSpeed = 2f;

    [Header("Padlock Drop")]
    public float dropDelay = 1f;        // quanto tempo depois do levantamento
    public float destroyDelay = 5f;     // quanto tempo até sumir após cair

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool unlocked = false;
    private bool hasDropped = false;

    void Start()
    {
        if (arcTransform != null)
        {
            originalPosition = arcTransform.localPosition;
            targetPosition = originalPosition + Vector3.up * liftHeight;
        }
    }

    void Update()
    {
        if (unlocked && !hasDropped && arcTransform != null)
        {
            arcTransform.localPosition = Vector3.Lerp(arcTransform.localPosition, targetPosition, Time.deltaTime * liftSpeed);

            if (Vector3.Distance(arcTransform.localPosition, targetPosition) < 0.01f)
            {
                hasDropped = true;
                StartCoroutine(DropSelf());
            }
        }
    }

    public void Unlock()
    {
        unlocked = true;
    }

    private IEnumerator DropSelf()
    {
        yield return new WaitForSeconds(dropDelay);

        
        if (!TryGetComponent<Rigidbody>(out var rb))
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        
        if (!TryGetComponent<Collider>(out var col))
        {
            gameObject.AddComponent<BoxCollider>();
        }

        Destroy(gameObject, destroyDelay);
    }
}
