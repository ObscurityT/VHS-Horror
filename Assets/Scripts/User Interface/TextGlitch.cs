using TMPro;
using UnityEngine;

public class TextGlitch : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public float intervalo = 0.1f;

    private void Start()
    {
        InvokeRepeating(nameof(Piscar), intervalo, intervalo);
    }

    void Piscar()
    {
        texto.enabled = !texto.enabled;
    }
    
}
