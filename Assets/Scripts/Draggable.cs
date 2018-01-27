using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

[RequireComponent(typeof(Outline))]
public class Draggable : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    [SerializeField]
    private int rotationAmount = 3;

    private Outline _outline;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    private void OnMouseEnter()
    {
        _outline.enabled = true;
    }

    private void OnMouseExit()
    {
        _outline.enabled = false;
    }

    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

        gameObject.transform.Rotate(new Vector3(0, rotationAmount, 0) * Input.GetAxis("Mouse ScrollWheel") * 10);

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            gameObject.transform.rotation = Quaternion.identity;
        }
    }
}
