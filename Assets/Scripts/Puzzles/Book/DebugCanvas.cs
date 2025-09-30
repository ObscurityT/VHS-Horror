using UnityEngine;

public class DebugCanvas : MonoBehaviour
{
    void OnDrawGizmos()
    {
        foreach (var img in GetComponentsInChildren<UnityEngine.UI.Image>(true))
        {
            RectTransform rt = img.GetComponent<RectTransform>();
            Gizmos.color = Color.red;
            Vector3 worldPos = rt.position;
            Gizmos.DrawWireCube(worldPos, rt.rect.size);
        }
    }
}
