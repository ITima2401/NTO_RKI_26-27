using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] WaveData[] waveDatas;
    [SerializeField] GameObject finalPanel;
    private int WaveNumber;

    private void Start()
    {
        WaveStarting();
    }
    public void WaveStarting()
    {

        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) {Debug.LogError(" GameBootstrap = null"); return;}
        if (waveDatas == null || waveDatas.Length == 0) {Debug.LogWarning("waveDatas = null"); return;}
        if(WaveNumber >= waveDatas.Length) { finalPanel.SetActive(true); return; }
        WaveData wave = waveDatas[WaveNumber % waveDatas.Length];
        WaveNumber++;
        if (wave == null){ Debug.LogError("Пустая волна");WaveStarting(); return;} 
        bus.Publish(new WaveStartedEvent(wave, WaveNumber));

        
    }
}
