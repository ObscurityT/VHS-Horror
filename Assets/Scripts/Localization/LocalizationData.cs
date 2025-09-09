using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationData", menuName = "Localization/LocalizationData")]
public class LocalizationData : ScriptableObject
{
    public SystemLanguage language;
    public List<LocalizationEntry> entries;
}

[System.Serializable]
public class LocalizationEntry
{
    public string key;
    [TextArea] public string value;
}