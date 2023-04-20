using UnityEngine;

/// <summary> Used instead of string literal </summary>
[CreateAssetMenu(fileName = "NewKey", menuName = "Key")]
public class Key : ScriptableObject
{
    [SerializeField] private string _name;

    public string Name => string.IsNullOrEmpty(_name) ? name : _name;
}
