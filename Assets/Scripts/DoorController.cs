using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class DoorController : MonoBehaviour
{
    private const string Open = nameof(Open);

    [SerializeField] private Animator _animator;

    private List<Loader> _loaders = new List<Loader>();

    private void Awake() => _animator = GetComponent<Animator>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Loader loader))
        {
            _loaders.Add(loader);
            _animator.SetBool(Open, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Loader loader))
        {
            _loaders.Remove(loader);

            if (_loaders.Count == 0)
                _animator.SetBool(Open, false);
        }
    }
}