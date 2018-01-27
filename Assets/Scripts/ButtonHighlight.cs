using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonHighlight : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    [SerializeField]
    private Color[] _colors;
    [SerializeField]
    private Image[] _images;
    [SerializeField]
    private TextMeshProUGUI _buttonText;
    [SerializeField]
    private Image _fader;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_fader.enabled)
        {
            foreach(Image img in _images)
            {
                img.color = _colors[1];
            }
            _buttonText.color = _colors[1];
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (Image img in _images)
        {
            img.color = _colors[0];
        }
        _buttonText.color = _colors[0];
    }
}
