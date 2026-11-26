using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] WaveData[] waveDatas;
    private int WaveNumber;
    public void WaveStarting()
    {

        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) {Debug.LogError(" GameBootstrap = null");return;}
        if (waveDatas == null || waveDatas.Length == 0) {Debug.LogWarning("waveDatas = null");return;}

        WaveData wave = waveDatas[WaveNumber % waveDatas.Length];
        WaveNumber++;
        bus.Publish(new WaveStartedEvent(wave, WaveNumber));

        
    }
}
