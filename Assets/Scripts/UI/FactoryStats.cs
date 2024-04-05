using UnityEngine;

public class FactoryStats : MonoBehaviour
{
    [SerializeField] private ResourceScoreView _resourceView;
    [SerializeField] private LoaderCounterView _loaderView;
    [SerializeField] private RectTransform _flagIndicator;
    [SerializeField] private FactoryNameView _nameView;

    public void Initialize(ResourceScore score, LoaderSpawner spawner, string name)
    {
        _resourceView.Initialize(score);
        _loaderView.Initialize(spawner);
        _nameView.Show(name);
    }

    public void HideFlagIndicator() => _flagIndicator.gameObject.SetActive(false);
}