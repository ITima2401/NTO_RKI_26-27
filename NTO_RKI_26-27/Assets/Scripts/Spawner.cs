using System;
using System.Collections;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    public EnemyData[] Enemy;
    public float WaveTime;
    public int SpawnCount;
    public bool isWaveActive;
    public static event Action WaveEnd;
    float minX;
    float maxX;
    float minZ;
    float maxZ;
    private float spawnRate;
    private float spawnPause;
    [SerializeField] GameObject Krest;
    [SerializeField] float lenSide = 4.5f;

    void Start()
    {
        minX = transform.position.x - (lenSide * transform.localScale.x);
        minZ = transform.position.z - (lenSide * transform.localScale.z);
        maxX = transform.position.x + (lenSide * transform.localScale.x);
        maxZ = transform.position.z + (lenSide * transform.localScale.z);
    }

    void Update()
    {
        if (isWaveActive)
        {
            WaveTime -= Time.deltaTime;
            spawnPause += Time.deltaTime;
            if(spawnPause >= spawnRate && WaveTime >= 4f)
            {
                spawnPause = 0f;
                StartCoroutine(Spawn());
            }
            else if(WaveTime <= 0f)
            {
                isWaveActive = false;
                WaveEnd?.Invoke();
            }
        }
    }
    IEnumerator Spawn()
    {
        Vector3 PointOfSpawn = new Vector3(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(0f, 0.2f), UnityEngine.Random.Range(minZ, maxZ));
        GameObject _krest = Instantiate(Krest, PointOfSpawn, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(_krest);
        EnemyData data = Enemy[UnityEngine.Random.Range(0, Enemy.Length)];
        Enemy _enemy = Instantiate(data.EnemyPref, PointOfSpawn, Quaternion.identity).GetComponent<Enemy>();
        _enemy.DataUpdate(data);
        Destroy(_enemy.gameObject,WaveTime);
        yield return null;
    }
    public void WaveStart(WaveData waveData)
    {
        WaveTime = waveData.WaveTime;
        SpawnCount = waveData.SpawnCount;
        Enemy = waveData.Enemy;
        isWaveActive=true;
        spawnRate = (WaveTime - 4) / SpawnCount;
    }
    private void OnEnable()
    {
        WaveManager.WaveStart += WaveStart;
    }
    private void OnDisable()
    {
        WaveManager.WaveStart -= WaveStart;
    }
}
