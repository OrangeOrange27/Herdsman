using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AnimalController : SpawnableEntity, IDisposable, IFollowTarget
{
    [SerializeField] private float _speed = 3;

    public bool IsInYard
    {
        get => _isInYard || _collider.IsTouchingLayers(LayerMask.NameToLayer("Yard"));

        set => _isInYard = true;
    }

    private Rigidbody2D _rb;
    private Collider2D _collider;
    private Transform _followTarget;
    private bool _isFollowingPlayer;
    private bool _isInYard;

    public void FollowTarget()
    {
        if(_followTarget==null)
            return;
        
        var direction = _followTarget.position - transform.position;
        _rb.velocity = direction.normalized * _speed;
    }

    public void SetFollowTarget(Transform target)
    {
        _followTarget = target;
    }

    public void FollowPlayer(Transform target)
    {
        if(_isInYard)
            return;
        
        SetFollowTarget(target);
        _isFollowingPlayer = true;
    }

    public void MoveToYard(Transform yard)
    {
        SetFollowTarget(yard);
        _isInYard = true;
        _isFollowingPlayer = false;
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        FollowTarget();
    }

    public void Dispose()
    {
    }
}
