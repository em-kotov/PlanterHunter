using System;
using UnityEngine;

public class Aura : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource _grabSound;

    public float Duration = 9f;
    public bool IsIncreasingAttackSpeed = true;
    public bool IsIncreasingMoveSpeed = true;
    public bool IsIncreasingDamage = true;
    public bool IsIncreasingSize = true;
    public bool IsIncreasingNumber = true;

    private SpriteRenderer _renderer;

    public event Action WasCollected;

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
            if (filler.GetActiveAurasCount() < 3f)
            {
                filler.SetAura(_renderer.color, Duration);

                if (IsIncreasingAttackSpeed)
                    weapon.SetAttackSpeed(Duration);

                if (IsIncreasingMoveSpeed)
                    weapon.SetMoveSpeed(Duration);

                if (IsIncreasingDamage)
                    weapon.SetDamage(Duration);

                if (IsIncreasingSize)
                    weapon.SetSize(Duration);

                if (IsIncreasingNumber)
                    weapon.SetNumber(Duration);

                WasCollected?.Invoke();
                WasCollected = null;
                _grabSound.Play();
                Collider2D thisCollider = GetComponent<Collider2D>();
                thisCollider.enabled = false;
                _renderer.enabled = false;
                Destroy(gameObject, _grabSound.clip.length);
            }
        }
    }
}
