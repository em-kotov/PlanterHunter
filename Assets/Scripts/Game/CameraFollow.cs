using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _player;
    public float _smoothness = 0.1f;  // Lower value = more delay (0.1)
    public float _leadDistance = 0.5f;   // How far ahead camera looks (2)
    public float _zoomSpeed = 0.5f;      // How fast camera zooms (2)
    public float _minZoom = 6f;        // Camera won't zoom closer than this (5)
    public float _maxZoom = 9f;        // Camera won't zoom further than this (8)
    public SpriteRenderer BackgroundSprite;

    private Vector3 lastPlayerPos;
    private float currentZoom;

    private void Start()
    {
        if (_player != null)
        {
            lastPlayerPos = _player.position;
            currentZoom = (_minZoom + _maxZoom) / 2f;  // Start at middle zoom
            // currentZoom = _minZoom;
        }
    }

    private void LateUpdate()
    {
        if (_player == null) return;

        // Calculate player's movement direction
        Vector3 moveDirection = (_player.position - lastPlayerPos).normalized;
        lastPlayerPos = _player.position;

        // Target position is ahead of player based on their movement
        Vector3 targetPosition = _player.position + moveDirection * _leadDistance;

        // Keep camera's height (y position)
        targetPosition.z = transform.position.z;

        // Smooth camera position
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            _smoothness * Time.deltaTime * 60f  // Make it frame-rate independent
        );

        // Dynamic zoom based on player speed
        float speed = (_player.position - lastPlayerPos).magnitude;
        float targetZoom = Mathf.Lerp(_minZoom, _maxZoom, speed / 10f);
        currentZoom = Mathf.Lerp(currentZoom, targetZoom, _zoomSpeed * Time.deltaTime);

        // Apply zoom to camera
        Camera.main.orthographicSize = currentZoom;

        AnimateHue();
    }

    public void SetDefaultZoom()
    {
        _minZoom = 7f;
    }

    public void SetIncreasedZoom()
    {
        _minZoom = 12f;
    }

    public void SetHue(float hue)
    {
        // Ensure hue is between 0-360
        hue = Mathf.Clamp(hue, 0f, 360f);

        // Convert current RGB to HSV
        Color.RGBToHSV(BackgroundSprite.color, out float h, out float s, out float v);

        // Create new color with modified hue and set alpha to 7
        Color newColor = Color.HSVToRGB(hue / 360f, s, v);
        newColor.a = 2f / 255f; // Alpha needs to be in 0-1 range, so we divide by 255
        BackgroundSprite.color = newColor;
    }

    //Optional: Animate hue change over time
    public void AnimateHue()
    {
        float hue = Time.time * 100f % 360f; // Cycles through hues over time
        SetHue(hue);
    }
}