using System.Collections;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]

public class LoaderMovement : MonoBehaviour
{
    [SerializeField] private ResourceCollector _collector;

    private WaitForFixedUpdate _physicsDelay = new WaitForFixedUpdate();
    private WaitForSeconds _startDelay = new WaitForSeconds(0.4f);
    private bool _isCompleteTake = false;
    private NavMeshAgent _agent;

    private void Awake() => _agent = GetComponent<NavMeshAgent>();

    private void OnEnable() => _collector.ResourceLoaded += CompleteTake;

    private void OnDisable() => _collector.ResourceLoaded -= CompleteTake;

    public void Initialize(Route route)
    {
        _isCompleteTake = false;
        StartCoroutine(Drive(route));
    }

    private IEnumerator DriveToPoint(Vector3 target)
    {
        _agent.SetDestination(target);

        while (_agent.remainingDistance > _agent.stoppingDistance)
            yield return _physicsDelay;
    }

    private IEnumerator DriveToTake(Vector3 target)
    {
        _agent.SetDestination(target);

        while (_isCompleteTake == false)
            yield return _physicsDelay;
    }

    private IEnumerator Drive(Route route)
    {
        yield return _startDelay;
        yield return StartCoroutine(DriveToPoint(route.Exit));
        yield return StartCoroutine(DriveToTake(route.Target));
        yield return StartCoroutine(DriveToPoint(route.Entry));
        yield return StartCoroutine(DriveToPoint(route.Stock));
    }

    private void CompleteTake() => _isCompleteTake = true;
}