using UnityEngine;
using UnityEngine.Rendering;

public class PlantableObject : MonoBehaviour, IInteractable
{
    // [SerializeField] private Sprite _startSprite;
    // [SerializeField] private Sprite _mediumSprite;
    // [SerializeField] private Sprite _readySprite;
    [SerializeField] private Sprite[] _growthStages;  // Array of sprites for different stages
     private float _timeBetweenStages = 6f;  // Time in seconds between stages
    // [SerializeField] private int wateringsNeeded = 2;  // How many times plant needs water

    private SpriteRenderer _spriteRenderer;
    private int _currentStage = 0;
    private float _nextStageTime;
    // private int timesWatered = 0;
    // private bool needsWater = false;

    public bool IsCut { get; private set; } = false;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _growthStages[0];
        _nextStageTime = Time.time + _timeBetweenStages;
    }

    private void Update()
    {
        if (_currentStage >= _growthStages.Length - 1)
            return;

        // if (needsWater)
        // {
        //     // Plant won't grow until watered
        //     return;
        // }

        if (Time.time >= _nextStageTime)
        {
            GrowToNextStage();
        }
    }

    public bool IsReadyToCut()
    {
        return _currentStage >= _growthStages.Length - 1;
    }

    public void SetCut()
    {
        IsCut = true;
    }

    public void Deactivate()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        gameObject.SetActive(false);
    }

    private void GrowToNextStage()
    {
        _currentStage++;
        _spriteRenderer.sprite = _growthStages[_currentStage];

        // // Check if plant needs water before next stage
        // if (currentStage >= growthStages.Length - wateringsNeeded)
        // {
        //     needsWater = true;
        //     // Optional: Add visual indicator that plant needs water
        //     // For example, show a water droplet sprite above
        // }
        // else
        // {
        _nextStageTime = Time.time + _timeBetweenStages;
        // }
    }

    // public void Water()
    // {
    //     if (needsWater)
    //     {
    //         needsWater = false;
    //         timesWatered++;
    //         nextStageTime = Time.time + timeBetweenStages;
    //     }
    // }
}
