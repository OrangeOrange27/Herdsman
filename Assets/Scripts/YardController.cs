using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class YardController : MonoBehaviour
{
    public event Action<AnimalController> OnMoveAnimalToYardEvent;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Animal"))
        {
            var animal = col.GetComponent<AnimalController>();
            col.enabled=false;

            animal.MoveToYard(transform);
            
            OnMoveAnimalToYardEvent?.Invoke(animal);
        }
    }
}
