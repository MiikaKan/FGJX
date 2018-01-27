using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {

    private Vector3 screenPoint;
    private Vector3 offset;
    [SerializeField]
    private int rotationAmount = 3;

    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseOver()
    {
        gameObject.transform.Rotate(new Vector3(0, rotationAmount, 0) * Input.GetAxis("Mouse ScrollWheel") * 10);
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            gameObject.transform.rotation = Quaternion.identity;
        }
    }
}
