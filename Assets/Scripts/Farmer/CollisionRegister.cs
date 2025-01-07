using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class CollisionRegister : MonoBehaviour
{
    // public event Action<Aura> AuraFound;
    public event Action<PlantableObject> PlantFound;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // if (collider.gameObject.TryGetComponent(out Aura aura))
        // {
        //     AuraFound?.Invoke(aura);
        // }

        if (collider.gameObject.TryGetComponent(out IInteractable interactable))
        {
            if (interactable is PlantableObject)
                PlantFound?.Invoke(interactable as PlantableObject);
        }
    }
}