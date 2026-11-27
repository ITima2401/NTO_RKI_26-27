using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour
{
    public Item item;
    public GameObject InfPanel;
    public TMP_Text text;
    public void PointerEnter(BaseEventData eventData)
    {
        if(InfPanel == null || text == null || item == null) return;
        InfPanel.SetActive(true);
        text.text = item.Info();
    }
    public void PointerExit(BaseEventData eventData)
    {
        if (InfPanel == null) return;
        InfPanel.SetActive(false);
    }
}
