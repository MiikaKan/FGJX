using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class HighlightButton : MonoBehaviour, IPointerEnterHandler, IDeselectHandler
{

    public TextMeshProUGUI buttonText;
    [SerializeField]
    private Color textColor;

    private void Start()
    {
        if (!EventSystem.current.alreadySelecting)
            EventSystem.current.SetSelectedGameObject(this.gameObject);
            buttonText.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!EventSystem.current.alreadySelecting)
            print("ye");
            EventSystem.current.SetSelectedGameObject(this.gameObject);
            buttonText.color = Color.white;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        this.GetComponent<Selectable>().OnPointerExit(null);
        buttonText.color = textColor;
    }
}
