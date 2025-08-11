using UnityEngine;

public class Padlock : MonoBehaviour, IInteractable
{
    [Header("Logic")]
    public KeyType expectedKey;
    [SerializeField] private bool unlocked;

    [Header("Animator no Parent")]
    public Animator animator;                 
    public string unlockTrigger = "Unlock";   
    public string openStateName = "PadlockOpen";

    [Header("Refs")]
    public PuzzleManager puzzleManager;

    void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
        if (puzzleManager == null) puzzleManager = FindFirstObjectByType<PuzzleManager>();
        if (animator == null) Debug.LogError("[Padlock] Animator não atribuído no " + name);
    }

    public void Interact(GameObject interactor)
    {
        Debug.Log("[Padlock] Interact chamado em " + name);
        if (unlocked) { Debug.Log("[Padlock] Já está aberto."); return; }

        var keyInHand = interactor.GetComponent<InHandKey>();
        if (keyInHand == null) { Debug.LogWarning("[Padlock] Player sem InHandKey."); return; }

        var got = keyInHand.GetKey();
        Debug.Log($"[Padlock] Chave na mão: {got} | Esperada: {expectedKey}");

        if (got == expectedKey)
        {
            unlocked = true;

            if (animator != null)
            {
                if (!string.IsNullOrEmpty(unlockTrigger))
                {
                    Debug.Log("[Padlock] SetTrigger(Unlock)");
                    animator.SetTrigger(unlockTrigger);
                }

                // Fallback garante tocar o state aberto
                if (!string.IsNullOrEmpty(openStateName))
                {
                    Debug.Log("[Padlock] Play(PadlockOpen)");
                    animator.Play(openStateName, 0, 0f);
                }
            }

            keyInHand.ClearKey();
            puzzleManager?.CheckPuzzle();
        }
        else
        {
            Debug.Log("[Padlock] Chave errada.");
        }
    }

    public bool IsUnlocked() => unlocked;

    
    public void ForceOpen()
    {
        Debug.Log("[Padlock] ForceOpen()");
        unlocked = true;
        if (animator)
        {
            if (!string.IsNullOrEmpty(unlockTrigger))
                animator.SetTrigger(unlockTrigger);
            if (!string.IsNullOrEmpty(openStateName))
                animator.Play(openStateName, 0, 0f);
        }
    }
}
