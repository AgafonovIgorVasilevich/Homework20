using UnityEngine;
using System;

public class ResourceCollector : MonoBehaviour
{
    private Resource _resource;

    public event Action ResourceLoaded;

    private void OnTriggerEnter(Collider other)
    {
        if (IsFull())
            return;

        if (other.TryGetComponent(out Resource resource))
            if (resource.IsReserved && resource.IdRecipient == GetInstanceID())
                LoadingResource(resource);
    }

    public bool IsFull() => _resource != null;

    public void UnloadingResource()
    {
        if (IsFull() == false)
            return;

        _resource.BackToPool();
        _resource = null;
    }

    private void LoadingResource(Resource resource)
    {
        _resource = resource;
        _resource.transform.parent = transform;
        _resource.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        ResourceLoaded?.Invoke();
    }
}