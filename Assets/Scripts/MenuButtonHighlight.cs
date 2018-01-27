using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuButtonHighlight : MonoBehaviour, IPointerEnterHandler, IDeselectHandler
{

    public TextMeshProUGUI buttonText;
    [SerializeField]
    private Color[] textColors;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!EventSystem.current.alreadySelecting)
        {
            EventSystem.current.SetSelectedGameObject(this.gameObject);
            buttonText.color = textColors[1];
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        this.GetComponent<Selectable>().OnPointerExit(null);
        buttonText.color = textColors[0];
    }
}
