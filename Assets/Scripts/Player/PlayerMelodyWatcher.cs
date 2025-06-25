using UnityEngine;

public class PlayerMelodyWatcher : MonoBehaviour
{
    public PlayerStatus playerStatus;

    private bool melodyActive = false;
    private float timeWithoutMoving = 0f;
    public float tempoDePerdao = 1.5f; // tempo de toler�ncia ap�s in�cio
    private float tempoDesdeMelodyStart = 0f;


    void Start()
    {
        if (playerStatus == null)
        {
            playerStatus = GetComponent<PlayerStatus>();
            if (playerStatus == null)
                Debug.LogError("PlayerStatus n�o foi atribu�do nem encontrado!");
        }
    }

    private void OnEnable()
    {
        GameEvents.OnMelodyStart += AtivarVerificacao;
        GameEvents.OnMelodyEnd += DesativarVerificacao;
    }

    private void OnDisable()
    {
        GameEvents.OnMelodyStart -= AtivarVerificacao;
        GameEvents.OnMelodyEnd -= DesativarVerificacao;
    }

    private void AtivarVerificacao()
    {
        melodyActive = true;
        tempoDesdeMelodyStart = 0f;
        timeWithoutMoving = 0f; 
        Debug.Log("Melodia started - monitorando movimento");
    }

    private void DesativarVerificacao()
    {
        melodyActive = false;
        Debug.Log("Melodia finished - parando monitoramento");
    }

    void Update()
    {
        if (!melodyActive) return;

        tempoDesdeMelodyStart += Time.deltaTime;
        Debug.Log("Tempo desde in�cio da melodia: " + tempoDesdeMelodyStart);

        // Enquanto dentro do tempo de perd�o, n�o verifica movimento
        if (tempoDesdeMelodyStart < tempoDePerdao)
        {
            Debug.Log("Dentro do tempo de perd�o.");
            return; }

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        if (Mathf.Abs(inputX) > 0.01f || Mathf.Abs(inputZ) > 0.01f)
        {
            Debug.Log("Jogador se moveu durante a melodia!");
            playerStatus.DecreaseSanity(10f);
            melodyActive = false;
            return;
        }
    }
}
