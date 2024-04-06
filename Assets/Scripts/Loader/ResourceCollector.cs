using UnityEngine;
using System;

public class ResourceCollector : MonoBehaviour
{
    private Resource _resource;
    private int _resourceID;

    public event Action ResourceLoaded;

    private void OnTriggerEnter(Collider other)
    {
        if (_resource != null)
            return;

        if (other.TryGetComponent(out Resource resource) &&
            resource.GetInstanceID() == _resourceID)
            LoadResource(resource);
    }

    public void SetTargetId(int resourceID) => _resourceID = resourceID;

    public bool TryUnload(out Resource resource)
    {
        resource = _resource;

        if (_resource)
        {
            _resource = null;
            return true;
        }

        return false;
    }

    private void LoadResource(Resource resource)
    {
        _resourceID = int.MinValue;
        _resource = resource;
        _resource.transform.parent = transform;
        _resource.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        ResourceLoaded?.Invoke();
    }
}