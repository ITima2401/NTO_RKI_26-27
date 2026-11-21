using System.Collections.Generic;
using UnityEngine;

public class WeaponsPos : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] int count;
    [SerializeField] float radius;
    [SerializeField] GameObject[] Weapons;//временно
    private List<GameObject> livePref = new List<GameObject>();
    private void Start()
    {
        PlayerStats.Weapons = new List<GameObject>(Weapons);
    }
    private void ChosePos(WaveData wave)
    {
        count = PlayerStats.Weapons.Count;
        float step = 2f * Mathf.PI / count;

        for (int i = 0; i < count; i++)
        {
            float angle = i * step;
            Vector3 pos = new Vector3(
                Mathf.Cos(angle) * radius,
                0f,
                Mathf.Sin(angle) * radius
            );

            GameObject targetInstance = Instantiate(target, pos + transform.position, Quaternion.identity, transform);
            GameObject weaponInstance = Instantiate(PlayerStats.Weapons[i], pos + transform.position, Quaternion.identity);

            weaponInstance.GetComponent<Weapon>().Target = targetInstance.transform;

            livePref.Add(weaponInstance);
            livePref.Add(targetInstance);

        }
    }
    private void Clean()
    {
        for (int i = 0; i < livePref.Count; i++)
        {
            Destroy(livePref[i]);
        }
        livePref.Clear();
    }
    private void OnEnable()
    {
        WaveManager.WaveStart += ChosePos;
        Spawner.WaveEnd += Clean;
    }
    private void OnDisable()
    {
        WaveManager.WaveStart -= ChosePos;
        Spawner.WaveEnd -= Clean;

    }
}
