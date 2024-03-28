using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Route))]

public class Factory : MonoBehaviour
{
    [SerializeField] private float _timeBetweenSearch = 3;
    [SerializeField] private ResourceDetector _detector;
    [SerializeField] private LoaderSpawner _spawner;

    private Route _route;

    private void Awake()
    {
        _route = GetComponent<Route>();
        StartCoroutine(Search());
    }

    private IEnumerator Search()
    {
        WaitForSeconds _delay = new WaitForSeconds(_timeBetweenSearch);

        while (_detector != null)
        {
            yield return _delay;
            SendLoader();
        }
    }

    private void SendLoader()
    {
        Resource resource = _detector.FindBox();
        Loader loader = _spawner.GetFreeCar();
        Transform[] waypoints;

        if (loader != null && resource != null)
        {
            waypoints = _route.GetWaypoints(resource.transform);
            loader.Initialize(transform, waypoints);
            resource.Reserve(loader.GetId());
        }
    }
}