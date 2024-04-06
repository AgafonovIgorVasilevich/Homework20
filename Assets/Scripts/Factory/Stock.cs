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
            if(loader.TryUnload(out Resource resource))
            {
                _resourcePool.Put(resource);
                ResourceReceived?.Invoke();
            }

            _loaderPool.Put(loader);
        }
    }
}