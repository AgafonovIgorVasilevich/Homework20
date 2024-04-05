using System.Collections;
using UnityEngine;
using System;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRadius = 25;
    [SerializeField] private float _spawnHeight = 0;
    [SerializeField] private ResourcePool _pool;
    [SerializeField] private int _maxCount = 5;

    private WaitForSeconds _delay = new WaitForSeconds(1);
    private int _currentCount = 0;

    public event Action<Resource> ResorceSpawned;

    private void OnEnable() => _pool.InstancePuted += AddFreePlace;

    private void OnDisable() => _pool.InstancePuted -= AddFreePlace;

    private void Start() => StartCoroutine(Spawn());

    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        float radiusFreeSpace = 1;
        int limitCollisions = 1;
        bool inProgress = true;
        int countTries = 0;
        int maxTries = 100;

        while (inProgress && countTries < maxTries)
        {
            spawnPosition = UnityEngine.Random.insideUnitSphere * _spawnRadius;
            spawnPosition.y = _spawnHeight;
            countTries++;

            if (Physics.OverlapSphere(spawnPosition, radiusFreeSpace).Length <= limitCollisions)
                inProgress = false;
        }

        return spawnPosition;
    }

    private void AddFreePlace() => _currentCount--;

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            if (_currentCount < _maxCount)
            {
                Resource resource = _pool.Get();
                resource.Initialize(GetSpawnPosition());
                ResorceSpawned?.Invoke(resource);
                _currentCount++;
                yield return _delay;
            }

            yield return null;
        }
    }
}
