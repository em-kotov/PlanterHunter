using System;
using UnityEngine;

public class Aura : MonoBehaviour, IInteractable
{
    public bool IsIncreasingAttackSpeed = true;
    public bool IsIncreasingDamage = true;
    public bool IsIncreasingMoveSpeed = true;

    private SpriteRenderer _renderer;

    public float Duration { get; private set; } = 4f;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        AuraSlotFiller filler = collider.gameObject.GetComponentInChildren<AuraSlotFiller>();
        Weapon weapon = collider.gameObject.GetComponentInChildren<Weapon>();

        if (filler != null && weapon != null)
        {
            filler.SetAura(_renderer.color, Duration);

            if (IsIncreasingAttackSpeed)
                weapon.SetAttackSpeed(Duration);

            if (IsIncreasingDamage)
                weapon.SetDamage(Duration);

            if (IsIncreasingMoveSpeed)
                weapon.SetMoveSpeed(Duration);

            gameObject.SetActive(false);
        }
    }
}
