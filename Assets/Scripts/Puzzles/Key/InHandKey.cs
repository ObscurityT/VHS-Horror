using UnityEngine;

public class InHandKey : MonoBehaviour
{
    public KeyType currentKey = KeyType.None;

    public void SetKey(KeyType newKey)
    { 
        currentKey = newKey;
        Debug.Log("Key picked up: " + currentKey);
    }
    public KeyType GetKey()
    {
        return currentKey;
    }

    public void ClearKey()
    {
        currentKey = KeyType.None;
    }
}
