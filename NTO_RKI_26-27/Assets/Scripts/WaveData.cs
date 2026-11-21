using UnityEngine;

[CreateAssetMenu(menuName = "Wave")]
public class WaveData : ScriptableObject
{
    public GameObject[] Enemy;
    public float WaveTime;
    public int SpawnCount;
}
