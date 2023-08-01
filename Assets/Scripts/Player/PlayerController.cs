using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : SpawnableEntity, IFollowTarget
{
    private const int MAX_GROUP_COUNT = 5;
    
    [SerializeField] private float _animalCollectRadius;
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;
    
    private Vector2 _target;
    private List<AnimalController> _trackedAnimals;
    private Collider2D[] _animalsArray = new Collider2D[15];

    public List<AnimalController> TrackedAnimals => _trackedAnimals;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _trackedAnimals = new List<AnimalController>();
    }

    private void Update()
    {
        FollowTarget();
        LookForAnimals();
    }
    
    private void LookForAnimals()
    {
        Physics2D.OverlapCircleNonAlloc(transform.position, _animalCollectRadius, _animalsArray);
        
        foreach (var animal in _animalsArray)
        {
            if(animal==null || _trackedAnimals.Count>=MAX_GROUP_COUNT) continue;
            
            if(animal.TryGetComponent<AnimalController>(out var animalController))
            {
                if (_trackedAnimals.Contains(animalController) || animalController.IsInYard) 
                    continue;

                animalController.FollowPlayer(transform);
                _trackedAnimals.Add(animalController);
            }
        }
    }

    public void FollowTarget()
    {
        var direction = _target - (Vector2)transform.position;
        _rb.velocity = direction.normalized * _speed * Time.deltaTime;
        
        transform.position = Vector2.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }

    public void SetFollowTarget(Vector2 target)
    {
        _target = target;
    }
}
