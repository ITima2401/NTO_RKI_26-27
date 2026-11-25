using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] WaveData[] waveDatas;
    public static event Action<WaveData> WaveStart;
    private int WaveNumber;
    public void WaveStarting()
    {
        WaveStart?.Invoke(waveDatas[WaveNumber % (waveDatas.Length)]);
        WaveNumber++;
        PlayerStats.WaveNum = WaveNumber;
    }
}
