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

    private bool _isReadyToBuild = true;
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

    public void Initialize(ResourcePool resourcePool, LoaderPool loaderPool, RectTransform statsTable)
    {
        _stok.Initialize(resourcePool, loaderPool);
        _spawner.Initialize(loaderPool);
        _stats = Instantiate(_stats, statsTable);
        _stats.Initialize(_score, _spawner, _name);
    }

    public void IncreaseLoaderCount() => _spawner.IncreaseLoaderCount();

    public Upgrader TryGetUpgrader()
    {
        if (_isReadyToBuild)
            return _upgrader;
        else
            return null;
    }

    private void SetName()
    {
        _name = $"База {s_number}";
        _nameView.Show(_name);
        s_number++;
    }

    private void SendLoaderToLoad()
    {
        Resource resource = _detector.FindResource();

        if (resource == null)
            return;

        resource.Mark();

        Route route = _routeCreator.CreateRoute(resource.transform);
        Loader loader = _spawner.GetFreeLoader();

        loader.Initialize(route, resource.GetInstanceID());
    }

    private void BuildNewFactory()
    {
        _stats.HideFlagIndicator();
        _isReadyToBuild = false;
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
        Loader loader = null;

        while (loader == null)
        {
            loader = _spawner.GetFreeLoader();
            yield return null;
        }

        _spawner.DecreaseLoaderCount();
        loader.Initialize(_upgrader.BuildingPosition);
    }
}