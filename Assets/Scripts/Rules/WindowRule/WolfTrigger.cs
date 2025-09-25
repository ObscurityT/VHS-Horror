using System.Data.Common;
using AudioSystem;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class WolfTrigger : MonoBehaviour
{
    [Header("Áudio")]
    public string wolfSFXName = "LoboUivo";
    [Range(0f, 1f)] public float volume = 1f;

    [Header("Parâmetros")]
    public string playerTag = "Player";
    public string windowTag = "Window";
    public string closeParameter = "Close"; //aqui vem o nome do parametro que tem no animator do unity na animação de fechar a janela
    public bool useTriggerParameter = true;

    bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag(playerTag)) return;

        triggered = true;
        PlayWolf();
        // CloseAllWindows(); reativar depois, quando tiver a animação anexada
        Debug.Log("[WolfTrigger] Player entrou na área do lobo. Toca o som e fecha as janelas.");

    }

    void PlayWolf()
    {
        AudioManager.Instance.PlaySFX("LoboUivo");
    }

// reativar essa método depois, quando tiver a animação anexada
    // void CloseAllWindows()
    // {
    //     var windows = GameObject.FindGameObjectsWithTag(windowTag);
    //     foreach (var w in windows)
    //     {
    //         var animator = w.GetComponent<Animator>();
    //         if (useTriggerParameter)
    //         {
    //             animator.SetTrigger(closeParameter);
    //         }
    //         else
    //         {
    //             animator.SetBool(closeParameter, true);
    //         }
    //     }
    // }
}
