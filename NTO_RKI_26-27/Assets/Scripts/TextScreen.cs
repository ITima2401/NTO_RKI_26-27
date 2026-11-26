using TMPro;
using UnityEngine;

public class TextScreen : MonoBehaviour
{
    public TMP_Text text;

    private void Update()
    {
        string textString = "";
        textString += $"Здоровье {GameStats.HP}     ";
        textString += $"Макс.здоровье {GameStats.MaxHP}    ";
        textString += $"Скорость {GameStats.Speed}     ";
        textString += $"Урон {GameStats.Damage}      ";
        textString += $"Кнопки {GameStats.Money}      ";
        text.text = textString;
    }
}
