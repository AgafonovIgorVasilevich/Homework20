using UnityEngine.AI;
using UnityEngine;
using System;

public class ResourceDetector : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private float _radius = 30;
    [SerializeField] private LayerMask _mask;

    public event Action<float> SearchStarted;

    public Resource FindResource()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _mask);

        SearchStarted?.Invoke(_radius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Resource resource) && resource.IsMarked == false)
                if (IsReachableResouce(resource))
                    return resource;
        }    

        return null;
    }

    private bool IsReachableResouce(Resource resource)
    {
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(_startPoint.position, resource.transform.position,
            NavMesh.AllAreas, path);

        return path.status == NavMeshPathStatus.PathComplete;
    }
}