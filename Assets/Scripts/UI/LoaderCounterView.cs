using UnityEngine;
using TMPro;

public class LoaderCounterView : MonoBehaviour
{
    [SerializeField] private TextAnimator _animator;
    [SerializeField] private TMP_Text _text;

    private LoaderSpawner _spawner;

    private void OnDisable() => _spawner.LoaderCountChanged -= Show;

    public void Initialize(LoaderSpawner spawner)
    {
        _spawner = spawner;
        _spawner.LoaderCountChanged += Show;
    }

    private void Show(int current, int max)
    {
        _text.text = $"{current}/{max}";
        _animator.Hihglight();
    }
}