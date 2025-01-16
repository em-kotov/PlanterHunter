using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public float MoveSpeed = 7f;
    public Collider2D PlayerCollider;
    public SpriteRenderer PlayerRenderer;
    public SpriteRenderer HatRenderer;
    public Weapon Weapon;
    public TextMeshPro HealthText;
    public TextMeshPro DamageText;
    public ParticleSystem DeathEffect;

    private FarmerInput _farmerInput;
    private Vector3 _startPosition = new Vector3(0f, 0f, 0f);
    private Vector2 _moveDirection;
    private Vector2 _smoothedDirection;
    private float _deadzone = 0.1f;
    private float _smoothing = 0.1f;
    private float _health = 5.7f;
    private float _damage = 0.95f;
    private bool _isDead = false;

    private void Awake()
    {
        _farmerInput = new FarmerInput();

        PlayerCollider.enabled = true;
        PlayerRenderer.enabled = true;
        HatRenderer.enabled = true;
        Weapon.enabled = true;
        _isDead = false;
    }

    private void Start()
    {
        // transform.position = _startPosition;
    }

    private void Update()
    {
        if (_isDead == true)
            return;

        if (_health <= 0)
        {
            Die();
        }

        Move(_farmerInput.Farmer.Move.ReadValue<Vector2>());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            _health -= _damage;

            if (_health < 0)
                _health = 0;

            //damage indicator text
            TextMeshPro text = Instantiate(DamageText, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);
            string indicator = "-" + _damage.ToString() + "!";
            text.text = indicator;
            text.transform.Translate(Vector3.up * Time.deltaTime * 2f);
            Destroy(text, 0.3f);

            //total health text
            HealthText.text = "health: " + _health.ToString() + "!";
        }
    }

    private void OnEnable()
    {
        _farmerInput.Enable();
    }

    private void OnDisable()
    {
        _farmerInput.Disable();
    }

    private void OnDestroy()
    {
        _farmerInput.Dispose();
    }

    public void Move(Vector2 direction)
    {
        _moveDirection = direction;

        // Apply smoothing
        _smoothedDirection = Vector2.Lerp(_smoothedDirection, _moveDirection, 1f / _smoothing);

        // More reliable deadzone check
        if (Mathf.Abs(_smoothedDirection.x) < _deadzone)
        {
            _smoothedDirection.x = 0f;
        }
        if (Mathf.Abs(_smoothedDirection.y) < _deadzone)
        {
            _smoothedDirection.y = 0f;
        }

        // Only move if we have actual input
        if (_smoothedDirection != Vector2.zero)
        {
            float scaledMoveSpeed = MoveSpeed * Time.deltaTime;
            Vector3 offset = new Vector3(_smoothedDirection.x, _smoothedDirection.y, 0f) * scaledMoveSpeed;
            transform.Translate(offset);
        }
    }

    private void Die()
    {
        PlayerCollider.enabled = false;
        PlayerRenderer.enabled = false;
        HatRenderer.enabled = false;
        Weapon.enabled = false;
        _isDead = true;

        ParticleSystem effect = Instantiate(DeathEffect, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        effect.Play();
        float duration = effect.main.duration;
        Destroy(effect.gameObject, duration);

        StartCoroutine(ReloadWithDelay(duration + 1f));
    }

    private IEnumerator ReloadWithDelay(float seconds)
    {
        WaitForSeconds delay = new WaitForSeconds(seconds);
        yield return delay;

        SceneManager.LoadScene("Version 2");
    }
}