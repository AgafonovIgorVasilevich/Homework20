using UnityEngine;

[RequireComponent(typeof(LoaderMovement))]

public class Loader : MonoBehaviour
{
    [SerializeField] private ResourceCollector _collector;

    private LoaderMovement _movement;

    public LoaderState State { get; private set; } = LoaderState.Empty;

    private void Awake() => _movement = GetComponent<LoaderMovement>();

    private void OnEnable() => _collector.ResourceLoaded += BecomeFull;

    private void OnDisable() => _collector.ResourceLoaded -= BecomeFull;

    public void Initialize(Route route, int idResource)
    {
        State = LoaderState.Empty;
        _movement.Initialize(route);
        _collector.SetTargetId(idResource);
    }

    public Resource Unload() => _collector.UnloadResource();

    private void BecomeFull() => State = LoaderState.Full;
}