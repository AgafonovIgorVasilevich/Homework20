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

    private void OnDisable()
    {
        _collector.ResourceLoaded -= CompleteTake;
        _agent.enabled = false;
    }

    public void DriveToLoad(Route route) => StartCoroutine(Drive(route));

    public void DriveToMigrate(Vector3 target) => StartCoroutine(Drive(target));

    private IEnumerator DriveDirect(Vector3 target)
    {
        transform.LookAt(target);

        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target,
                _agent.speed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator DriveToPoint(Vector3 target)
    {
        _agent.enabled = true;
        _agent.SetDestination(target);

        while (_agent.pathPending)
            yield return _physicsDelay;

        while (_agent.remainingDistance > _agent.stoppingDistance)
            yield return _physicsDelay;

        _agent.enabled = false;
    }

    private IEnumerator DriveToTake(Vector3 target)
    {
        _agent.enabled = true;
        _agent.SetDestination(target);

        while (_agent.pathPending)
            yield return _physicsDelay;

        while (_isCompleteTake == false)
            yield return _physicsDelay;

        _agent.enabled = false;
    }

    private IEnumerator Drive(Route route)
    {
        _isCompleteTake = false;
        yield return _startDelay;
        yield return StartCoroutine(DriveDirect(route.Exit));
        yield return StartCoroutine(DriveToTake(route.Target));
        yield return StartCoroutine(DriveToPoint(route.Entry));
        yield return StartCoroutine(DriveDirect(route.Factory));
    }

    private IEnumerator Drive(Vector3 target)
    {
        yield return _startDelay;
        StartCoroutine(DriveToPoint(target));
    }

    private void CompleteTake() => _isCompleteTake = true;
}