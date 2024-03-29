using UnityEngine;

public class SearchEffect : MonoBehaviour
{
    [SerializeField] private ResourceDetector _detector;
    [SerializeField] private ParticleSystem _effect;

    private ParticleSystem.ShapeModule _shape;

    private void Awake() => _shape = _effect.shape;

    private void OnEnable() => _detector.SearchStarted += Show;

    private void OnDisable() => _detector.SearchStarted -= Show;

    private void Show(float radius)
    {
        _shape.radius = radius;
        _effect.Play();
    }
}