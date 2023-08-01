using System;
using System.Threading;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private AnimalSpawner _animalSpawner;

    public AnimalSpawner GetAnimalSpawner() => _animalSpawner;

    public event Action<PlayerController> OnPlayerSpawnedEvent;

    private CancellationTokenSource _cts;
    
    private void Start()
    {
        _cts = new CancellationTokenSource(); 
        
        _animalSpawner.SpawnRandomAmountOfAnimalsOnLevel();
        _animalSpawner.StartRecursiveAnimalSpawnWithDelay(_cts.Token);
        OnPlayerSpawnedEvent?.Invoke(_playerSpawner.Spawn(Vector3.zero));
    }

    private void OnDestroy()
    {
        _cts.Cancel();
    }
}
