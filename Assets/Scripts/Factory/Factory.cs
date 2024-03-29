using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RouteCreator))]

public class Factory : MonoBehaviour
{
    [SerializeField] private float _timeBetweenSearch = 3;
    [SerializeField] private ResourceDetector _detector;
    [SerializeField] private LoaderSpawner _spawner;

    private RouteCreator _route;

    private void Awake()
    {
        _route = GetComponent<RouteCreator>();
        StartCoroutine(Search());
    }

    private IEnumerator Search()
    {
        WaitForSeconds delay = new WaitForSeconds(_timeBetweenSearch);

        while (_detector != null)
        {
            if (_spawner.IsEmpty == false)
                SendLoader();

            yield return delay;
        }
    }

    private void SendLoader()
    {
        Resource resource = _detector.FindResource();

        if (resource == null)
            return;

        resource.Mark();

        Route route = _route.CreateRoute(resource.transform);
        Loader loader = _spawner.GetFreeLoader();

        loader.Initialize(route, resource.GetInstanceID());
    }
}