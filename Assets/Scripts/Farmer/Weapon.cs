using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Attack Speed (Rotation)")]
    public float _baseAttackSpeed = 200f;
    public float _finalAttackSpeeed = 1f;

    [Header("Player Move Speed")]
    public float _baseMoveSpeed = 7f;
    public float _finalMoveSpeeed = 1f;

    [Header("Damage")]
    public float _baseDamage = 1f;
    public float FinalDamage = 1f;

    [Header("Blade Size")]
    public float _baseSize = 1f;
    public float _finalSize = 1f;

    [Header("Blade Number")]
    public float _baseNumber = 1f;
    public float _finalNumber = 1f;

    [Header("Effect timer")]
    public float AttackSpeedSecondsLeft = 0;
    public float MoveSpeedSecondsLeft = 0;
    public float DamageSecondsLeft = 0;
    public float SizeSecondsLeft = 0;
    public float NumberSecondsLeft = 0;

    [Header("Multipliers")]
    public float _baseMultiplier = 2.25f;
    public float _moveSpeedMultiplier = 1.25f;
    public float _sizeMultiplier = 1.25f;

    [Header("Other settings")]
    public SpriteRenderer _bladeSpriteRenderer;
    public AuraSlotFiller AuraSlotFiller;
    public Movement PlayerMovement;
    public TextMeshPro TotalDamage;
    public float TotalDamageValue = 0;
    public AudioSource KnifeSwip;

    private float _soundTimer = 0f;
    private float _totalRotation = 0f;

    public List<Transform> Blades = new List<Transform>();

    private void Start()
    {
        RemoveAttackSpeed();
        RemoveMoveSpeed();
        RemoveDamage();
        RemoveSize();
        RemoveNumber();
    }

    private void Update()
    {
        transform.Rotate(0f, 0f, _finalAttackSpeeed * Time.deltaTime);
        // TotalDamage.text = "total: " + TotalDamageValue.ToString() + "!";

        // if(PlayerMovement.IsDead == false)
        // {
        //     // Calculate how many degrees the knife rotated this frame
        //     float rotationThisFrame = _finalAttackSpeeed * Time.deltaTime;
        //     _totalRotation += rotationThisFrame;

        //     // Play sound every 360 degrees (one full rotation)
        //     if (!KnifeSwip.isPlaying && _totalRotation >= 360f)
        //     {
        //         KnifeSwip.Play();
        //         _totalRotation = 0f;
        //     }
        // }

        #region timers
        if (AttackSpeedSecondsLeft > 0)
        {
            AttackSpeedSecondsLeft -= Time.deltaTime;
            _finalAttackSpeeed = _baseAttackSpeed * _baseMultiplier * AuraSlotFiller.GetActiveAurasCount();

            if (AttackSpeedSecondsLeft <= 0)
            {
                RemoveAttackSpeed();
            }
        }

        if (MoveSpeedSecondsLeft > 0)
        {
            MoveSpeedSecondsLeft -= Time.deltaTime;
            float activeAuras = AuraSlotFiller.GetActiveAurasCount();

            if (activeAuras == 1)
                _finalMoveSpeeed = _baseMoveSpeed * 1.44f;
            else if (activeAuras == 2)
                _finalMoveSpeeed = _baseMoveSpeed * 1.88f;
            else if (activeAuras == 3)
                _finalMoveSpeeed = _baseMoveSpeed * 2.32f;

            PlayerMovement.MoveSpeed = _finalMoveSpeeed;

            if (MoveSpeedSecondsLeft <= 0)
            {
                RemoveMoveSpeed();
            }
        }

        if (DamageSecondsLeft > 0)
        {
            DamageSecondsLeft -= Time.deltaTime;
            FinalDamage = _baseDamage * _baseMultiplier * AuraSlotFiller.GetActiveAurasCount();

            if (DamageSecondsLeft <= 0)
            {
                RemoveDamage();
            }
        }

        if (SizeSecondsLeft > 0)
        {
            SizeSecondsLeft -= Time.deltaTime;
            float activeAuras = AuraSlotFiller.GetActiveAurasCount();

            if (activeAuras == 1)
                _finalSize = _baseSize * 1.7f;
            else if (activeAuras == 2)
                _finalSize = _baseSize * 2.4f;
            else if (activeAuras == 3)
                _finalSize = _baseSize * 3.1f;

            foreach (Transform blade in Blades)
            {
                blade.transform.localScale = new Vector3(_finalSize, transform.localScale.y, 0f);
            }

            FindFirstObjectByType<CameraFollow>().SetIncreasedZoom();

            if (SizeSecondsLeft <= 0)
            {
                RemoveSize();
            }
        }

        if (NumberSecondsLeft > 0)
        {
            NumberSecondsLeft -= Time.deltaTime;
            _finalNumber = _baseNumber * AuraSlotFiller.GetActiveAurasCount();

            if (_finalNumber == 1)
            {
                Blades[1].gameObject.SetActive(true);

                Blades[2].gameObject.SetActive(false);
                Blades[3].gameObject.SetActive(false);
                Blades[4].gameObject.SetActive(false);
                Blades[5].gameObject.SetActive(false);
            }

            if (_finalNumber == 2)
            {
                Blades[1].gameObject.SetActive(false);
                Blades[4].gameObject.SetActive(false);
                Blades[5].gameObject.SetActive(false);

                Blades[2].gameObject.SetActive(true);
                Blades[3].gameObject.SetActive(true);
            }

            if (_finalNumber == 3)
            {
                Blades[1].gameObject.SetActive(true);
                Blades[4].gameObject.SetActive(true);
                Blades[5].gameObject.SetActive(true);

                Blades[2].gameObject.SetActive(false);
                Blades[3].gameObject.SetActive(false);
            }

            if (NumberSecondsLeft <= 0)
            {
                RemoveNumber();
            }
        }

        #endregion
    }

    #region attack speed
    public void SetAttackSpeed(float seconds)
    {
        AttackSpeedSecondsLeft = seconds;
    }

    public void RemoveAttackSpeed()
    {
        _finalAttackSpeeed = _baseAttackSpeed;
    }
    #endregion

    #region move speed
    public void SetMoveSpeed(float seconds)
    {
        MoveSpeedSecondsLeft = seconds;
    }

    public void RemoveMoveSpeed()
    {
        _finalMoveSpeeed = _baseMoveSpeed;
        PlayerMovement.MoveSpeed = _finalMoveSpeeed;
    }
    #endregion

    #region damage
    public void SetDamage(float seconds)
    {
        DamageSecondsLeft = seconds;
    }

    public void RemoveDamage()
    {
        FinalDamage = _baseDamage;
    }
    #endregion

    #region size
    public void SetSize(float seconds)
    {
        SizeSecondsLeft = seconds;
    }

    public void RemoveSize()
    {
        foreach (Transform blade in Blades)
        {
            blade.transform.localScale = new Vector3(_baseSize, transform.localScale.y, 1f);
        }

        FindFirstObjectByType<CameraFollow>().SetDefaultZoom();
    }
    #endregion

    #region number
    public void SetNumber(float seconds)
    {
        NumberSecondsLeft = seconds;
    }

    public void RemoveNumber()
    {
        _finalNumber = _baseNumber;

        for (int i = 1; i < Blades.Count; i++)
        {
            Blades[i].gameObject.SetActive(false);
        }
    }
    #endregion
}
