using UnityEngine;
using System;

public class LoaderSpawner : MonoBehaviour
{
    [SerializeField] private Upgrader _upgrader;
    [SerializeField] private Stock _stock;

    private int _freeCount => _maxCount - _currenCount;
    private int _currenCount;
    private int _maxCount;

    private LoaderPool _pool;

    public bool IsEmpty => _currenCount == _maxCount;

    public event Action<int, int> LoaderCountChanged;

    private void OnEnable()
    {
        _upgrader.LoadredBuyed += IncreaseLoaderCount;
        _stock.ResourceReceived += AddFreeLoader;
    }

    private void OnDisable()
    {
        _upgrader.LoadredBuyed -= IncreaseLoaderCount;
        _stock.ResourceReceived -= AddFreeLoader;
    }

    public void Initialize(LoaderPool loaderPool) => _pool = loaderPool;

    public Loader GetFreeLoader()
    {
        if (IsEmpty || _pool == null)
            return null;

        Loader loader = _pool.Get();
        loader.transform.position = transform.position;
        loader.transform.rotation = transform.rotation;
        _currenCount++;
        LoaderCountChanged?.Invoke(_freeCount, _maxCount);
        return loader;
    }

    public void IncreaseLoaderCount()
    {
        _maxCount++;
        LoaderCountChanged?.Invoke(_freeCount, _maxCount);
    }

    public void DecreaseLoaderCount()
    {
        _maxCount--;
        _currenCount--;
        LoaderCountChanged?.Invoke(_freeCount, _maxCount);
    }

    private void AddFreeLoader()
    {
        _currenCount--;
        LoaderCountChanged?.Invoke(_freeCount, _maxCount);
    }
}