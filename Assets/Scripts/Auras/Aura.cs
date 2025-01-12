using System;
using UnityEngine;

public class Aura : MonoBehaviour, IInteractable
{
    public bool CanCollect { get; private set; } = true;
    public float Duration { get; private set; } = 5f;

    private SpriteRenderer _renderer;

    public event Action<Vector3, Aura> Collected;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Deactivate()
    {
        CanCollect = false;
        Collected?.Invoke(transform.position, this);
        gameObject.SetActive(false);
    }

    public void Initialize(Color color)
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = color;
    }

    public Color GetColor()
    {
        return _renderer.color;
    }
}
