using UnityEngine;
using System;

public class LoaderSpawner : MonoBehaviour
{
    [SerializeField] private int _maxLoadersCount;
    [SerializeField] private LoaderPool _pool;

    private int _freeLoadersCount;

    public event Action<int, int> CarCountChanged;

    private void Awake() => _freeLoadersCount = _maxLoadersCount;

    private void OnEnable() => _pool.InstancePuted += AddFreeLoader;

    private void OnDisable() => _pool.InstancePuted -= AddFreeLoader;

    private void Start() => CarCountChanged?.Invoke(_freeLoadersCount, _maxLoadersCount);

    public Loader GetFreeCar()
    {
        if (_freeLoadersCount <= 0)
            return null;

        Loader loader = _pool.Get();
        loader.transform.position = transform.position;
        loader.transform.rotation = transform.rotation;
        _freeLoadersCount--;
        CarCountChanged?.Invoke(_freeLoadersCount, _maxLoadersCount);
        return loader;
    }

    private void AddFreeLoader()
    {
        _freeLoadersCount++;
        CarCountChanged?.Invoke(_freeLoadersCount, _maxLoadersCount);
    }
}