using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private YardController _yardController;
    [SerializeField] private UIController _ui;
    [SerializeField] private SpawnManager _spawnManager;

    private int _collectedAnimalsCount;
    private int _totalAnimalsCount;
    private AnimalSpawner _animalSpawner;
    private PlayerController _player;

    private void Awake()
    {
        _collectedAnimalsCount = 0;
        _totalAnimalsCount = 0;

        _animalSpawner = _spawnManager.GetAnimalSpawner();
        
        _yardController.OnMoveAnimalToYardEvent += OnMoveAnimalToYard;
        _animalSpawner.OnAnimalSpawnedEvent += OnAnimalSpawned;
        _spawnManager.OnPlayerSpawnedEvent += OnPlayerSpawned;
    }

    private void OnAnimalSpawned()
    {
        _totalAnimalsCount++;
        UpdateUI();
    }

    private void OnPlayerSpawned(PlayerController playerController)
    {
        _player = playerController;
    }
    
    private void OnMoveAnimalToYard(AnimalController animal)
    {
        _collectedAnimalsCount++;
        UpdateUI();

        _player.TrackedAnimals.Remove(animal);
    }

    private void UpdateUI()
    {
        UpdateUI(_collectedAnimalsCount, _totalAnimalsCount);
    }
    
    private void UpdateUI(int count, int max)
    {
        _ui.UpdateUI(count,max);
    }

    private void OnDestroy()
    {
        _animalSpawner.OnAnimalSpawnedEvent -= OnAnimalSpawned;
        _yardController.OnMoveAnimalToYardEvent -= OnMoveAnimalToYard;
        _spawnManager.OnPlayerSpawnedEvent -= OnPlayerSpawned;
    }
}
