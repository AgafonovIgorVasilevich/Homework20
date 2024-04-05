using UnityEngine;

[RequireComponent (typeof(Animator))]

public class TextAnimator : MonoBehaviour
{
    private const string Flash = nameof(Flash);

    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void Hihglight() => _animator.SetTrigger(Flash);
}