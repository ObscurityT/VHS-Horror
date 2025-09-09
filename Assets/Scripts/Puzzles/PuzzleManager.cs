using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    public Padlock[] padlocks;

    [Tooltip("Chamado quando todos os cadeados estiverem destrancados.")]
    public UnityEvent onSolved;

    private bool _alreadySolved;

    public void CheckPuzzle()
    {
        if (_alreadySolved) return;

        if (padlocks == null || padlocks.Length == 0)
        {
            Debug.LogWarning("[PuzzleManager] Nenhum padlock atribuído.");
            return;
        }

        for (int i = 0; i < padlocks.Length; i++)
        {
            var p = padlocks[i];
            if (p == null || !p.IsUnlocked())
                return; // ainda falta
        }

        _alreadySolved = true;
        Debug.Log("[PuzzleManager] Puzzle resolvido!");
        onSolved?.Invoke();
    }
}
