using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public int ID => GetInstanceID();

    public string Name { get; set; }
    [field: SerializeField]

    public string Description { get; set; }
    [field: SerializeField]
}
