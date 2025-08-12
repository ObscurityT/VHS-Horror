using System.Collections;
using UnityEngine;

public class PadlockAnimator : MonoBehaviour
{
    [Header("Referência do arco")]
    public Transform arcTransform;

    [Header("Animação de subida")]
    public float liftHeight = 0.1f;
    public float liftSpeed = 2f;

    [Header("Queda do cadeado")]
    public float dropDelay = 1f;
    public float destroyDelay = 3f;

    [Header("Áudio")]
    public string destrancarSound = "mecanismo_girando";
    public string cairSound = "metal_caindo";

    private Vector3 initialPosition;
    private Vector3 liftedPosition;
    private bool isUnlocking = false;
    private bool hasDropped = false;

    private void Start()
    {
        if (arcTransform == null)
        {
            Debug.LogError("Cadeado_Cima não atribuído!");
            enabled = false;
            return;
        }

        initialPosition = arcTransform.localPosition;
        liftedPosition = initialPosition + Vector3.up * liftHeight;
    }

    private void Update()
    {
        if (isUnlocking && !hasDropped)
        {
            arcTransform.localPosition = Vector3.Lerp(arcTransform.localPosition, liftedPosition, Time.deltaTime * liftSpeed);

            if (Vector3.Distance(arcTransform.localPosition, liftedPosition) < 0.005f)
            {
                hasDropped = true;
                StartCoroutine(DropArc());
            }
        }
    }

    public void Unlock()
    {
        if (isUnlocking) return;

        isUnlocking = true;

        if (!string.IsNullOrEmpty(destrancarSound))
        {
            AudioSystem.AudioManager.Instance.PlaySFX(destrancarSound);
        }

        Debug.Log("Cadeado desbloqueado, iniciando animação.");
    }

    private IEnumerator DropArc()
    {
        yield return new WaitForSeconds(dropDelay);

        if (!string.IsNullOrEmpty(cairSound))
            AudioSystem.AudioManager.Instance.PlaySFX(cairSound);

     
        GameObject padlockObject = transform.root.gameObject;

        if (!padlockObject.TryGetComponent<Rigidbody>(out var rb))
            rb = padlockObject.AddComponent<Rigidbody>();

        if (!padlockObject.TryGetComponent<Collider>(out var col))
            padlockObject.AddComponent<BoxCollider>();

        rb.mass = 1f;
        rb.angularDamping = 0.05f;

        Destroy(padlockObject, destroyDelay);
    }
}
