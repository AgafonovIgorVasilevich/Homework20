using System.Collections;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]

public class LoaderMovement : MonoBehaviour
{
    [SerializeField] private float _distanceFactor = 0.1f;
    [SerializeField] private float _timeToShift = 0.3f;
    private NavMeshAgent _agent;

    private void Awake() => _agent = GetComponent<NavMeshAgent>();

    public IEnumerator Drive(Transform[] wayponts)
    {
        WaitForSeconds delay = new WaitForSeconds(_timeToShift);
        int index = 0;

        yield return delay;

        while (index < wayponts.Length)
        {
            _agent.SetDestination(wayponts[index].position);
            yield return delay;

            if (_agent.remainingDistance <= _distanceFactor)
                index++;
        }
    }
}