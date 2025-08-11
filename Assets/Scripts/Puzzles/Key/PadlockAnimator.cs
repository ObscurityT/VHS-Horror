using System.Collections;
using UnityEngine;

public class PadlockAnimator : MonoBehaviour
{
    [Header("Parte de cima)")]
    public Transform topPart;

    [Header("Movimento")]
    public float moveUpDistance = 0.05f; // 5 cm (ajuste)
    public float duration = 0.25f;

    Vector3 _closedPos;
    Vector3 _openPos;
    bool _isOpen;

    void Awake()
    {
        if (topPart == null)
        {
            Debug.LogError("PadlockAnimator: 'topPart' não atribuído.");
            enabled = false; return;
        }
        _closedPos = topPart.localPosition;
        _openPos = _closedPos + Vector3.up * moveUpDistance;
    }

    public void Unlock()
    {
        if (_isOpen || !isActiveAndEnabled) return;
        StopAllCoroutines();
        StartCoroutine(MoveTo(_openPos));
        _isOpen = true;
    }

    public void Lock()
    {
        if (!_isOpen || !isActiveAndEnabled) return;
        StopAllCoroutines();
        StartCoroutine(MoveTo(_closedPos));
        _isOpen = false;
    }

    IEnumerator MoveTo(Vector3 target)
    {
        float t = 0f;
        Vector3 from = topPart.localPosition;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            topPart.localPosition = Vector3.Lerp(from, target, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        topPart.localPosition = target;
    }
}
