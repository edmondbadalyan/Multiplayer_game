using UnityEngine;

public class Skins : MonoBehaviour
{
    [SerializeField] private Material[] _material;

    public int Length => _material.Length;

    public Material GetMaterial(int index)
    {
        if(_material.Length <= index) return _material[0];
        return _material[index];
    }
}
