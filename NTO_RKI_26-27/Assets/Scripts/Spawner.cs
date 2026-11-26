using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemyData[] Enemy;
    public float WaveTime;
    public int SpawnCount;
    public bool isWaveActive;

    private float minX, maxX, minZ, maxZ;
    private float spawnRate;
    private float spawnPause;
    private int currentWaveNumber;

    [SerializeField] GameObject Krest;
    [SerializeField] float lenSide = 4.5f;

    private void Start()
    {
        minX = transform.position.x - (lenSide * transform.localScale.x);
        minZ = transform.position.z - (lenSide * transform.localScale.z);
        maxX = transform.position.x + (lenSide * transform.localScale.x);
        maxZ = transform.position.z + (lenSide * transform.localScale.z);
    }

    private void OnEnable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError("GameBootstrap = null"); return; }
        bus.Subscribe<WaveStartedEvent>(OnWaveStarted);
    }

    private void OnDisable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) return;
        bus.Unsubscribe<WaveStartedEvent>(OnWaveStarted);
    }

    private void Update()
    {
        if (!isWaveActive) return;

        WaveTime -= Time.deltaTime;
        spawnPause += Time.deltaTime;

        if (spawnPause >= spawnRate && WaveTime >= 4f)
        {
            spawnPause = 0f;
            StartCoroutine(Spawn());
        }
        else if (WaveTime <= 0f)
        {
            isWaveActive = false;
            GameBootstrap.Instance?.Bus.Publish(new WaveEndedEvent(currentWaveNumber));
        }
    }

    private IEnumerator Spawn()
    {
        Vector3 point = new Vector3(Random.Range(minX, maxX),Random.Range(0f, 0.2f),Random.Range(minZ, maxZ));

        GameObject krest = Instantiate(Krest, point, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(krest);

        if (Enemy == null || Enemy.Length == 0) yield break;

        EnemyData data = Enemy[Random.Range(0, Enemy.Length)];
        Enemy enemy = Instantiate(data.EnemyPref, point, Quaternion.identity).GetComponent<Enemy>();
        enemy.DataUpdate(data);
        Destroy(enemy.gameObject, WaveTime);
    }

    private void OnWaveStarted(WaveStartedEvent evt)
    {
        if (evt.Wave == null) { Debug.LogWarning("Wave == null"); return; }
        if (evt.Wave.SpawnCount <= 0) { Debug.LogWarning("SpawnCount <= 0"); return; }
        if (evt.Wave.Enemy == null || evt.Wave.Enemy.Length == 0) { Debug.LogWarning("Enemy == null"); return; }

        WaveTime = evt.Wave.WaveTime;
        SpawnCount = evt.Wave.SpawnCount;
        Enemy = evt.Wave.Enemy;
        currentWaveNumber = evt.Number;

        isWaveActive = true;
        spawnPause = 0f;
        spawnRate = (WaveTime - 4f) / SpawnCount;
    }
}