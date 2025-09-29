using AudioSystem;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerStatus))]
public class PlayerStatusEditor : Editor
{
    private AudioManager audioManager;

    void OnEnable()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerStatus ps = (PlayerStatus)target;

        if (audioManager != null)
        {
            var audioNames = audioManager.GetSFXNames();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Sons do Sistema de Sanidade", EditorStyles.boldLabel);

            ps.sfxRespiracao = DrawSfxDropdown("Respiração Ofegante", ps.sfxRespiracao, audioNames);
            ps.sfxMurmurios = DrawSfxDropdown("Murmúrios", ps.sfxMurmurios, audioNames);
            ps.sfxGrito = DrawSfxDropdown("Grito Fundo", ps.sfxGrito, audioNames);
            ps.sfxVozInterna = DrawSfxDropdown("Voz Interna", ps.sfxVozInterna, audioNames);
            ps.sfxGameOver = DrawSfxDropdown("Game Over", ps.sfxGameOver, audioNames);
        }
        else
        {
            EditorGUILayout.HelpBox("AudioManager nao foi encontrado", MessageType.Warning);
        }
    }

    // Função utilitária para gerar dropdowns de SFX
    private string DrawSfxDropdown(string label, string current, List<string> audioNames)
    {
        int currentIndex = Mathf.Max(0, audioNames.IndexOf(current));
        int newIndex = EditorGUILayout.Popup(label, currentIndex, audioNames.ToArray());
        return audioNames.Count > 0 ? audioNames[newIndex] : "";
    }
}