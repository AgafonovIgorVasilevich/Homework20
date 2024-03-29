using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]

public class Stock : MonoBehaviour
{
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private LoaderPool _loaderPool;

    public event Action ResourceReceived;

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