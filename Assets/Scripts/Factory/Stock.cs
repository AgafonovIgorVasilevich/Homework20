using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]

public class Stock : MonoBehaviour
{
    private ResourcePool _resourcePool;
    private LoaderPool _loaderPool;

    public event Action ResourceReceived;

    public void Initialize(ResourcePool resourcePool, LoaderPool loaderPool)
    {
        _resourcePool = resourcePool;
        _loaderPool = loaderPool;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Loader loader) && loader.State == LoaderState.Full)
        {
            _resourcePool.Put(loader.Unload());
            _loaderPool.Put(loader);
            ResourceReceived?.Invoke();
        }
    }
}