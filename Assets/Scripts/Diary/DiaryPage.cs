using UnityEngine;

public class DiaryPage : MonoBehaviour
{
    public int pageNumber;
    public string pageText;

    public void Collect()
    {
        DiaryManager.Instance.AddLegendPage(pageNumber, pageText);
        Destroy(gameObject);
    }
}
