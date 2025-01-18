using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    void Awake()
    {
        // If there's no instance yet, set this as the instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // If an instance already exists, destroy this duplicate
        else
        {
            Destroy(gameObject);
        }
    }
}
