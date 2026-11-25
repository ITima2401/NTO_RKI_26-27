using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int HP;
    public int MaxHP;
    private void OnEnable()
    {
        PlayerStats.OnHPChanged += HPChange;
        PlayerStats.OnMaxHPChanged += MaxHPChange;
    }
    private void OnDisable()
    {
        PlayerStats.OnHPChanged -= HPChange;
        PlayerStats.OnMaxHPChanged -= MaxHPChange;
    }
    private void HPChange(int hp)
    {
        HP = hp;
        if (HP <= 0) Death();
    }
    private void MaxHPChange(int hp)
    {
        MaxHP = hp;
        if (HP <= 0) Death();
    }
    private void Death()
    {
        Debug.Log("Смерть");
    }
}
