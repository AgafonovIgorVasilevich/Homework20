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

    public void Initialize(Transform factory, Transform[] waypoints)
    {
        State = LoaderState.Empty;
        StartCoroutine(_movement.Drive(waypoints));
    }

    public int GetId() => _collector.GetInstanceID();

    public void BackToPool()
    {
        _collector.UnloadingResource();

        if (transform.parent.TryGetComponent(out LoaderPool pool))
            pool.Put(this);
        else
            Destroy(gameObject);
    }

    private void BecomeFull() => State = LoaderState.Full;
}