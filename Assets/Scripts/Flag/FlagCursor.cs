using Unity.AI.Navigation;
using UnityEngine;

public class FlagCursor : MonoBehaviour
{
    [SerializeField] private Transform[] _checkerPoints;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Material _correctMaterial;
    [SerializeField] private Material _errorMaterial;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _mask;

    private Ray _groundChecker;
    private int _entries;

    private void Update() => Repaint();

    public bool IsCorrect()
    {
        foreach (Transform checker in _checkerPoints)
            if (IsGrounded(checker) == false)
                return false;

        return IsFreePlace();
    }

    private void Repaint()
    {
        if (IsCorrect())
            _renderer.material = _correctMaterial;
        else
            _renderer.material = _errorMaterial;
    }

    private bool IsFreePlace()
    {
        _entries = Physics.OverlapSphere(transform.position, _radius, _mask).Length;
        return _entries == 0;
    }

    private bool IsGrounded(Transform checkerPoint)
    {
        _groundChecker = new Ray(checkerPoint.position, Vector3.down);

        if (Physics.Raycast(_groundChecker, out RaycastHit hit))
            return hit.transform.GetComponent<NavMeshSurface>();

        return false;
    }
}