using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float Health = 6.75f;
    public ParticleSystem DeathEffect;
    public TextMeshPro DamageText;

    private Transform _player;
    private float _speed = 1.5f;
    private float _chaseDistance = 13f;
    private float _detectRadius = 13f;
    private bool _canChase = false;

    private void Awake()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Die();
        }

        DetectPlayer();

        if (!_canChase)
            return;

        ChasePlayer();
        UpdateTarget();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Weapon weapon = collider.gameObject.GetComponentInParent<Weapon>();

        if (weapon != null)
        {
            float registeredDamage = weapon.FinalDamage;
            Health -= registeredDamage;

            //enemy damage indicator text
            TextMeshPro text = Instantiate(DamageText, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);
            string indicator = "+" + registeredDamage.ToString() + "!";
            text.text = indicator;
            text.transform.Translate(Vector3.up * Time.deltaTime * 2f);
            Destroy(text, 0.3f);

            //weapon total damage text
            weapon.TotalDamageValue += registeredDamage;
            weapon.TotalDamage.text = "total damage: " + weapon.TotalDamageValue.ToString() + "!";
        }
    }

    private void Die()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        ParticleSystem effect = Instantiate(DeathEffect, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        effect.Play();
        gameObject.SetActive(false);
    }

    private void DetectPlayer()
    {
        int playerLayerIndex = 8;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _detectRadius, LayerMaskConverter.GetLayerMask(playerLayerIndex));

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Movement player))
            {
                _player = player.transform;
                _canChase = true;
            }
        }
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
    }

    private void UpdateTarget()
    {
        if (Vector3Extensions.IsEnoughClose(transform.position, _player.position, _chaseDistance) == false)
        {
            _player = null;
            _canChase = false;
        }
    }
}
