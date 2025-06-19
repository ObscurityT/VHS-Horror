using UnityEngine;

public class PadlockAnimator : MonoBehaviour
{
    public Transform arcTransform; // referência ao CadeadoCima
    public float liftHeight = 0.2f;
    public float liftSpeed = 2f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool unlocked = false;

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
        if (unlocked && arcTransform != null)
        {
            arcTransform.localPosition = Vector3.Lerp(arcTransform.localPosition, targetPosition, Time.deltaTime * liftSpeed);
        }
    }

    public void Unlock()
    {
        unlocked = true;
    }
}
