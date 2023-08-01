using System;
using UnityEngine;

public abstract class SpawnerBase<T> : MonoBehaviour, ISpawner<T>
where T : SpawnableEntity
{
    [SerializeField] private T _prefab;
    
    public event Action<T> OnSpawnEvent;

    public virtual T Spawn(Vector3 position)
    {
        var obj = Instantiate(_prefab, position, _prefab.transform.rotation);
            
        OnSpawnEvent?.Invoke(obj);
        return obj;
    }

}
