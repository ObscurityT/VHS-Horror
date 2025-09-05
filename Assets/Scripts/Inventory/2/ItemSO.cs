using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public int ID => GetInstanceID();

    [field: SerializeField]
    public string NameKey { get; set; }

    [field: SerializeField]
    public string DescriptionKey { get; set; }

    [field: SerializeField]
    public Sprite ItemImage { get; set; }
}
