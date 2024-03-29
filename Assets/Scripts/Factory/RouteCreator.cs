using UnityEngine;

public class RouteCreator : MonoBehaviour
{
    [SerializeField] private Transform _stock;
    [SerializeField] private Transform _entry;
    [SerializeField] private Transform _exit;

    public Route CreateRoute(Transform target)
    {
        return new Route(target.position, _stock.position, _entry.position, _exit.position);
    }
}