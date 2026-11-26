using System.Collections.Generic;
using UnityEngine;

public class WeaponsPos : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float radius = 1.5f;
    private readonly List<GameObject> livePref = new();

    private void OnEnable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) { Debug.LogError("GameBootstrap = null"); return; }
        bus.Subscribe<WaveStartedEvent>(OnWaveStarted);
        bus.Subscribe<WaveEndedEvent>(OnWaveEnded);
    }

    private void OnDisable()
    {
        var bus = GameBootstrap.Instance?.Bus;
        if (bus == null) return;
        bus.Unsubscribe<WaveStartedEvent>(OnWaveStarted);
        bus.Unsubscribe<WaveEndedEvent>(OnWaveEnded);
    }

    private void OnWaveStarted(WaveStartedEvent _) => BuildWeapons();
    private void OnWaveEnded(WaveEndedEvent _) => Clean();

    private void BuildWeapons()
    {
        int count = GameStats.Weapons.Count;
        if (count == 0) return;

        float step = 2f * Mathf.PI / count;
        for (int i = 0; i < count; i++)
        {
            float angle = i * step;
            Vector3 pos = new Vector3(Mathf.Cos(angle) * radius, 1f, Mathf.Sin(angle) * radius);

            GameObject targetInst = Instantiate(target, pos + transform.position, Quaternion.identity, transform);

            Weapon weaponInst = Instantiate(GameStats.Weapons[i].WeaponPrefab, pos + transform.position, Quaternion.identity).GetComponent<Weapon>();
            weaponInst.InstData(GameStats.Weapons[i],targetInst.transform);

            livePref.Add(weaponInst.gameObject);
            livePref.Add(targetInst);
        }
    }

    private void Clean()
    {
        for (int i = 0; i < livePref.Count; i++)
        {
            if(livePref[i] != null) Destroy(livePref[i]);
        }
        livePref.Clear();
    }
}