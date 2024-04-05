using UnityEngine;

public class PathLimits : MonoBehaviour
{
    [SerializeField] private float _minHeight = 10;
    [SerializeField] private float _maxHeight = 30;
    [SerializeField] private float _minScale = 0.1f;
    [SerializeField] private float _maxScale = 1;

    public float MinHeight => _minHeight;
    public float MaxHeight =>_maxHeight;
    public float MinScale => _minScale;
    public float MaxScale => _maxScale;
}