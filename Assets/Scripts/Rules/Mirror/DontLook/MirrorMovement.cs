using UnityEngine;

public class MirrorCameraController : MonoBehaviour
{
    public Transform playerCamera;
    public Transform mirror;

    void Start()
    {
        if (playerCamera == null && Camera.main != null)
            playerCamera = Camera.main.transform;

        if (playerCamera == null)
            Debug.LogError("MirrorCameraController: playerCamera não foi atribuído.");
    }

    void LateUpdate()
    {
        if (playerCamera == null || mirror == null)
            return;

        Vector3 localPlayer = mirror.InverseTransformPoint(playerCamera.position);
        if (!IsVectorValid(localPlayer)) return;

        transform.position = mirror.TransformPoint(new Vector3(localPlayer.x, localPlayer.y, -localPlayer.z));

        Vector3 lookAtWorld = mirror.TransformPoint(new Vector3(-localPlayer.x, localPlayer.y, localPlayer.z));
        if (IsVectorValid(lookAtWorld))
            transform.LookAt(lookAtWorld);
    }

    bool IsVectorValid(Vector3 v)
    {
        return !(float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z) ||
                 float.IsInfinity(v.x) || float.IsInfinity(v.y) || float.IsInfinity(v.z));
    }
}
