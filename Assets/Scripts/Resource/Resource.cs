using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Resource : MonoBehaviour
{
    [SerializeField] private ParticleSystem _selectEffect;

    public bool IsMarked => _selectEffect.isPlaying;

    public void Initialize(Vector3 position)
    {
        _selectEffect.Stop();
        transform.position = position;
        transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(0, 360));
    }

    public void Mark() => _selectEffect.Play();
}