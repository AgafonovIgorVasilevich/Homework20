using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private Transform _stock;
    [SerializeField] private Transform _exit;
    [SerializeField] private Transform _entry;

    public Transform[] GetWaypoints(Transform target)
    {
        return new Transform[] {
            _exit,
            target,
            _entry,
            _stock
        };
    }
}