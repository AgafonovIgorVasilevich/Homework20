using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RouteCreator))]

public class Factory : MonoBehaviour
{
    private static int s_number = 1;

    [SerializeField] private ResourceDetector _detector;
    [SerializeField] private FactoryNameView _nameView;
    [SerializeField] private LoaderSpawner _spawner;
    [SerializeField] private ResourceScore _score;
    [SerializeField] private FactoryStats _stats;
    [SerializeField] private Upgrader _upgrader;
    [SerializeField] private Stock _stok;

    [SerializeField] private float _searchTime = 3;

    private RouteCreator _routeCreator;
    private string _name;

    private void Awake()
    {
        _routeCreator = GetComponent<RouteCreator>();
        StartCoroutine(Search());
        SetName();
    }

    private void OnEnable() => _upgrader.FactoryBuyed += BuildNewFactory;

    private void OnDisable() => _upgrader.FactoryBuyed -= BuildNewFactory;

    public void Initialize(ResourcePool resourcePool, LoaderPool loaderPool, FactoryList statsTable)
    {
        _stok.Initialize(resourcePool, loaderPool);
        _spawner.Initialize(loaderPool);
        _stats = Instantiate(_stats);
        _stats.Initialize(_score, _spawner, _name);
        statsTable.AddItem(_stats);
    }

    public void IncreaseLoaderCount() => _spawner.IncreaseLoaderCount();

    private void SetName()
    {
        _name = $"База {s_number}";
        _nameView.Show(_name);
        s_number++;
    }

    private void SendLoaderToLoad()
    {
        if(_detector.TryFindResource(out Resource resource) == false)
            return;

        Route route = _routeCreator.CreateRoute(resource.transform);

        if (_spawner.TryGetLoader(out Loader loader))
            loader.Initialize(route, resource.GetInstanceID());
    }

    private void BuildNewFactory()
    {
        _stats.HideFlagIndicator();
        StartCoroutine(Build());
    }

    private IEnumerator Search()
    {
        WaitForSeconds delay = new WaitForSeconds(_searchTime);

        while (_detector != null)
        {
            yield return delay;

            if (_spawner.IsEmpty == false)
                SendLoaderToLoad();
        }
    }

    private IEnumerator Build()
    {
        Loader loader;

        while (_spawner.TryGetLoader(out loader) == false)
            yield return null;

        _spawner.DecreaseLoaderCount();
        loader.Initialize(_upgrader.BuildingPosition);
    }
}