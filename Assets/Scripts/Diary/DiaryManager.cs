using System.Collections.Generic;
using UnityEngine;

public class DiaryManager : MonoBehaviour
{
    public static DiaryManager Instance;

    private SortedDictionary<int, string> legendPages = new SortedDictionary<int, string>();
    private SortedDictionary<int, string> rulesPages = new SortedDictionary<int, string>();

    private void Start()
    {
        // Adiciona a única página de regras ao iniciar
        rulesPages.Add(0, "-Nunca olhe diretamente para o espelho.\r\n-Não saia pela mesma porta que entrou.\r\n-Não fique mais do que 5 segundos dentro de um comôdo com um espelho quebrado.-\r\n-Feche todas as janelas se ouvir um uivo.\r\n-Se ouvir uma melodia não se mova!");
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddLegendPage(int pageNumber, string text)
    {
        if (!legendPages.ContainsKey(pageNumber))
            legendPages.Add(pageNumber, text);
    }

    public List<string> GetLegendPages()
    {
        return new List<string>(legendPages.Values);
    }

    public List<string> GetRulesPages()
    {
        return new List<string>(rulesPages.Values);
    }
}
