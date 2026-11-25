using System.Collections.Generic;
using UnityEngine;

public class WeaponsPos : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] int count;
    [SerializeField] float radius;
    private List<GameObject> livePref = new List<GameObject>();
    private void ChosePos(WaveData wave)
    {
        count = PlayerStats.Weapons.Count;
        float step = 2f * Mathf.PI / count;

        for (int i = 0; i < count; i++)
        {
            float angle = i * step;
            Vector3 pos = new Vector3(Mathf.Cos(angle) * radius,1f,Mathf.Sin(angle) * radius);

            GameObject targetInstance = Instantiate(target, pos + transform.position, Quaternion.identity, transform);
            Weapon weaponInstance = Instantiate(PlayerStats.Weapons[i].WeaponPrefab, pos + transform.position, Quaternion.identity).GetComponent<Weapon>();

            weaponInstance.Target = targetInstance.transform;
            weaponInstance.InstData(PlayerStats.Weapons[i]);

            livePref.Add(weaponInstance.gameObject);
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
