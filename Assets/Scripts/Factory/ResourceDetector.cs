using UnityEngine;
using System;

public class ResourceDetector : MonoBehaviour
{
    [SerializeField] private float _radius = 30;
    [SerializeField] private LayerMask _mask;

    public event Action<float> SearchStarted;

    public Resource FindResource()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _mask);

        SearchStarted?.Invoke(_radius);

        foreach (Collider hit in hits)
        {
            if(hit.TryGetComponent(out Resource resource) && resource.IsMarked == false)
                return resource;
        }    

        return null;
    }
}