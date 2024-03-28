using UnityEngine;

public class FastTester : MonoBehaviour
{
    [SerializeField][Range(0.1f, 10)]private float speed = 1;

    private void OnValidate() => Time.timeScale = speed;
}
