using UnityEngine;

public class Padlock : MonoBehaviour, IInteractable
{
    [Header("Configuração")]
    public KeyType expectedKey;

    [Header("Referências")]
    public PuzzleManager puzzleManager;
    public PadlockAnimator padlockAnimator;
    public PuzzleAudioHelper audioHelper;

    private bool unlocked = false;

    public void Interact(GameObject interactor)
    {
        if (unlocked) return;

        // Verifica se o jogador está com uma chave na mão
        InHandKey keyInHand = interactor.GetComponent<InHandKey>();
        if (keyInHand == null)
        {
            Debug.Log(" Nenhuma chave na mão.");
            return;
        }

        // Verifica se é a chave correta
        if (keyInHand.GetKey() == expectedKey)
        {
            unlocked = true;
            Debug.Log("Cadeado destrancado com a chave: " + expectedKey);

            // Toca som de sucesso
            audioHelper?.PlaySuccessSound();

            // Limpa a chave da mão do jogador
            keyInHand.ClearKey();

            // Executa a animação do arco subindo
            padlockAnimator?.Unlock();

            // Checa se o puzzle foi resolvido
            puzzleManager?.CheckPuzzle();
        }
        else
        {
            Debug.Log("Chave incorreta: " + keyInHand.GetKey());
            audioHelper?.PlayFailSound();
        }
    }

    public bool IsUnlocked() => unlocked;
}
