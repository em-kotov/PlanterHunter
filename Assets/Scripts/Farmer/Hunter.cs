using UnityEngine;

public class Hunter : MonoBehaviour
{
    [Header("Visual Feedback")]
    [SerializeField] private SpriteRenderer _hatSprite;

    public void SetActive(bool isActive)
    {
        _hatSprite.enabled = isActive;
        enabled = isActive;
    }
}
