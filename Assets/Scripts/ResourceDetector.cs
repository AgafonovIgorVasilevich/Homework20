using UnityEngine;
using System;

public class ResourceDetector : MonoBehaviour
{
    [SerializeField] private float _radius = 50;

    public event Action<float> SearchStarted;

    public Resource FindBox()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius);

        SearchStarted?.Invoke(_radius);

        foreach (Collider hit in hits)
        {
            if(hit.TryGetComponent(out Resource resource) && resource.IsReserved == false)
                return resource;
        }    

        return null;
    }
}