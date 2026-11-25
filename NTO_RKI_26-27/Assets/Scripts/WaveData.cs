using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/Wave")]
public class WaveData : ScriptableObject
{
    public EnemyData[] Enemy;
    public float WaveTime;
    public int SpawnCount;
}
