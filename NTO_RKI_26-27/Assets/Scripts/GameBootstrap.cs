using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    public static GameBootstrap Instance { get; private set; }
    public IEventBus Bus { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        Bus = new EventBus();
    }

    private void OnDestroy()
    {
        if (Instance != this) return;
        Bus?.Clear();
        Bus = null;
        Instance = null;
    }
}