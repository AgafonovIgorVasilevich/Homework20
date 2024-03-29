using UnityEngine;
using System;

public class LoaderSpawner : MonoBehaviour
{
    [SerializeField] private int _maxCount;
    [SerializeField] private LoaderPool _pool;

    private int _freeCount;

    public bool IsEmpty => _freeCount == 0;

    public event Action<int, int> LoaderCountChanged;

    private void Awake() => _freeCount = _maxCount;

    private void OnEnable() => _pool.InstancePuted += AddFreeLoader;

    private void OnDisable() => _pool.InstancePuted -= AddFreeLoader;

    private void Start() => LoaderCountChanged?.Invoke(_freeCount, _maxCount);

    public Loader GetFreeLoader()
    {
        if(IsEmpty)
            return null;

        Loader loader = _pool.Get();
        loader.transform.position = transform.position;
        loader.transform.rotation = transform.rotation;
        _freeCount--;
        LoaderCountChanged?.Invoke(_freeCount, _maxCount);
        return loader;
    }

    private void AddFreeLoader()
    {
        _freeCount++;
        LoaderCountChanged?.Invoke(_freeCount, _maxCount);
    }
}