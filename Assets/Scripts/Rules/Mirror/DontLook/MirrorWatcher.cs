using UnityEngine;

public class MirrorWatcher : MonoBehaviour
{
    public float maxLookTime = 3f; // tempo máximo olhando pro espelho
    public LayerMask mirrorLayer;
    public float rayDistance = 10f;

   [SerializeField]
    private PlayerStatus status;

    [SerializeField] 
    private Camera playerCamera;

    private float lookTimer = 0f;

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, mirrorLayer))
        {
            lookTimer += Time.deltaTime;

            if (lookTimer >= maxLookTime)
            {
                Debug.Log("Olhou demais pro espelho!");

                if (status != null)
                {
                    status.DecreaseSanity(1); 
                }

                lookTimer = 0f;
            }
        }
        else
        {
            lookTimer = 0f;
        }
    }
}
