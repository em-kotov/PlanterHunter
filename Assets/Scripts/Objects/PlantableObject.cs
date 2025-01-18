using UnityEngine;

public class PlantableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite[] _growthStages;
    [SerializeField] private AudioSource _popSound;

    private float _timeBetweenStages = 2f;
    private SpriteRenderer _spriteRenderer;
    private int _currentStage = 0;
    private float _nextStageTime;

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

        if (Time.time >= _nextStageTime)
            GrowToNextStage();
    }

    public bool IsReadyToCut()
    {
        return _currentStage >= _growthStages.Length - 1;
    }

    public void SetCut()
    {
        IsCut = true;
    }

    private void GrowToNextStage()
    {
        _currentStage++;
        _spriteRenderer.sprite = _growthStages[_currentStage];
        _nextStageTime = Time.time + _timeBetweenStages;

        if(_currentStage >= 2f)
        {
            _popSound.Play();
        }
    }
}
