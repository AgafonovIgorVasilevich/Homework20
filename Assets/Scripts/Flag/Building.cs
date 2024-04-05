using Unity.AI.Navigation;
using UnityEngine;
using System;

public class Building : MonoBehaviour
{
    [SerializeField] private Factory _template;

    private ResourcePool _resourcePool;
    private LoaderPool _loaderPool;
    private RectTransform _statsTable;
    private NavMeshSurface _surface;

    public event Action BuildCompleted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Loader loader) &&
            loader.State == LoaderState.Migratory)
        {
            _loaderPool.Put(loader);
            Build();
        }
    }

    public void Initialize(NavMeshSurface surface, ResourcePool resourcePool,
        LoaderPool loaderPool, RectTransform statsTable)
    {
        _surface = surface;
        _resourcePool = resourcePool;
        _loaderPool = loaderPool;
        _statsTable = statsTable;
    }

    private void Build()
    {
        Factory factory = Instantiate(_template, transform);
        factory.Initialize(_resourcePool, _loaderPool, _statsTable);
        factory.IncreaseLoaderCount();
        factory.transform.parent = null;

        _surface.BuildNavMesh();
        BuildCompleted?.Invoke();
        Destroy(gameObject);
    }
}