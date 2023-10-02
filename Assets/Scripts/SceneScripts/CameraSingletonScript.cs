using UnityEngine;

public class CameraSingletonScript : MonoBehaviour
{
    // Static reference to the main camera instance
    public static CameraSingletonScript Instance { get; private set; }

    private void Awake()
    {
        // Ensure there is only one instance of CameraSingleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the manager GameObject persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate manager GameObjects
        }
    }
    // Add any camera-related methods or properties here
}