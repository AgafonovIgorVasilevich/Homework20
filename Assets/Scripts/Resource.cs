using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Resource : MonoBehaviour
{
    [SerializeField] GameObject _selectEffect;

    private ResourcePool _pool;

    public int IdRecipient { get; private set; }
    public bool IsReserved { get; private set; }

    public void Initialize(Vector3 position, ResourcePool pool)
    {
        transform.position = position;
        transform.LookAt(Vector3.zero);
        _selectEffect.SetActive(false);
        IsReserved = false;
        _pool = pool;
    }

    public void Reserve(int idLoader)
    {
        _selectEffect.SetActive(true);
        IdRecipient = idLoader;
        IsReserved = true;
    }

    public void BackToPool()
    {
        if (_pool != null)
            _pool.Put(this);
        else
            Destroy(gameObject);
    }
}