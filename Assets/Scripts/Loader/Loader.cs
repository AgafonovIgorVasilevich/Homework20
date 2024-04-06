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
        _movement.DriveToLoad(route);
        _collector.SetTargetId(idResource);
    }

    public void Initialize(Vector3 target)
    {
        State = LoaderState.Migratory;
        _movement.DriveToMigrate(target);
    }

    public bool TryUnload(out Resource resource) => _collector.TryUnload(out resource);

    private void BecomeFull() => State = LoaderState.Full;
}