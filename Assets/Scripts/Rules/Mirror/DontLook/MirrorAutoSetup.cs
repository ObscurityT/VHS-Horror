using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MirrorAutoSetup : MonoBehaviour
{
    private Camera mirrorCamera;
    private MeshRenderer mirrorRenderer;

    void Awake()
    {
        mirrorCamera = GetComponentInChildren<Camera>(includeInactive: true);
        mirrorRenderer = GetComponent<MeshRenderer>();

        if (mirrorCamera == null || mirrorRenderer == null)
        {
            Debug.LogError("MirrorAutoSetup: faltando câmera ou renderer!");
            return;
        }

        mirrorCamera.enabled = true;

        RenderTexture renderTex = new RenderTexture(512, 512, 24);
        renderTex.name = "MirrorTex_" + GetInstanceID();
        renderTex.Create();

        mirrorCamera.targetTexture = renderTex;

        Material instancedMat = new Material(mirrorRenderer.sharedMaterial);
        instancedMat.mainTexture = renderTex;
        mirrorRenderer.material = instancedMat;
    }
}
