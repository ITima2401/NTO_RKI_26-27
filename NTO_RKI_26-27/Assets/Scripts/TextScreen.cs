using TMPro;
using UnityEngine;

public class TextScreen : MonoBehaviour
{
    public TMP_Text text;

    private void Update()
    {
        string textString = "";
        textString += $"Здоровье {PlayerStats.HP}     ";
        textString += $"Макс.здоровье {PlayerStats.MaxHP}    ";
        textString += $"Скорость {PlayerStats.Speed}     ";
        textString += $"Урон {PlayerStats.Damage}      ";
        textString += $"Кнопки {PlayerStats.Money}      ";
        text.text = textString;
    }
}
