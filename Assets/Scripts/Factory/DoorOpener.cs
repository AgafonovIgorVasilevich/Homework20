using UnityEngine;

[RequireComponent(typeof(Animator))]

public class DoorOpener : MonoBehaviour
{
    private const string Open = nameof(Open);

    private Animator _animator;

    private int _countLoaders = 0;

    private void Awake() => _animator = GetComponent<Animator>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Loader loader))
        {
            _countLoaders++;
            _animator.SetBool(Open, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Loader loader))
        {
            _countLoaders--;

            if (_countLoaders == 0)
                _animator.SetBool(Open, false);
        }
    }
}