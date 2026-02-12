using System.Collections.Generic; // Required for Lists
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<string> _heldKeys = new List<string>();

    public void RegisterKey(string id)
    {
        _heldKeys.Add(id);
        Debug.Log("Picked up key: " + id);
    }

    public bool HasKey(string id)
    {
        return _heldKeys.Contains(id);
    }

    public void UseKey(string id)
    {
        if (_heldKeys.Contains(id))
        {
            _heldKeys.Remove(id);
        }
    }
}