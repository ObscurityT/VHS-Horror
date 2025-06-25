using UnityEngine;

public class MelodyController : MonoBehaviour
{

    public AudioSource melodia;
    public float duracaoMelodia = 5f;

    private bool tocando = false;

    public void TocarMelodia()
    {
        if (melodia == null)
        {
            Debug.LogError("ERRO: Campo 'melodia' está NULO!");
            return;
        }
        Debug.Log("playing");
       
        tocando = true;
        melodia.Play();
        GameEvents.OnMelodyStart?.Invoke();

        Invoke(nameof(PararMelodia), melodia.clip.length);
    }

    private void PararMelodia()
    {
        tocando = false;
        melodia.Stop();
        GameEvents.OnMelodyEnd?.Invoke();
    }
}
    
