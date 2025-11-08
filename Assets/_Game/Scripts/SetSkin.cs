using UnityEngine;

public class SetSkin : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _m_Renderers;
    public void Set(Material material)
    {
        for (int i = 0; i < _m_Renderers.Length; i++) {

            _m_Renderers[i].material = material;
        }
    }
}
