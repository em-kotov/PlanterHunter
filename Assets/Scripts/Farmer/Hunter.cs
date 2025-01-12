using UnityEngine;

public class Hunter : MonoBehaviour
{
    [SerializeField] private AuraSlot _auraSlot1;
    [SerializeField] private AuraSlot _auraSlot2;
    [SerializeField] private AuraSlot _auraSlot3;

    [Header("Visual Feedback")]
    [SerializeField] private SpriteRenderer _hatSprite;

    private int _activeAuraSlotsCount = 0;
   
    public void SetActive(bool isActive)
    {
        _hatSprite.enabled = isActive;
        enabled = isActive;
    }

    public void Shoot()
    {

    }
}
