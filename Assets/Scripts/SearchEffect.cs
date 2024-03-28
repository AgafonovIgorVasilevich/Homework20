using UnityEngine;

public class SearchEffect : MonoBehaviour
{
    [SerializeField] private ResourceDetector _detector;
    [SerializeField] private ParticleSystem _effect;

    private void OnEnable() => _detector.SearchStarted += Show;

    private void OnDisable() => _detector.SearchStarted -= Show;

    private void Show(float radius)
    {
        var shape = _effect.shape;
        shape.radius = radius;
        _effect.Play();
    }
}