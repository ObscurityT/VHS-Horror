using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneEndTimer : MonoBehaviour
{
    public PlayableDirector cutscene;
    public string nextSceneName;

    void Start()
    {
        if (cutscene != null)
            cutscene.stopped += OnCutsceneEnd;
    }

    void OnCutsceneEnd(PlayableDirector director)
    {
        if (director == cutscene)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

