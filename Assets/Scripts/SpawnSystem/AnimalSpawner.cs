using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimalSpawner : SpawnerBase<AnimalController>
{
    [SerializeField] private LevelController _levelController;
    [SerializeField] private int _maxAnimalsOnLevel = 12;
    [SerializeField] private int _minAnimalsOnLevel = 3;
    [SerializeField] private float _maxSpawnInterval = 3;
    [SerializeField] private float _minSpawnInterval = 0;

    public event Action OnAnimalSpawnedEvent;

    private List<AnimalController> _spawnedAnimals;
    private float _timer;


    public void SpawnRandomAmountOfAnimalsOnLevel()
    {
        var animalsCount = Random.Range(_minAnimalsOnLevel, _maxAnimalsOnLevel);

        if (_spawnedAnimals == null)
            _spawnedAnimals = new List<AnimalController>();
        else
            DisposeSpawnedAnimals();

        for (var i = 0; i < animalsCount; i++)
        {
            SpawnAnimal();
        }
    }

    public void StartRecursiveAnimalSpawnWithDelay(CancellationToken cancellationToken)
    {
        SpawnAnimalWithDelayRecursive(cancellationToken).Forget();
    }

    private async UniTask SpawnAnimalWithDelayRecursive(CancellationToken cancellationToken)
    {
        await SpawnAnimalWithDelay();
        if (cancellationToken.IsCancellationRequested)
            return;
        await SpawnAnimalWithDelayRecursive(cancellationToken);
    }

    private async UniTask SpawnAnimalWithDelay()
    {
        var rand = new System.Random();
        var delay = rand.NextDouble() * (_maxSpawnInterval - _minSpawnInterval) + _minSpawnInterval;
        await UniTask.Delay(TimeSpan.FromSeconds(delay));
        SpawnAnimal();
    }

    private void SpawnAnimal()
    {
        AnimalController animal = null;
        do
        {
            if (animal != null)
                Destroy(animal.gameObject);

            animal = Spawn(_levelController.GetRandomPositionOnLevel());
        } while (animal.IsInYard);

        _spawnedAnimals.Add(animal);
        
        OnAnimalSpawnedEvent?.Invoke();
    }

    private void DisposeSpawnedAnimals()
    {
        foreach (var animal in _spawnedAnimals)
        {
            animal?.Dispose();
            Destroy(animal.gameObject);
        }

        _spawnedAnimals = new List<AnimalController>();
    }
}
