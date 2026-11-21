using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] WaveData[] waveDatas;
    [SerializeField] GameObject Shop;
    public static event Action<WaveData> WaveStart;
    private int WaveNumber;
    public void WaveStarting()
    {
        Shop.SetActive(false);
        WaveStart?.Invoke(waveDatas[WaveNumber % (waveDatas.Length)]);
        WaveNumber++;
    }
    public void WaveEnding()
    {
        Shop.SetActive(true);
    }
    private void OnEnable()
    {
        Spawner.WaveEnd += WaveEnding;
    }
    private void OnDisable()
    {
        Spawner.WaveEnd -= WaveEnding;

    }
}
