using System.Collections;
using UnityEngine;

public class ResourceDestroyer : MonoBehaviour
{
    [SerializeField] private ResourceSpawner _spawner;
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private float _lifeTime = 10;

    private WaitForSeconds _destroyDelay;

    private void Awake() => _destroyDelay = new WaitForSeconds(_lifeTime);

    private void OnEnable() => _spawner.ResorceSpawned += GrowOld;

    private void OnDisable() => _spawner.ResorceSpawned -= GrowOld;

    private void GrowOld(Resource resource) => StartCoroutine(DelayDestroy(resource));

    private IEnumerator DelayDestroy(Resource resource)
    {
        yield return _destroyDelay;

        if(resource.IsMarked == false)
            _pool.Put(resource);
    }
}