using UnityEngine;
using TMPro;

public class InteracaoObjeto : MonoBehaviour
{
    [Header("UI")]
    public GameObject textoInteracao;

    [Header("Inspeção")]
    public Transform inspectionPoint; // Empty posicionado na frente da câmera
    public float velocidadeRotacao = 100f;

    [Header("Player")]
    public MonoBehaviour playerMovement; // Script de movimento do player

    [Header("Som")]
    public AudioClip somInspecao; // som ao começar a inspecionar
    private AudioSource audioSource;

    private bool emProximidade = false;
    private bool inspecionando = false;

    private Vector3 posicaoOriginal;
    private Quaternion rotacaoOriginal;

    void Start()
    {
        posicaoOriginal = transform.position;
        rotacaoOriginal = transform.rotation;

        if (textoInteracao != null)
            textoInteracao.SetActive(false);

        // pega ou adiciona automaticamente um AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // Iniciar inspeção
        if (emProximidade && Input.GetKeyDown(KeyCode.E) && !inspecionando)
        {
            ComecarInspecao();
        }

        // Durante inspeção
        if (inspecionando)
        {
            RotacionarObjeto();

            if (Input.GetKeyDown(KeyCode.Tab)) // tecla Tab para sair
            {
                SairInspecao();
            }
        }
    }

    void ComecarInspecao()
    {
        inspecionando = true;
        textoInteracao.SetActive(false);

        // Bloqueia o player
        if (playerMovement != null)
            playerMovement.enabled = false;

        posicaoOriginal = transform.position;
        rotacaoOriginal = transform.rotation;

        // Move o objeto para o ponto de inspeção
        transform.position = inspectionPoint.position;
        transform.rotation = inspectionPoint.rotation;

        // Toca som
        if (somInspecao != null && audioSource != null)
        {
            audioSource.PlayOneShot(somInspecao);
        }
    }


    void SairInspecao()
    {
        inspecionando = false;

        // Libera o player
        if (playerMovement != null)
            playerMovement.enabled = true;

        // Volta o objeto para posição original
        transform.position = posicaoOriginal;
        transform.rotation = rotacaoOriginal;
    }

    void RotacionarObjeto()
    {
        float rotX = 0f;
        float rotY = 0f;

        if (Input.GetKey(KeyCode.A)) rotY = -velocidadeRotacao * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) rotY = velocidadeRotacao * Time.deltaTime;
        if (Input.GetKey(KeyCode.W)) rotX = -velocidadeRotacao * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) rotX = velocidadeRotacao * Time.deltaTime;

        transform.Rotate(Vector3.right, rotX, Space.World);
        transform.Rotate(Vector3.up, rotY, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !inspecionando)
        {
            textoInteracao.SetActive(true);
            emProximidade = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textoInteracao.SetActive(false);
            emProximidade = false;
        }
    }


}
