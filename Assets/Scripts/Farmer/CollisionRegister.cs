using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class CollisionRegister : MonoBehaviour
{
    public event Action<Aura> AuraFound;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out IInteractable interactable))
        {
            if (interactable is Aura)
                AuraFound?.Invoke(interactable as Aura);
        }
    }
}