using SplineMesh;
using UnityEngine;
using System;

[RequireComponent(typeof(Spline))]

public class CameraMover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string Height = nameof(Height);

    [SerializeField] private PathLimits _limits;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _flySpeed;
    [SerializeField] private float _sensetive;

    private CurveSample _sample;
    private Spline _path;

    private float _splineRate;
    private float _scale = 1;
    private float _height;

    private Quaternion _rotation;
    private Vector3 _direction;
    private Vector3 _input;

    private void Start()
    {
        _path = GetComponent<Spline>();

        ChangePosition();
        Rotate();

        _height = transform.position.y;
    }

    private void Update()
    {
        Rotate();

        if (IsDetectedInput())
        {
            ChangeScale();
            ChangeHeight();
            ChangePosition();
        }
    }

    private bool IsDetectedInput()
    {
        _input.x = Input.GetAxis(Horizontal);
        _input.y = Input.GetAxis(Height);
        _input.z = -Input.GetAxis(Vertical);

        return _input != Vector3.zero;
    }

    private void Rotate()
    {
        _direction = Vector3.zero - _camera.position;
        _rotation = Quaternion.LookRotation(_direction);
        _rotation = Quaternion.Lerp(_camera.rotation, _rotation, _sensetive * Time.unscaledDeltaTime);
        _camera.rotation = _rotation;
    }

    private void ChangeScale()
    {
        _scale += _input.z * _zoomSpeed * Time.unscaledDeltaTime;

        if (_limits != null)
            _scale = Math.Clamp(_scale, _limits.MinScale, _limits.MaxScale);

        transform.localScale = Vector3.one * _scale;
    }

    private void ChangeHeight()
    {
        _height += _input.y * _flySpeed * Time.unscaledDeltaTime;

        if (_limits != null)
            _height = Math.Clamp(_height, _limits.MinHeight, _limits.MaxHeight);

        transform.position = new Vector3(transform.position.x, _height, transform.position.z);
    }

    private void ChangePosition()
    {
        _splineRate += _moveSpeed * Time.unscaledDeltaTime * _input.x;

        if (_splineRate < 0)
            _splineRate = _path.nodes.Count - 1;

        if (_splineRate > _path.nodes.Count - 1)
            _splineRate = 0;

        _sample = _path.GetSample(_splineRate);
        _camera.localPosition = _sample.location;
    }
}