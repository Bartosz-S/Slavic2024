using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    private Material _defaultMaterial;
    private MeshRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _defaultMaterial = _renderer.material;
    }
    public void ChangeColor(bool isHighlighted)
    {
        if (!isHighlighted)
        {
            _renderer.material = _defaultMaterial;
        }
        else
        {
            _renderer.material = highlightMaterial;
        }
    }
}
