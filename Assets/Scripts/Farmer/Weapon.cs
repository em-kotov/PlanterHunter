using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Rotation Speed")]
    public float _baseAttackSpeed = 1f;
    public float _finalAttackSpeeed = 1f;

    [Header("Damage")]
    public float _baseDamage = 1f;
    public float _finalDamage = 1f;

    [Header("Blade Number")]
    public float _baseNumber = 1f;
    public float _finalNumber = 1f;

    [Header("Blade Size")]
    public float _baseWidgth = 1f;
    public float _finalWidgth = 1f;

    [Header("Player Move Speed")]
    public float _baseMoveSpeed = 1f;
    public float _finalMoveSpeeed = 1f;

    [Header("Effect timer")]
    public float AttackSpeedSecondsLeft = 0;
    public float DamageSecondsLeft = 0;
    public float NumberSecondsLeft = 0;
    public float MoveSpeedSecondsLeft = 0;

    [Header("Blade settings")]
    public SpriteRenderer _bladeSpriteRenderer;
    [Header("2 Blade settings")]
    public SpriteRenderer _2Renderer;
    public Collider2D _2Collider;
    [Header("3 Blade settings")]
    public SpriteRenderer _3ARenderer;
    public Collider2D _3ACollider;
    public SpriteRenderer _3BRenderer;
    public Collider2D _3BCollider;

    private void Start()
    {
        RemoveAttackSpeed();
        RemoveDamage();
        RemoveMoveSpeed();
        RemoveNumber();
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, _finalAttackSpeeed * Time.deltaTime);

        //timers
        if (AttackSpeedSecondsLeft > 0)
        {
            AttackSpeedSecondsLeft -= Time.deltaTime;

            if (AttackSpeedSecondsLeft <= 0)
            {
                RemoveAttackSpeed();
            }
        }

        if (DamageSecondsLeft > 0)
        {
            DamageSecondsLeft -= Time.deltaTime;

            if (DamageSecondsLeft <= 0)
            {
                RemoveDamage();
            }
        }

        if (MoveSpeedSecondsLeft > 0)
        {
            MoveSpeedSecondsLeft -= Time.deltaTime;

            if (MoveSpeedSecondsLeft <= 0)
            {
                RemoveMoveSpeed();
            }
        }
    }

    //attack speed
    public void SetAttackSpeed(float seconds)
    {
        _finalAttackSpeeed = _baseAttackSpeed * 2f;
        AttackSpeedSecondsLeft = seconds;

        // float current = _bladeSpriteRenderer.sprite.bounds.size.x;
        // float scaleX = 2;
        // transform.localScale = new Vector3(scaleX, transform.localScale.y, 1f);
        FindFirstObjectByType<CameraFollow>().SetIncreasedZoom();
        SetNumber(seconds);
    }

    public void RemoveAttackSpeed()
    {
        _finalAttackSpeeed = _baseAttackSpeed;
        // transform.localScale = new Vector3(1, transform.localScale.y, 1f);
        FindFirstObjectByType<CameraFollow>().SetDefaultZoom();
        RemoveNumber();
    }

    //move speed
    public void SetMoveSpeed(float seconds)
    {
        _finalMoveSpeeed = _baseMoveSpeed * 2f;
        MoveSpeedSecondsLeft = seconds;
    }

    public void RemoveMoveSpeed()
    {
        _finalMoveSpeeed = _baseMoveSpeed;
    }

    //damage
    public void SetDamage(float seconds)
    {
        _finalDamage = _baseDamage * 2f;
        DamageSecondsLeft = seconds;
    }

    public void RemoveDamage()
    {
        _finalDamage = _baseDamage;
    }

    //number
    public void SetNumber(float seconds)
    {
        _finalNumber = _baseNumber * 3f;
        _finalNumber = Mathf.Clamp(_finalNumber, 1, 3);
        NumberSecondsLeft = seconds;

        if (_finalNumber == 2)
        {
            _2Renderer.enabled = true;
            _2Collider.enabled = true;
        }

        if (_finalNumber == 3)
        {
            _3ARenderer.enabled = true;
            _3ACollider.enabled = true;

            _3BRenderer.enabled = true;
            _3BCollider.enabled = true;
        }
    }

    public void RemoveNumber()
    {
        _finalNumber = _baseNumber;

        _2Renderer.enabled = false;
        _2Collider.enabled = false;

        _3ARenderer.enabled = false;
        _3ACollider.enabled = false;

        _3BRenderer.enabled = false;
        _3BCollider.enabled = false;
    }
}
