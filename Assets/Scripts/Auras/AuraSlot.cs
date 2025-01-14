using System;
using System.Collections;
using UnityEngine;

public class AuraSlot : MonoBehaviour
{
    private SpriteRenderer _renderer;

    public bool IsEmpty { get; private set; } = true;
    public float SecondsLeft { get; private set; }

    public event Action OnDeactivated;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        Deactivate();
    }

    private void Update()
    {
        if (IsEmpty == false && SecondsLeft > 0)
        {
            SecondsLeft -= Time.deltaTime;

            if (SecondsLeft <= 0)
            {
                Deactivate();
            }
        }
    }

    public void Initialize(Color color, float seconds)
    {
        _renderer.enabled = true;
        _renderer.color = color;
        SecondsLeft = seconds;
        IsEmpty = false;
    }

    public void Deactivate()
    {
        _renderer.enabled = false;
        IsEmpty = true;
        OnDeactivated?.Invoke();
    }
}
