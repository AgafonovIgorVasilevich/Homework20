using UnityEngine;

[RequireComponent (typeof(Factory))]

public class MainFactory : MonoBehaviour
{
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private LoaderPool _loaderPool;
    [SerializeField] private RectTransform _statsTable;
    [SerializeField] private int _loadersCount = 3;

    private Factory _factory;

    private void Start()
    {
        _factory = GetComponent<Factory>();
        _factory.Initialize(_resourcePool, _loaderPool, _statsTable);
        
        for(int i = 0; i < _loadersCount; i++)
            _factory.IncreaseLoaderCount();
    }
}