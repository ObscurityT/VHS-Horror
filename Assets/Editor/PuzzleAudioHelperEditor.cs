using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(PuzzleAudioHelper))]
public class PuzzleAudioHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PuzzleAudioHelper helper = (PuzzleAudioHelper)target;

        List<string> options = new List<string>();

        
        AudioSystem.AudioManager manager = AudioSystem.AudioManager.Instance;
        if (manager == null)
            manager = GameObject.FindFirstObjectByType<AudioSystem.AudioManager>();

        if (manager != null && manager.sfxClips != null)
        {
            options.Add("[Nenhum]");

            foreach (var clip in manager.sfxClips)
            {
                if (clip != null && !string.IsNullOrEmpty(clip.name))
                    options.Add(clip.name);
            }
        }

        //Se não achar nada, avise
        if (options.Count == 0)
        {
            EditorGUILayout.HelpBox("Sem efeitos sonoros carregados do AudioManager. Rode a cena ou verifique a hierarquia.", MessageType.Warning);
            DrawDefaultInspector();
            return;
        }

        //Dropdowns dos sons
        EditorGUILayout.LabelField("Sons do Puzzle", EditorStyles.boldLabel);

        helper.soundOnOpen = DrawDropdown("Sound On Open", helper.soundOnOpen, options);
        helper.soundOnClose = DrawDropdown("Sound On Close", helper.soundOnClose, options);
        helper.soundOnSuccess = DrawDropdown("Sound On Success", helper.soundOnSuccess, options);
        helper.soundOnFail = DrawDropdown("Sound On Fail", helper.soundOnFail, options);
        

        EditorGUILayout.Space();
        helper.randomPitch = EditorGUILayout.Toggle("Random Pitch", helper.randomPitch);

        if (GUI.changed)
            EditorUtility.SetDirty(helper);
    }

    private string DrawDropdown(string label, string current, List<string> options)
    {
        int index = options.IndexOf(current);
        if (index < 0) index = 0;
        index = EditorGUILayout.Popup(label, index, options.ToArray());
        return options[index];
    }
}