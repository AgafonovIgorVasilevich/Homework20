using SplineMesh;
using UnityEngine;
using System;
using Unity.VisualScripting;

[RequireComponent(typeof(Spline))]

public class CameraMover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string Height = nameof(Height);

    [SerializeField] private MouseHandler _mouse;
    [SerializeField] private PathLimits _limits;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _flySpeed;
    [SerializeField] private float _sensetive;

    private CurveSample _sample;
    private Transform _target;
    private Spline _path;

    private float _splineRate;
    private float _scale = 1;
    private float _height;

    private Vector3 _targetPosition = Vector3.zero;
    private Vector3 _direction;
    private Vector3 _input;
    private Quaternion _rotation;

    private void OnEnable() => _mouse.Focused += SetTarget;

    private void OnDisable() => _mouse.Focused -= SetTarget;

    private void Start()
    {
        _path = GetComponent<Spline>();
        _height = transform.position.y;

        ChangePosition();
        Rotate();
    }

    private void Update()
    {
        MoveOrigin();
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
        if (_target == null)
            _direction = Vector3.zero - _camera.position;
        else
            _direction = _target.position - _camera.position;

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

    private void MoveOrigin()
    {
        if (_target)
            _targetPosition = _target.position;
        else
            _targetPosition = Vector3.zero;

        _targetPosition.y = _height;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition,
            _sensetive * Time.unscaledDeltaTime);
    }

    private void SetTarget(Transform target) => _target = target;
}