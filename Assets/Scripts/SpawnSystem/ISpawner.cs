using System;
using UnityEngine;

public interface ISpawner<T>
    where T : SpawnableEntity
{
    T Spawn(Vector3 position);

    public event Action<T> OnSpawnEvent;
}